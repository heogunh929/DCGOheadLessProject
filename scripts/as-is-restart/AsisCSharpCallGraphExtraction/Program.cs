using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

var workspace = @"C:\Users\HG\.codex\worktrees\793a\headlessDCGO";
var inventoryPath = Path.Combine(workspace, "docs", "generated", "as-is-restart", "asis-full-file-inventory.json");
var rolePath = Path.Combine(workspace, "docs", "generated", "as-is-restart", "asis-role-reclassification.json");
var fileIndexPath = Path.Combine(workspace, "docs", "generated", "as-is-restart", "asis-csharp-file-index.json");
var symbolIndexPath = Path.Combine(workspace, "docs", "generated", "as-is-restart", "asis-csharp-symbol-index.json");
var outDocDir = Path.Combine(workspace, "docs", "as-is-restart");
var outGenDir = Path.Combine(workspace, "docs", "generated", "as-is-restart");

Directory.CreateDirectory(outDocDir);
Directory.CreateDirectory(outGenDir);

using var inventoryJson = JsonDocument.Parse(File.ReadAllBytes(inventoryPath));
using var roleJson = JsonDocument.Parse(File.ReadAllBytes(rolePath));
using var fileIndexJson = JsonDocument.Parse(File.ReadAllBytes(fileIndexPath));
using var symbolIndexJson = JsonDocument.Parse(File.ReadAllBytes(symbolIndexPath));

var asisRoot = inventoryJson.RootElement.GetProperty("asisRoot").GetString() ?? "";
var inventoryGeneratedAtUtc = inventoryJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "";
var roleGeneratedAtUtc = roleJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "";
var fileIndexGeneratedAtUtc = fileIndexJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "";
var symbolIndexGeneratedAtUtc = symbolIndexJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "";
var generatedAtUtc = DateTimeOffset.UtcNow.ToString("O");

var files = LoadCSharpFiles(fileIndexJson.RootElement);
var symbols = LoadSymbols(symbolIndexJson.RootElement);
var symbolsByFile = symbols
    .GroupBy(symbol => symbol.RelativePath, StringComparer.OrdinalIgnoreCase)
    .ToDictionary(
        group => group.Key,
        group => group.OrderBy(symbol => symbol.Location.EndLine - symbol.Location.StartLine)
            .ThenBy(symbol => symbol.Location.StartLine)
            .ToList(),
        StringComparer.OrdinalIgnoreCase);
var symbolsByName = symbols
    .Where(symbol => !string.IsNullOrWhiteSpace(symbol.Name))
    .GroupBy(symbol => StripGeneric(symbol.Name), StringComparer.Ordinal)
    .ToDictionary(group => group.Key, group => group.ToList(), StringComparer.Ordinal);
var typeSymbolsByName = symbols
    .Where(symbol => symbol.SymbolKind == "type")
    .GroupBy(symbol => StripGeneric(symbol.Name), StringComparer.Ordinal)
    .ToDictionary(group => group.Key, group => group.ToList(), StringComparer.Ordinal);
var memberSymbolsByName = symbols
    .Where(symbol => symbol.SymbolKind is "method" or "constructor" or "property" or "field" or "event" or "enumMember")
    .GroupBy(symbol => StripGeneric(symbol.Name), StringComparer.Ordinal)
    .ToDictionary(group => group.Key, group => group.ToList(), StringComparer.Ordinal);

var parseOptions = CSharpParseOptions.Default
    .WithLanguageVersion(LanguageVersion.Preview)
    .WithDocumentationMode(DocumentationMode.Parse)
    .WithKind(SourceCodeKind.Regular);

const int UnresolvedSampleLimit = 5000;
const int CandidateLimitPerName = 5;

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};
var writerOptions = new JsonWriterOptions
{
    Indented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};
var edgeIndexPath = Path.Combine(outGenDir, "asis-csharp-call-edge-index.json");
var unresolvedPath = Path.Combine(outGenDir, "asis-csharp-unresolved-calls.json");
var edgeIndexTempPath = edgeIndexPath + ".tmp";
var unresolvedTempPath = unresolvedPath + ".tmp";

var stats = new CallGraphAccumulator(UnresolvedSampleLimit);
var perFileInputs = new List<CallGraphFileInputRecord>(files.Count);
var extractionFailures = new List<CallGraphExtractionFailureRecord>();
var edgeSequence = 0;

using var edgeIndexStream = File.Create(edgeIndexTempPath);
using var edgeIndexWriter = new Utf8JsonWriter(edgeIndexStream, writerOptions);
using var unresolvedStream = File.Create(unresolvedTempPath);
using var unresolvedWriter = new Utf8JsonWriter(unresolvedStream, writerOptions);

WriteCallEdgeIndexHeader(edgeIndexWriter);
edgeIndexWriter.WritePropertyName("edges");
edgeIndexWriter.WriteStartArray();

WriteUnresolvedHeader(unresolvedWriter);
unresolvedWriter.WritePropertyName("unresolvedEdges");
unresolvedWriter.WriteStartArray();

foreach (var file in files.OrderBy(file => file.RelativePath, StringComparer.OrdinalIgnoreCase))
{
    var edgeStart = stats.TotalEdgeCount;
    try
    {
        var text = File.ReadAllText(file.Path);
        var tree = CSharpSyntaxTree.ParseText(text, parseOptions, file.Path, Encoding.UTF8);
        var root = tree.GetCompilationUnitRoot();
        var diagnostics = tree.GetDiagnostics().ToList();
        var context = new ExtractionContext(file, tree, symbolsByFile.TryGetValue(file.RelativePath, out var fileSymbols) ? fileSymbols : new List<SymbolRef>());

        foreach (var node in root.DescendantNodes(descendIntoTrivia: false))
        {
            switch (node)
            {
                case InvocationExpressionSyntax invocation:
                    AddInvocationEdge(invocation, context);
                    break;
                case ObjectCreationExpressionSyntax objectCreation:
                    AddObjectCreationEdge(objectCreation, context);
                    break;
                case ImplicitObjectCreationExpressionSyntax implicitObjectCreation:
                    AddImplicitObjectCreationEdge(implicitObjectCreation, context);
                    break;
                case AssignmentExpressionSyntax assignment when assignment.OperatorToken.IsKind(SyntaxKind.PlusEqualsToken) || assignment.OperatorToken.IsKind(SyntaxKind.MinusEqualsToken):
                    AddEventAssignmentEdge(assignment, context);
                    break;
                case MemberAccessExpressionSyntax memberAccess:
                    AddMemberAccessEdge(memberAccess, context);
                    break;
                case IdentifierNameSyntax identifierName:
                    AddIdentifierReferenceEdge(identifierName, context);
                    break;
                case YieldStatementSyntax yieldStatement:
                    AddYieldEdge(yieldStatement, context);
                    break;
            }
        }

        perFileInputs.Add(new CallGraphFileInputRecord(
            file.Path,
            file.RelativePath,
            file.Roles,
            file.PrimaryRole,
            file.IsSourceOfTruth,
            "Parsed",
            diagnostics.Count,
            stats.TotalEdgeCount - edgeStart));
    }
    catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException or ArgumentException or DecoderFallbackException)
    {
        extractionFailures.Add(new CallGraphExtractionFailureRecord(file.RelativePath, $"{ex.GetType().Name}: {ex.Message}"));
        perFileInputs.Add(new CallGraphFileInputRecord(
            file.Path,
            file.RelativePath,
            file.Roles,
            file.PrimaryRole,
            file.IsSourceOfTruth,
            "ExtractionFailed",
            0,
            0));
    }
}

edgeIndexWriter.WriteEndArray();
edgeIndexWriter.WriteNumber("callEdgeCount", stats.TotalEdgeCount);
edgeIndexWriter.WriteEndObject();
edgeIndexWriter.Flush();

unresolvedWriter.WriteEndArray();
unresolvedWriter.WriteNumber("unresolvedCount", stats.UnresolvedCount);
unresolvedWriter.WriteNumber("unresolvedSampleLimit", UnresolvedSampleLimit);
unresolvedWriter.WriteEndObject();
unresolvedWriter.Flush();

edgeIndexWriter.Dispose();
edgeIndexStream.Dispose();
unresolvedWriter.Dispose();
unresolvedStream.Dispose();

File.Move(edgeIndexTempPath, edgeIndexPath, overwrite: true);
File.Move(unresolvedTempPath, unresolvedPath, overwrite: true);

var summary = BuildSummary(stats, perFileInputs, extractionFailures);
var callGraph = BuildCallGraph(stats, perFileInputs, summary);

WriteUtf8(
    Path.Combine(outGenDir, "asis-csharp-call-graph-summary.json"),
    JsonSerializer.Serialize(summary, jsonOptions) + Environment.NewLine);

WriteUtf8(
    Path.Combine(outGenDir, "asis-csharp-call-graph.json"),
    JsonSerializer.Serialize(callGraph, jsonOptions) + Environment.NewLine);

WriteUtf8(Path.Combine(outDocDir, "GOAL_05_ASIS_CSHARP_CALL_GRAPH.md"), BuildDetailMarkdown(summary));
WriteUtf8(Path.Combine(outDocDir, "asis-csharp-call-graph-summary.md"), BuildSummaryMarkdown(summary));

Console.WriteLine(JsonSerializer.Serialize(new
{
    analyzedCSharpFileCount = perFileInputs.Count,
    callEdgeCount = stats.TotalEdgeCount,
    summary.Totals.ExactCount,
    summary.Totals.ProbableCount,
    summary.Totals.HeuristicCount,
    summary.Totals.UnresolvedCount,
    summary.Totals.UnityTaggedCount,
    summary.Totals.GManagerTaggedCount,
    summary.Totals.PhotonTaggedCount,
    summary.Totals.UiTaggedCount,
    summary.Totals.CoroutineTaggedCount,
    summary.Totals.SourceOfTruthCallerEdgeCount,
    extractionFailureCount = extractionFailures.Count
}, jsonOptions));

void WriteCallEdgeIndexHeader(Utf8JsonWriter writer)
{
    writer.WriteStartObject();
    writer.WriteString("schemaVersion", "dcgo.as-is-csharp-call-edge-index.v1");
    writer.WriteString("generatedAtUtc", generatedAtUtc);
    writer.WriteString("goal", "GOAL 05 AS-IS C# Call Graph Extraction");
    writer.WriteString("inputInventoryPath", inventoryPath);
    writer.WriteString("inputRolePath", rolePath);
    writer.WriteString("inputFileIndexPath", fileIndexPath);
    writer.WriteString("inputSymbolIndexPath", symbolIndexPath);
    writer.WriteString("asisRoot", asisRoot);
}

void WriteUnresolvedHeader(Utf8JsonWriter writer)
{
    writer.WriteStartObject();
    writer.WriteString("schemaVersion", "dcgo.as-is-csharp-unresolved-calls.v1");
    writer.WriteString("generatedAtUtc", generatedAtUtc);
    writer.WriteString("goal", "GOAL 05 AS-IS C# Call Graph Extraction");
    writer.WriteString("asisRoot", asisRoot);
}

void AddInvocationEdge(InvocationExpressionSyntax invocation, ExtractionContext context)
{
    var raw = invocation.Expression.ToString();
    var memberName = InvocationName(invocation.Expression);
    var receiver = ReceiverExpression(invocation.Expression);
    var kind = "methodInvocation";
    var tags = TagsFor(raw, receiver, context.File.RelativePath);
    if (tags.Contains("Coroutine"))
    {
        kind = "coroutineCall";
    }
    else if (IsEventCandidate(memberName))
    {
        kind = "eventInvocationCandidate";
    }

    AddEdge(context, invocation, kind, raw, memberName, receiver, invocation.ArgumentList?.Arguments.Count ?? 0, tags);
}

void AddObjectCreationEdge(ObjectCreationExpressionSyntax creation, ExtractionContext context)
{
    var raw = creation.Type.ToString();
    var memberName = LastIdentifier(raw);
    var tags = TagsFor(raw, "", context.File.RelativePath);
    AddEdge(context, creation, tags.Contains("Coroutine") ? "coroutineObjectCreation" : "constructorCall", raw, memberName, "", creation.ArgumentList?.Arguments.Count ?? 0, tags);
}

void AddImplicitObjectCreationEdge(ImplicitObjectCreationExpressionSyntax creation, ExtractionContext context)
{
    var raw = "new()";
    var tags = TagsFor(raw, "", context.File.RelativePath);
    AddEdge(context, creation, "constructorCall", raw, "", "", creation.ArgumentList?.Arguments.Count ?? 0, tags);
}

void AddEventAssignmentEdge(AssignmentExpressionSyntax assignment, ExtractionContext context)
{
    var raw = assignment.Left.ToString();
    var memberName = LastIdentifier(raw);
    var receiver = assignment.Left is MemberAccessExpressionSyntax memberAccess ? memberAccess.Expression.ToString() : "";
    var kind = assignment.OperatorToken.IsKind(SyntaxKind.PlusEqualsToken) ? "eventSubscription" : "eventUnsubscription";
    var tags = TagsFor(raw, receiver, context.File.RelativePath);
    AddEdge(context, assignment, kind, raw, memberName, receiver, 1, tags);
}

void AddMemberAccessEdge(MemberAccessExpressionSyntax memberAccess, ExtractionContext context)
{
    if (memberAccess.Parent is InvocationExpressionSyntax invocation && ReferenceEquals(invocation.Expression, memberAccess))
    {
        return;
    }

    if (memberAccess.Parent is AssignmentExpressionSyntax assignment &&
        ReferenceEquals(assignment.Left, memberAccess) &&
        (assignment.OperatorToken.IsKind(SyntaxKind.PlusEqualsToken) || assignment.OperatorToken.IsKind(SyntaxKind.MinusEqualsToken)))
    {
        return;
    }

    var raw = memberAccess.ToString();
    var memberName = LastIdentifier(memberAccess.Name.ToString());
    var receiver = memberAccess.Expression.ToString();
    var kind = MemberReferenceKind(memberName, receiver);
    var tags = TagsFor(raw, receiver, context.File.RelativePath);
    AddEdge(context, memberAccess, kind, raw, memberName, receiver, 0, tags);
}

void AddIdentifierReferenceEdge(IdentifierNameSyntax identifierName, ExtractionContext context)
{
    if (ShouldSkipIdentifier(identifierName))
    {
        return;
    }

    var raw = identifierName.Identifier.ValueText;
    var candidatesExist = memberSymbolsByName.ContainsKey(raw) || typeSymbolsByName.ContainsKey(raw);
    var tags = TagsFor(raw, "", context.File.RelativePath);
    if (!candidatesExist && tags.Length == 0)
    {
        return;
    }

    var kind = IdentifierReferenceKind(raw);
    AddEdge(context, identifierName, kind, raw, raw, "", 0, tags);
}

void AddYieldEdge(YieldStatementSyntax yieldStatement, ExtractionContext context)
{
    var raw = yieldStatement.Expression?.ToString() ?? yieldStatement.ReturnOrBreakKeyword.Text;
    var tags = TagsFor(raw, "", context.File.RelativePath).Append("Coroutine").Distinct(StringComparer.Ordinal).ToArray();
    AddEdge(context, yieldStatement, "coroutineYield", raw, LastIdentifier(raw), "", 0, tags);
}

void AddEdge(ExtractionContext context, SyntaxNode node, string callKind, string rawCalleeText, string memberName, string receiverExpression, int argumentCount, string[] tags)
{
    var location = ToLocationRecord(node, context.Tree);
    var caller = FindCaller(context, location.StartLine, location.StartColumn);
    var resolution = ResolveCallee(callKind, rawCalleeText, memberName, receiverExpression, tags);
    var callerRoles = caller?.Roles ?? context.File.Roles;
    var callerPrimaryRole = caller?.PrimaryRole ?? context.File.PrimaryRole;
    var crossBoundary = CrossBoundary(callerRoles, resolution.Candidates.FirstOrDefault(), tags);

    edgeSequence += 1;
    var edge = new CallEdgeRecord(
        $"edge-{edgeSequence}",
        context.File.RelativePath,
        caller?.Namespace ?? "",
        caller?.ContainingTypeFullName ?? "",
        caller?.Name ?? "(file-scope)",
        caller?.FullName ?? "(file-scope)",
        caller?.SymbolKind ?? "file",
        callerRoles,
        callerPrimaryRole,
        rawCalleeText,
        resolution.CanonicalCalleeText,
        resolution.Candidates,
        callKind,
        receiverExpression,
        argumentCount,
        location,
        resolution.Confidence,
        tags,
        crossBoundary);

    JsonSerializer.Serialize(edgeIndexWriter, edge, jsonOptions);
    if (edge.ResolutionConfidence == "Unresolved")
    {
        JsonSerializer.Serialize(unresolvedWriter, edge, jsonOptions);
    }
    stats.Record(edge);
}

CallerSymbol? FindCaller(ExtractionContext context, int line, int column)
{
    return context.FileSymbols
        .Where(symbol => symbol.SymbolKind is "method" or "constructor" or "property" or "field" or "event" or "type")
        .Where(symbol => Contains(symbol.Location, line, column))
        .OrderBy(symbol => (symbol.Location.EndLine - symbol.Location.StartLine) * 10000 + (symbol.Location.EndColumn - symbol.Location.StartColumn))
        .Select(symbol => new CallerSymbol(
            symbol.SymbolId,
            symbol.Namespace,
            symbol.ContainingTypeFullName,
            symbol.SymbolKind,
            symbol.Name,
            symbol.FullName,
            symbol.Roles,
            symbol.PrimaryRole))
        .FirstOrDefault();
}

bool Contains(LocationRecord location, int line, int column)
{
    if (line < location.StartLine || line > location.EndLine)
    {
        return false;
    }
    if (line == location.StartLine && column < location.StartColumn)
    {
        return false;
    }
    if (line == location.EndLine && column > Math.Max(location.EndColumn, location.StartColumn))
    {
        return false;
    }
    return true;
}

CalleeResolution ResolveCallee(string callKind, string rawCalleeText, string memberName, string receiverExpression, string[] tags)
{
    var candidates = new List<CalleeCandidateRecord>();
    var normalizedMemberName = StripGeneric(memberName);
    var rawLast = LastIdentifier(rawCalleeText);

    if (callKind.Contains("constructor", StringComparison.OrdinalIgnoreCase))
    {
        AddSymbolCandidates(candidates, typeSymbolsByName.GetValueOrDefault(StripGeneric(rawLast)) ?? new List<SymbolRef>(), rawCalleeText, receiverExpression, preferKinds: new[] { "type" });
    }
    else if (!string.IsNullOrWhiteSpace(normalizedMemberName))
    {
        AddSymbolCandidates(candidates, memberSymbolsByName.GetValueOrDefault(normalizedMemberName) ?? new List<SymbolRef>(), rawCalleeText, receiverExpression, preferKinds: PreferredKinds(callKind));
        if (candidates.Count == 0)
        {
            AddSymbolCandidates(candidates, symbolsByName.GetValueOrDefault(normalizedMemberName) ?? new List<SymbolRef>(), rawCalleeText, receiverExpression, preferKinds: Array.Empty<string>());
        }
    }

    foreach (var tag in tags)
    {
        if (tag is "Unity" or "GManager" or "Photon" or "UI" or "Coroutine")
        {
            candidates.Add(new CalleeCandidateRecord(
                $"{tag}:{rawCalleeText}",
                "ExternalTag",
                tag,
                "",
                "",
                rawCalleeText,
                "",
                "",
                Array.Empty<string>(),
                tag,
                "Heuristic"));
        }
    }

    candidates = candidates
        .GroupBy(candidate => candidate.CandidateKey, StringComparer.Ordinal)
        .Select(group => group.First())
        .Take(8)
        .ToList();

    if (candidates.Count == 0)
    {
        return new CalleeResolution(rawCalleeText, "Unresolved", Array.Empty<CalleeCandidateRecord>());
    }

    var exact = candidates.Where(candidate => candidate.Confidence == "Exact").ToList();
    if (exact.Count == 1)
    {
        return new CalleeResolution(exact[0].CanonicalText, "Exact", candidates);
    }

    var probable = candidates.Where(candidate => candidate.Confidence is "Exact" or "Probable").ToList();
    if (probable.Count == 1)
    {
        return new CalleeResolution(probable[0].CanonicalText, "Probable", candidates);
    }

    return new CalleeResolution(candidates[0].CanonicalText, candidates[0].Source == "ExternalTag" ? "Heuristic" : "Heuristic", candidates);
}

void AddSymbolCandidates(List<CalleeCandidateRecord> candidates, IReadOnlyList<SymbolRef> symbolsForName, string rawCalleeText, string receiverExpression, string[] preferKinds)
{
    var filtered = preferKinds.Length == 0
        ? symbolsForName
        : symbolsForName.Where(symbol => preferKinds.Contains(symbol.SymbolKind, StringComparer.Ordinal)).ToList();
    if (filtered.Count == 0)
    {
        filtered = symbolsForName;
    }

    foreach (var symbol in filtered.Take(CandidateLimitPerName))
    {
        var confidence = CandidateConfidence(symbol, rawCalleeText, receiverExpression, filtered.Count);
        candidates.Add(new CalleeCandidateRecord(
            symbol.SymbolId,
            "SymbolIndex",
            symbol.SymbolKind,
            symbol.Namespace,
            symbol.ContainingTypeFullName,
            symbol.Name,
            symbol.FullName,
            symbol.RelativePath,
            symbol.Roles,
            symbol.PrimaryRole,
            confidence));
    }
}

string CandidateConfidence(SymbolRef symbol, string rawCalleeText, string receiverExpression, int candidateCount)
{
    if (rawCalleeText == symbol.FullName || rawCalleeText.EndsWith("." + symbol.FullName, StringComparison.Ordinal))
    {
        return "Exact";
    }

    var receiverLast = LastIdentifier(receiverExpression);
    if (!string.IsNullOrWhiteSpace(receiverLast) &&
        (symbol.ContainingTypeFullName.EndsWith("." + receiverLast, StringComparison.Ordinal) ||
         symbol.ContainingTypeFullName == receiverLast ||
         StripGeneric(symbol.ContainingTypeFullName.Split('.').LastOrDefault() ?? "") == receiverLast))
    {
        return "Probable";
    }

    return candidateCount == 1 ? "Probable" : "Heuristic";
}

string[] PreferredKinds(string callKind)
{
    return callKind switch
    {
        "methodInvocation" or "coroutineCall" => new[] { "method" },
        "eventInvocationCandidate" or "eventSubscription" or "eventUnsubscription" => new[] { "event" },
        "propertyAccess" => new[] { "property" },
        "fieldAccess" => new[] { "field" },
        "enumReference" => new[] { "enumMember", "type" },
        _ => Array.Empty<string>()
    };
}

string CrossBoundary(string[] callerRoles, CalleeCandidateRecord? firstCandidate, string[] tags)
{
    var callerSource = callerRoles.Contains("SourceOfTruth", StringComparer.Ordinal);
    if (!callerSource)
    {
        return "NonSourceOfTruthCaller";
    }

    if (tags.Contains("Unity", StringComparer.Ordinal))
    {
        return "SourceOfTruthToUnity";
    }

    if (firstCandidate is not null)
    {
        if (firstCandidate.Roles.Contains("SourceOfTruth", StringComparer.Ordinal))
        {
            return "SourceOfTruthInternal";
        }
        if (firstCandidate.Roles.Contains("ExternalPackage", StringComparer.Ordinal) || firstCandidate.PrimaryRole == "ExternalPackage")
        {
            return "SourceOfTruthToExternalPackage";
        }
    }

    return "SourceOfTruthToOtherOrUnresolved";
}

string MemberReferenceKind(string memberName, string receiver)
{
    if (memberSymbolsByName.TryGetValue(memberName, out var candidates))
    {
        if (candidates.Any(candidate => candidate.SymbolKind == "enumMember")) return "enumReference";
        if (candidates.Any(candidate => candidate.SymbolKind == "property")) return "propertyAccess";
        if (candidates.Any(candidate => candidate.SymbolKind == "field")) return "fieldAccess";
        if (candidates.Any(candidate => candidate.SymbolKind == "event")) return "eventReference";
    }

    return LooksStaticReceiver(receiver) ? "staticMemberAccess" : "memberAccessReference";
}

string IdentifierReferenceKind(string identifier)
{
    if (memberSymbolsByName.TryGetValue(identifier, out var candidates))
    {
        if (candidates.Any(candidate => candidate.SymbolKind == "enumMember")) return "enumReference";
        if (candidates.Any(candidate => candidate.SymbolKind == "property")) return "propertyAccess";
        if (candidates.Any(candidate => candidate.SymbolKind == "field")) return "fieldAccess";
        if (candidates.Any(candidate => candidate.SymbolKind == "event")) return "eventReference";
        if (candidates.Any(candidate => candidate.SymbolKind == "method")) return "methodReference";
    }

    if (typeSymbolsByName.ContainsKey(identifier)) return "typeReference";
    return "identifierReference";
}

bool IsEventCandidate(string memberName)
{
    return memberSymbolsByName.TryGetValue(memberName, out var candidates) && candidates.Any(candidate => candidate.SymbolKind == "event");
}

bool LooksStaticReceiver(string receiver)
{
    var last = LastIdentifier(receiver);
    return !string.IsNullOrWhiteSpace(last) && char.IsUpper(last[0]);
}

bool ShouldSkipIdentifier(IdentifierNameSyntax identifier)
{
    if (identifier.Parent is MemberAccessExpressionSyntax memberAccess && ReferenceEquals(memberAccess.Name, identifier))
    {
        return true;
    }
    if (identifier.Parent is QualifiedNameSyntax or UsingDirectiveSyntax or AttributeSyntax or NameEqualsSyntax or NameColonSyntax)
    {
        return true;
    }
    if (identifier.Parent is InvocationExpressionSyntax invocation && ReferenceEquals(invocation.Expression, identifier))
    {
        return true;
    }
    if (identifier.Parent is ObjectCreationExpressionSyntax objectCreation && ReferenceEquals(objectCreation.Type, identifier))
    {
        return true;
    }
    if (identifier.Parent is DeclarationPatternSyntax or TypeDeclarationSyntax or EnumDeclarationSyntax or DelegateDeclarationSyntax)
    {
        return true;
    }
    return false;
}

string InvocationName(ExpressionSyntax expression)
{
    return expression switch
    {
        IdentifierNameSyntax identifier => StripGeneric(identifier.Identifier.ValueText),
        GenericNameSyntax generic => StripGeneric(generic.Identifier.ValueText),
        MemberAccessExpressionSyntax memberAccess => LastIdentifier(memberAccess.Name.ToString()),
        MemberBindingExpressionSyntax memberBinding => LastIdentifier(memberBinding.Name.ToString()),
        ConditionalAccessExpressionSyntax conditional => InvocationName(conditional.WhenNotNull),
        _ => LastIdentifier(expression.ToString())
    };
}

string ReceiverExpression(ExpressionSyntax expression)
{
    return expression switch
    {
        MemberAccessExpressionSyntax memberAccess => memberAccess.Expression.ToString(),
        ConditionalAccessExpressionSyntax conditional => conditional.Expression.ToString(),
        _ => ""
    };
}

string LastIdentifier(string text)
{
    if (string.IsNullOrWhiteSpace(text))
    {
        return "";
    }

    var trimmed = text.Trim();
    var lastDot = trimmed.LastIndexOf('.');
    if (lastDot >= 0 && lastDot < trimmed.Length - 1)
    {
        trimmed = trimmed[(lastDot + 1)..];
    }
    var paren = trimmed.IndexOf('(');
    if (paren >= 0) trimmed = trimmed[..paren];
    var bracket = trimmed.IndexOf('[');
    if (bracket >= 0) trimmed = trimmed[..bracket];
    return StripGeneric(trimmed.Trim());
}

string StripGeneric(string text)
{
    if (string.IsNullOrWhiteSpace(text))
    {
        return "";
    }

    var idx = text.IndexOf('<');
    return idx >= 0 ? text[..idx] : text;
}

string[] TagsFor(string raw, string receiver, string relativePath)
{
    var combined = $"{raw} {receiver} {relativePath}";
    var tags = new List<string>();
    if (ContainsAny(combined, "UnityEngine", "MonoBehaviour", "GameObject", "Transform", "Debug.", "Mathf.", "Resources.", "Instantiate", "Destroy", "GetComponent", "AddComponent", "FindObject", "SceneManager", "Application.", "PlayerPrefs", "Input.", "Time.", "Vector2", "Vector3", "Quaternion", "Color.", "Sprite", "Material", "Animator", "Animation"))
    {
        tags.Add("Unity");
    }
    if (ContainsAny(combined, "GManager", "GameManager", "gManager"))
    {
        tags.Add("GManager");
    }
    if (ContainsAny(combined, "Photon", "PhotonNetwork", "PhotonView", "PunRPC", "RaiseEvent"))
    {
        tags.Add("Photon");
    }
    if (ContainsAny(combined, "UnityEngine.UI", "Button", "TextMeshPro", "TextMesh", "TMP_", "Canvas", "RectTransform", "Dropdown", "Toggle", "Slider", "ScrollRect", "Image", "RawImage", "SelectCardPanel", "outline", "Outline"))
    {
        tags.Add("UI");
    }
    if (ContainsAny(combined, "Coroutine", "StartCoroutine", "StopCoroutine", "StopAllCoroutines", "IEnumerator", "WaitForSeconds", "WaitUntil", "WaitWhile", "yield return"))
    {
        tags.Add("Coroutine");
    }
    return tags.Distinct(StringComparer.Ordinal).ToArray();
}

bool ContainsAny(string text, params string[] needles)
{
    return needles.Any(needle => text.Contains(needle, StringComparison.OrdinalIgnoreCase));
}

LocationRecord ToLocationRecord(SyntaxNode node, SyntaxTree tree)
{
    var span = tree.GetLineSpan(node.Span);
    return new LocationRecord(
        span.StartLinePosition.Line + 1,
        span.StartLinePosition.Character + 1,
        span.EndLinePosition.Line + 1,
        span.EndLinePosition.Character + 1);
}

List<CSharpFileInfo> LoadCSharpFiles(JsonElement fileIndexRoot)
{
    var result = new List<CSharpFileInfo>();
    foreach (var file in fileIndexRoot.GetProperty("files").EnumerateArray())
    {
        result.Add(new CSharpFileInfo(
            file.GetProperty("path").GetString() ?? "",
            file.GetProperty("relativePath").GetString() ?? "",
            file.GetProperty("roles").EnumerateArray().Select(item => item.GetString() ?? "").Where(item => item.Length > 0).ToArray(),
            file.GetProperty("primaryRole").GetString() ?? "Unknown",
            file.GetProperty("isSourceOfTruth").GetBoolean()));
    }
    return result;
}

List<SymbolRef> LoadSymbols(JsonElement symbolIndexRoot)
{
    var result = new List<SymbolRef>();
    foreach (var symbol in symbolIndexRoot.GetProperty("symbols").EnumerateArray())
    {
        var kind = symbol.GetProperty("symbolKind").GetString() ?? "";
        if (kind is not ("type" or "method" or "constructor" or "property" or "field" or "event" or "enumMember"))
        {
            continue;
        }

        var location = symbol.GetProperty("location");
        result.Add(new SymbolRef(
            symbol.GetProperty("symbolId").GetString() ?? "",
            symbol.GetProperty("relativePath").GetString() ?? "",
            symbol.GetProperty("roles").EnumerateArray().Select(item => item.GetString() ?? "").Where(item => item.Length > 0).ToArray(),
            symbol.GetProperty("primaryRole").GetString() ?? "Unknown",
            symbol.GetProperty("namespace").GetString() ?? "",
            symbol.GetProperty("containingTypeFullName").GetString() ?? "",
            kind,
            symbol.GetProperty("typeKind").GetString() ?? "",
            symbol.GetProperty("name").GetString() ?? "",
            symbol.GetProperty("fullName").GetString() ?? "",
            new LocationRecord(
                location.GetProperty("startLine").GetInt32(),
                location.GetProperty("startColumn").GetInt32(),
                location.GetProperty("endLine").GetInt32(),
                location.GetProperty("endColumn").GetInt32())));
    }
    return result;
}

CallGraphSummaryDocument BuildSummary(
    CallGraphAccumulator aggregate,
    IReadOnlyList<CallGraphFileInputRecord> fileInputs,
    IReadOnlyList<CallGraphExtractionFailureRecord> failures)
{
    return new CallGraphSummaryDocument(
        "dcgo.as-is-csharp-call-graph-summary.v1",
        generatedAtUtc,
        "GOAL 05 AS-IS C# Call Graph Extraction",
        inventoryPath,
        rolePath,
        fileIndexPath,
        symbolIndexPath,
        inventoryGeneratedAtUtc,
        roleGeneratedAtUtc,
        fileIndexGeneratedAtUtc,
        symbolIndexGeneratedAtUtc,
        asisRoot,
        new CallGraphTotals(
            files.Count,
            fileInputs.Count,
            aggregate.TotalEdgeCount,
            aggregate.ExactCount,
            aggregate.ProbableCount,
            aggregate.HeuristicCount,
            aggregate.UnresolvedCount,
            aggregate.UnityTaggedCount,
            aggregate.GManagerTaggedCount,
            aggregate.PhotonTaggedCount,
            aggregate.UiTaggedCount,
            aggregate.CoroutineTaggedCount,
            aggregate.SourceOfTruthCallerEdgeCount,
            failures.Count),
        aggregate.CallKindCounts,
        aggregate.ResolutionConfidenceCounts,
        aggregate.CallerRoleCounts,
        aggregate.CallerPrimaryRoleCounts,
        aggregate.CalleeCandidateCounts,
        aggregate.RawCalleeCounts,
        aggregate.CrossBoundaryCounts,
        aggregate.TagCounts,
        aggregate.FileEdgeCounts,
        aggregate.UnresolvedCallSamples,
        aggregate.UnresolvedSampleLimit,
        failures,
        new[]
        {
            "GOAL 06에서 SourceOfTruth caller edge만 별도 분석 입력으로 사용",
            "Unity/GManager/Photon/UI/Coroutine 태그 후보를 headless 필요 여부 판정 전에 별도 검토",
            "Unresolved edge는 semantic compilation 없이 syntax-only로 남은 후보이므로 후속 resolver 입력으로 사용",
            "SourceOfTruthInternal, SourceOfTruthToExternalPackage, SourceOfTruthToUnity 경계를 우선 검토"
        });
}

CallGraphDocument BuildCallGraph(CallGraphAccumulator aggregate, IReadOnlyList<CallGraphFileInputRecord> fileInputs, CallGraphSummaryDocument summary)
{
    var callerNodes = aggregate.CallerNodes.Values
        .Select(node => node.ToRecord())
        .OrderByDescending(node => node.EdgeCount)
        .ThenBy(node => node.CallerFile, StringComparer.OrdinalIgnoreCase)
        .ToList();
    var calleeNodes = aggregate.CalleeNodes.Values
        .Select(node => node.ToRecord())
        .OrderByDescending(node => node.EdgeCount)
        .ThenBy(node => node.CanonicalCalleeText, StringComparer.Ordinal)
        .ToList();

    return new CallGraphDocument(
        "dcgo.as-is-csharp-call-graph.v1",
        generatedAtUtc,
        "GOAL 05 AS-IS C# Call Graph Extraction",
        asisRoot,
        summary.Totals,
        fileInputs,
        callerNodes,
        calleeNodes,
        summary.CrossBoundaryCounts,
        summary.TagCounts);
}

IEnumerable<string[]> SortedRows(IReadOnlyDictionary<string, int> counts)
{
    return counts
        .OrderByDescending(item => item.Value)
        .ThenBy(item => item.Key, StringComparer.OrdinalIgnoreCase)
        .Select(item => new[] { item.Key, item.Value.ToString() });
}

string BuildDetailMarkdown(CallGraphSummaryDocument summary)
{
    var generatedNote = "> 이 문서는 GOAL 04 symbol inventory와 전체 `.cs` 구문 트리를 기반으로 호출/참조 edge를 추출한 기준선이다. headless 필요 여부 판정, capability 분석/구현, Verified 판정은 수행하지 않았다.";
    return string.Join(Environment.NewLine, new[]
    {
        "# GOAL 05 AS-IS C# Call Graph",
        "",
        generatedNote,
        "",
        "## 입력 기준",
        "",
        $"- AS-IS root 경로: `{summary.AsisRoot}`",
        "- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`",
        "- 입력 role inventory: `docs/generated/as-is-restart/asis-role-reclassification.json`",
        "- 입력 C# file index: `docs/generated/as-is-restart/asis-csharp-file-index.json`",
        "- 입력 C# symbol index: `docs/generated/as-is-restart/asis-csharp-symbol-index.json`",
        $"- GOAL 05 생성 시각 UTC: `{summary.GeneratedAtUtc}`",
        "- 추출 방식: Roslyn syntax-only call/reference extraction + GOAL 04 symbol-name heuristic resolution",
        "- semantic model 생성: `false`",
        "- headless 필요 여부 판정: `false`",
        "",
        "## 전체 요약",
        "",
        MarkdownTable(new[] { "항목", "값" }, new[]
        {
            new[] { "분석한 .cs 파일 수", summary.Totals.AnalyzedCSharpFileCount.ToString() },
            new[] { "call edge 총수", summary.Totals.CallEdgeCount.ToString() },
            new[] { "Exact", summary.Totals.ExactCount.ToString() },
            new[] { "Probable", summary.Totals.ProbableCount.ToString() },
            new[] { "Heuristic", summary.Totals.HeuristicCount.ToString() },
            new[] { "Unresolved", summary.Totals.UnresolvedCount.ToString() },
            new[] { "Unity 후보", summary.Totals.UnityTaggedCount.ToString() },
            new[] { "GManager 후보", summary.Totals.GManagerTaggedCount.ToString() },
            new[] { "Photon 후보", summary.Totals.PhotonTaggedCount.ToString() },
            new[] { "UI 후보", summary.Totals.UiTaggedCount.ToString() },
            new[] { "Coroutine 후보", summary.Totals.CoroutineTaggedCount.ToString() },
            new[] { "SourceOfTruth caller 호출 수", summary.Totals.SourceOfTruthCallerEdgeCount.ToString() },
            new[] { "추출 실패 파일 수", summary.Totals.ExtractionFailureCount.ToString() }
        }),
        "",
        "## Call kind별 수",
        "",
        MarkdownTable(new[] { "Call kind", "수" }, SortedRows(summary.CallKindCounts)),
        "",
        "## Resolution confidence별 수",
        "",
        MarkdownTable(new[] { "Confidence", "수" }, SortedRows(summary.ResolutionConfidenceCounts)),
        "",
        "## Caller role별 호출 수",
        "",
        MarkdownTable(new[] { "Caller role", "호출 수" }, SortedRows(summary.CallerRoleCounts)),
        "",
        "## Boundary별 호출 수",
        "",
        MarkdownTable(new[] { "Boundary", "호출 수" }, SortedRows(summary.CrossBoundaryCounts)),
        "",
        "## 태그별 후보 호출 수",
        "",
        MarkdownTable(new[] { "Tag", "호출 수" }, SortedRows(summary.TagCounts)),
        "",
        "## Callee 후보별 호출 수 상위 200개",
        "",
        MarkdownTable(new[] { "Callee 후보", "호출 수" }, SortedRows(summary.CalleeCandidateCounts).Take(200)),
        "",
        "## Unresolved call 샘플 상위 200개",
        "",
        UnresolvedMarkdown(summary.UnresolvedCalls.Take(200)),
        "",
        "## 다음 Goal 06 추천 입력",
        "",
        "- `docs/generated/as-is-restart/asis-csharp-call-graph.json`",
        "- `docs/generated/as-is-restart/asis-csharp-call-edge-index.json`",
        "- `docs/generated/as-is-restart/asis-csharp-unresolved-calls.json`",
        "- `docs/generated/as-is-restart/asis-csharp-call-graph-summary.json`",
        "",
        "## 금지 범위 준수",
        "",
        "- `src/` 아래 C# 코드는 수정하지 않았다.",
        "- headless engine 구현은 수행하지 않았다.",
        "- AS-IS 원본은 수정하지 않았다.",
        "- CardEffect body 구현은 수행하지 않았다.",
        "- C0039 이후 card-porting은 실행하지 않았다.",
        "- headless 필요 여부 최종 판정은 수행하지 않았다.",
        "- capability 분석/구현은 수행하지 않았다.",
        "- existing implementation trust audit은 수행하지 않았다.",
        "- Verified 판정은 수행하지 않았다.",
        "- commit/push는 수행하지 않았다.",
        ""
    });
}

string BuildSummaryMarkdown(CallGraphSummaryDocument summary)
{
    var generatedNote = "> GOAL 05 syntax-only C# call graph 요약이다. 구현 변경이나 headless 필요 여부 판정은 포함하지 않는다.";
    return string.Join(Environment.NewLine, new[]
    {
        "# AS-IS C# Call Graph Summary",
        "",
        generatedNote,
        "",
        "## 기준선",
        "",
        $"- AS-IS root 경로: `{summary.AsisRoot}`",
        $"- 분석한 `.cs` 파일 수: `{summary.Totals.AnalyzedCSharpFileCount}`",
        $"- call edge 총수: `{summary.Totals.CallEdgeCount}`",
        $"- Exact/Probable/Heuristic/Unresolved: `{summary.Totals.ExactCount}` / `{summary.Totals.ProbableCount}` / `{summary.Totals.HeuristicCount}` / `{summary.Totals.UnresolvedCount}`",
        $"- Unity/GManager/Photon/UI/Coroutine 후보: `{summary.Totals.UnityTaggedCount}` / `{summary.Totals.GManagerTaggedCount}` / `{summary.Totals.PhotonTaggedCount}` / `{summary.Totals.UiTaggedCount}` / `{summary.Totals.CoroutineTaggedCount}`",
        $"- SourceOfTruth caller 호출 수: `{summary.Totals.SourceOfTruthCallerEdgeCount}`",
        "",
        "## Boundary별 호출 수",
        "",
        MarkdownTable(new[] { "Boundary", "호출 수" }, SortedRows(summary.CrossBoundaryCounts)),
        "",
        "## Call kind별 수",
        "",
        MarkdownTable(new[] { "Call kind", "수" }, SortedRows(summary.CallKindCounts)),
        "",
        "## 다음 Goal 06 추천 입력",
        "",
        "- `docs/generated/as-is-restart/asis-csharp-call-graph.json`",
        "- `docs/generated/as-is-restart/asis-csharp-call-edge-index.json`",
        "- `docs/generated/as-is-restart/asis-csharp-unresolved-calls.json`",
        "- `docs/generated/as-is-restart/asis-csharp-call-graph-summary.json`",
        ""
    });
}

string MarkdownTable(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rows)
{
    var headerArray = headers.Select(EscapeTableValue).ToArray();
    var builder = new StringBuilder();
    builder.Append("| ");
    builder.Append(string.Join(" | ", headerArray));
    builder.AppendLine(" |");
    builder.Append("| ");
    builder.Append(string.Join(" | ", headerArray.Select(_ => "---")));
    builder.AppendLine(" |");
    foreach (var row in rows)
    {
        builder.Append("| ");
        builder.Append(string.Join(" | ", row.Select(EscapeTableValue)));
        builder.AppendLine(" |");
    }
    return builder.ToString().TrimEnd();
}

string EscapeTableValue(string value)
{
    return value.Replace("|", "\\|");
}

string UnresolvedMarkdown(IEnumerable<UnresolvedCallRecord> records)
{
    var list = records.ToList();
    if (list.Count == 0) return "- 없음";
    return string.Join(Environment.NewLine, list.Select(edge => $"- `{edge.CallerFile}` `{edge.CallerMember}` -> `{edge.RawCalleeText}` ({edge.CallKind}, line {edge.Location.StartLine})"));
}

void WriteUtf8(string filePath, string content)
{
    File.WriteAllText(filePath, content, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
}

public sealed record CSharpFileInfo(string Path, string RelativePath, string[] Roles, string PrimaryRole, bool IsSourceOfTruth);
public sealed record ExtractionContext(CSharpFileInfo File, SyntaxTree Tree, IReadOnlyList<SymbolRef> FileSymbols);
public sealed record LocationRecord(int StartLine, int StartColumn, int EndLine, int EndColumn);
public sealed record SymbolRef(string SymbolId, string RelativePath, string[] Roles, string PrimaryRole, string Namespace, string ContainingTypeFullName, string SymbolKind, string TypeKind, string Name, string FullName, LocationRecord Location);
public sealed record CallerSymbol(string SymbolId, string Namespace, string ContainingTypeFullName, string SymbolKind, string Name, string FullName, string[] Roles, string PrimaryRole);
public sealed record CalleeCandidateRecord(string CandidateKey, string Source, string SymbolKind, string Namespace, string ContainingTypeFullName, string Name, string CanonicalText, string RelativePath, string[] Roles, string PrimaryRole, string Confidence);
public sealed record CalleeResolution(string CanonicalCalleeText, string Confidence, IReadOnlyList<CalleeCandidateRecord> Candidates);
public sealed record CallEdgeRecord(string EdgeId, string CallerFile, string CallerNamespace, string CallerTypeFullName, string CallerMember, string CallerFullName, string CallerSymbolKind, string[] CallerRoles, string CallerPrimaryRole, string RawCalleeText, string CanonicalCalleeText, IReadOnlyList<CalleeCandidateRecord> CalleeCandidates, string CallKind, string ReceiverExpression, int ArgumentCount, LocationRecord Location, string ResolutionConfidence, string[] Tags, string CrossBoundaryKind);
public sealed record CallGraphFileInputRecord(string Path, string RelativePath, string[] Roles, string PrimaryRole, bool IsSourceOfTruth, string ParseStatus, int ParseDiagnosticCount, int EdgeCount);
public sealed record CallGraphExtractionFailureRecord(string RelativePath, string Reason);
public sealed record UnresolvedCallRecord(string EdgeId, string CallerFile, string CallerMember, string RawCalleeText, string CallKind, LocationRecord Location, string[] Tags);
public sealed record CallGraphTotals(int Goal04CSharpFileCount, int AnalyzedCSharpFileCount, int CallEdgeCount, int ExactCount, int ProbableCount, int HeuristicCount, int UnresolvedCount, int UnityTaggedCount, int GManagerTaggedCount, int PhotonTaggedCount, int UiTaggedCount, int CoroutineTaggedCount, int SourceOfTruthCallerEdgeCount, int ExtractionFailureCount);
public sealed record CallEdgeIndexDocument(string SchemaVersion, string GeneratedAtUtc, string Goal, string InputInventoryPath, string InputRolePath, string InputFileIndexPath, string InputSymbolIndexPath, string AsisRoot, IReadOnlyList<CallEdgeRecord> Edges);
public sealed record UnresolvedCallsDocument(string SchemaVersion, string GeneratedAtUtc, string Goal, string AsisRoot, int UnresolvedCount, IReadOnlyList<CallEdgeRecord> UnresolvedEdges);
public sealed record CallGraphSummaryDocument(string SchemaVersion, string GeneratedAtUtc, string Goal, string InputInventoryPath, string InputRolePath, string InputFileIndexPath, string InputSymbolIndexPath, string InputInventoryGeneratedAtUtc, string InputRoleGeneratedAtUtc, string InputFileIndexGeneratedAtUtc, string InputSymbolIndexGeneratedAtUtc, string AsisRoot, CallGraphTotals Totals, IReadOnlyDictionary<string, int> CallKindCounts, IReadOnlyDictionary<string, int> ResolutionConfidenceCounts, IReadOnlyDictionary<string, int> CallerRoleCounts, IReadOnlyDictionary<string, int> CallerPrimaryRoleCounts, IReadOnlyDictionary<string, int> CalleeCandidateCounts, IReadOnlyDictionary<string, int> RawCalleeCounts, IReadOnlyDictionary<string, int> CrossBoundaryCounts, IReadOnlyDictionary<string, int> TagCounts, IReadOnlyDictionary<string, int> FileEdgeCounts, IReadOnlyList<UnresolvedCallRecord> UnresolvedCalls, int UnresolvedSampleLimit, IReadOnlyList<CallGraphExtractionFailureRecord> ExtractionFailures, IReadOnlyList<string> NextGoalCandidateScopes);

public sealed class CallGraphAccumulator
{
    public CallGraphAccumulator(int unresolvedSampleLimit)
    {
        UnresolvedSampleLimit = unresolvedSampleLimit;
    }

    public int UnresolvedSampleLimit { get; }
    public int TotalEdgeCount { get; private set; }
    public int ExactCount { get; private set; }
    public int ProbableCount { get; private set; }
    public int HeuristicCount { get; private set; }
    public int UnresolvedCount { get; private set; }
    public int UnityTaggedCount { get; private set; }
    public int GManagerTaggedCount { get; private set; }
    public int PhotonTaggedCount { get; private set; }
    public int UiTaggedCount { get; private set; }
    public int CoroutineTaggedCount { get; private set; }
    public int SourceOfTruthCallerEdgeCount { get; private set; }
    public SortedDictionary<string, int> CallKindCounts { get; } = new(StringComparer.OrdinalIgnoreCase);
    public SortedDictionary<string, int> ResolutionConfidenceCounts { get; } = new(StringComparer.OrdinalIgnoreCase);
    public SortedDictionary<string, int> CallerRoleCounts { get; } = new(StringComparer.OrdinalIgnoreCase);
    public SortedDictionary<string, int> CallerPrimaryRoleCounts { get; } = new(StringComparer.OrdinalIgnoreCase);
    public SortedDictionary<string, int> CalleeCandidateCounts { get; } = new(StringComparer.OrdinalIgnoreCase);
    public SortedDictionary<string, int> RawCalleeCounts { get; } = new(StringComparer.OrdinalIgnoreCase);
    public SortedDictionary<string, int> CrossBoundaryCounts { get; } = new(StringComparer.OrdinalIgnoreCase);
    public SortedDictionary<string, int> TagCounts { get; } = new(StringComparer.OrdinalIgnoreCase);
    public SortedDictionary<string, int> FileEdgeCounts { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, CallerNodeAccumulator> CallerNodes { get; } = new(StringComparer.Ordinal);
    public Dictionary<string, CalleeNodeAccumulator> CalleeNodes { get; } = new(StringComparer.Ordinal);
    public List<UnresolvedCallRecord> UnresolvedCallSamples { get; } = new();

    public void Record(CallEdgeRecord edge)
    {
        TotalEdgeCount += 1;
        Increment(CallKindCounts, edge.CallKind);
        Increment(ResolutionConfidenceCounts, edge.ResolutionConfidence);
        Increment(CallerPrimaryRoleCounts, edge.CallerPrimaryRole);
        Increment(CalleeCandidateCounts, edge.CanonicalCalleeText);
        Increment(RawCalleeCounts, edge.RawCalleeText);
        Increment(CrossBoundaryCounts, edge.CrossBoundaryKind);
        Increment(FileEdgeCounts, edge.CallerFile);

        foreach (var role in edge.CallerRoles)
        {
            Increment(CallerRoleCounts, role);
        }

        foreach (var tag in edge.Tags)
        {
            Increment(TagCounts, tag);
        }

        if (edge.ResolutionConfidence == "Exact") ExactCount += 1;
        if (edge.ResolutionConfidence == "Probable") ProbableCount += 1;
        if (edge.ResolutionConfidence == "Heuristic") HeuristicCount += 1;
        if (edge.ResolutionConfidence == "Unresolved")
        {
            UnresolvedCount += 1;
            if (UnresolvedCallSamples.Count < UnresolvedSampleLimit)
            {
                UnresolvedCallSamples.Add(new UnresolvedCallRecord(edge.EdgeId, edge.CallerFile, edge.CallerFullName, edge.RawCalleeText, edge.CallKind, edge.Location, edge.Tags));
            }
        }

        if (edge.Tags.Contains("Unity", StringComparer.Ordinal)) UnityTaggedCount += 1;
        if (edge.Tags.Contains("GManager", StringComparer.Ordinal)) GManagerTaggedCount += 1;
        if (edge.Tags.Contains("Photon", StringComparer.Ordinal)) PhotonTaggedCount += 1;
        if (edge.Tags.Contains("UI", StringComparer.Ordinal)) UiTaggedCount += 1;
        if (edge.Tags.Contains("Coroutine", StringComparer.Ordinal)) CoroutineTaggedCount += 1;
        if (edge.CallerRoles.Contains("SourceOfTruth", StringComparer.Ordinal)) SourceOfTruthCallerEdgeCount += 1;

        var callerKey = $"{edge.CallerFile}\u001f{edge.CallerFullName}\u001f{edge.CallerPrimaryRole}";
        if (!CallerNodes.TryGetValue(callerKey, out var callerNode))
        {
            callerNode = new CallerNodeAccumulator(edge.CallerFile, edge.CallerFullName, edge.CallerPrimaryRole);
            CallerNodes[callerKey] = callerNode;
        }
        callerNode.Record(edge);

        if (!CalleeNodes.TryGetValue(edge.CanonicalCalleeText, out var calleeNode))
        {
            calleeNode = new CalleeNodeAccumulator(edge.CanonicalCalleeText);
            CalleeNodes[edge.CanonicalCalleeText] = calleeNode;
        }
        calleeNode.Record(edge);
    }

    private static void Increment(SortedDictionary<string, int> counts, string key)
    {
        if (string.IsNullOrWhiteSpace(key)) key = "(empty)";
        counts[key] = counts.TryGetValue(key, out var count) ? count + 1 : 1;
    }
}

public sealed class CallerNodeAccumulator
{
    public CallerNodeAccumulator(string callerFile, string callerFullName, string callerPrimaryRole)
    {
        CallerFile = callerFile;
        CallerFullName = callerFullName;
        CallerPrimaryRole = callerPrimaryRole;
    }

    public string CallerFile { get; }
    public string CallerFullName { get; }
    public string CallerPrimaryRole { get; }
    public int EdgeCount { get; private set; }
    public int UnresolvedCount { get; private set; }
    public SortedSet<string> Tags { get; } = new(StringComparer.Ordinal);

    public void Record(CallEdgeRecord edge)
    {
        EdgeCount += 1;
        if (edge.ResolutionConfidence == "Unresolved") UnresolvedCount += 1;
        foreach (var tag in edge.Tags) Tags.Add(tag);
    }

    public CallerNodeRecord ToRecord() => new(CallerFile, CallerFullName, CallerPrimaryRole, EdgeCount, UnresolvedCount, Tags.ToArray());
}

public sealed class CalleeNodeAccumulator
{
    public CalleeNodeAccumulator(string canonicalCalleeText)
    {
        CanonicalCalleeText = canonicalCalleeText;
    }

    public string CanonicalCalleeText { get; }
    public int EdgeCount { get; private set; }
    public SortedSet<string> ConfidenceKinds { get; } = new(StringComparer.Ordinal);
    public SortedSet<string> Tags { get; } = new(StringComparer.Ordinal);

    public void Record(CallEdgeRecord edge)
    {
        EdgeCount += 1;
        ConfidenceKinds.Add(edge.ResolutionConfidence);
        foreach (var tag in edge.Tags) Tags.Add(tag);
    }

    public CalleeNodeRecord ToRecord() => new(CanonicalCalleeText, EdgeCount, ConfidenceKinds.ToArray(), Tags.ToArray());
}

public sealed record CallerNodeRecord(string CallerFile, string CallerFullName, string CallerPrimaryRole, int EdgeCount, int UnresolvedCount, string[] Tags);
public sealed record CalleeNodeRecord(string CanonicalCalleeText, int EdgeCount, string[] ConfidenceKinds, string[] Tags);
public sealed record CallGraphDocument(string SchemaVersion, string GeneratedAtUtc, string Goal, string AsisRoot, CallGraphTotals Totals, IReadOnlyList<CallGraphFileInputRecord> Files, IReadOnlyList<CallerNodeRecord> CallerNodes, IReadOnlyList<CalleeNodeRecord> CalleeNodes, IReadOnlyDictionary<string, int> CrossBoundaryCounts, IReadOnlyDictionary<string, int> TagCounts);
