using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

var workspace = @"C:\Users\HG\.codex\worktrees\793a\headlessDCGO";
var inventoryPath = Path.Combine(workspace, "docs", "generated", "as-is-restart", "asis-full-file-inventory.json");
var rolePath = Path.Combine(workspace, "docs", "generated", "as-is-restart", "asis-role-reclassification.json");
var outDocDir = Path.Combine(workspace, "docs", "as-is-restart");
var outGenDir = Path.Combine(workspace, "docs", "generated", "as-is-restart");

Directory.CreateDirectory(outDocDir);
Directory.CreateDirectory(outGenDir);

using var inventoryJson = JsonDocument.Parse(File.ReadAllBytes(inventoryPath));
using var roleJson = JsonDocument.Parse(File.ReadAllBytes(rolePath));

var asisRoot = inventoryJson.RootElement.GetProperty("asisRoot").GetString() ?? "";
var inventoryGeneratedAtUtc = inventoryJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "";
var roleGeneratedAtUtc = roleJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "";
var generatedAtUtc = DateTimeOffset.UtcNow.ToString("O");

var roleMap = LoadRoleMap(roleJson.RootElement);
var csharpInventoryFiles = LoadCSharpInventoryFiles(inventoryJson.RootElement);
var goal01CSharpCount = csharpInventoryFiles.Count;
var fileRecords = new List<CSharpFileRecord>(goal01CSharpCount);
var symbols = new List<SymbolRecord>();
var parseFailures = new List<ParseFailureRecord>();
var parseDiagnosticFiles = new List<ParseDiagnosticFileRecord>();

var parseOptions = CSharpParseOptions.Default
    .WithLanguageVersion(LanguageVersion.Preview)
    .WithDocumentationMode(DocumentationMode.Parse)
    .WithKind(SourceCodeKind.Regular);

foreach (var inventoryFile in csharpInventoryFiles.OrderBy(file => file.RelativePath, StringComparer.OrdinalIgnoreCase))
{
    var roles = roleMap.TryGetValue(inventoryFile.RelativePath, out var role)
        ? role.Roles
        : new[] { "Unknown" };
    var primaryRole = roleMap.TryGetValue(inventoryFile.RelativePath, out var roleAgain)
        ? roleAgain.PrimaryRole
        : "Unknown";
    var isSourceOfTruth = roles.Contains("SourceOfTruth", StringComparer.Ordinal);
    var fileSymbolsStart = symbols.Count;
    var namespaceDeclarations = new SortedSet<string>(StringComparer.Ordinal);
    var parseStatus = "Parsed";
    var parseDiagnosticRecords = new List<ParseDiagnosticRecord>();
    string? parseFailureReason = null;

    try
    {
        var text = File.ReadAllText(inventoryFile.Path);
        var tree = CSharpSyntaxTree.ParseText(text, parseOptions, inventoryFile.Path, Encoding.UTF8);
        var root = tree.GetCompilationUnitRoot();
        var diagnostics = tree.GetDiagnostics().ToList();

        if (diagnostics.Count > 0)
        {
            parseStatus = "ParsedWithDiagnostics";
            parseDiagnosticRecords.AddRange(diagnostics.Take(50).Select(diagnostic => ToParseDiagnosticRecord(diagnostic, tree)));
            parseDiagnosticFiles.Add(new ParseDiagnosticFileRecord(
                inventoryFile.RelativePath,
                diagnostics.Count,
                parseDiagnosticRecords));
        }

        var context = new FileParseContext(
            inventoryFile,
            roles,
            primaryRole,
            tree,
            namespaceDeclarations,
            symbols);
        ProcessCompilationUnit(root, context);
    }
    catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException or ArgumentException or DecoderFallbackException)
    {
        parseStatus = "ParseFailed";
        parseFailureReason = $"{ex.GetType().Name}: {ex.Message}";
        parseFailures.Add(new ParseFailureRecord(inventoryFile.RelativePath, parseFailureReason));
    }

    var fileSymbols = symbols.Skip(fileSymbolsStart).ToList();
    fileRecords.Add(new CSharpFileRecord(
        inventoryFile.Path,
        inventoryFile.RelativePath,
        inventoryFile.Directory,
        inventoryFile.FileName,
        inventoryFile.SizeBytes,
        roles,
        primaryRole,
        isSourceOfTruth,
        namespaceDeclarations.ToArray(),
        parseStatus,
        parseFailureReason,
        parseDiagnosticRecords.Count,
        parseDiagnosticRecords,
        fileSymbols.Count(symbol => symbol.SymbolKind == "namespace"),
        fileSymbols.Count(symbol => symbol.SymbolKind == "type"),
        fileSymbols.Count(symbol => symbol.SymbolKind == "type" && symbol.TypeKind == "delegate"),
        fileSymbols.Count(symbol => symbol.SymbolKind == "method"),
        fileSymbols.Count(symbol => symbol.SymbolKind == "constructor"),
        fileSymbols.Count(symbol => symbol.SymbolKind == "property"),
        fileSymbols.Count(symbol => symbol.SymbolKind == "field"),
        fileSymbols.Count(symbol => symbol.SymbolKind == "event"),
        fileSymbols.Count(symbol => symbol.SymbolKind == "enumMember"),
        fileSymbols.Count(symbol => symbol.SymbolKind == "attribute"),
        inventoryFile.TopLevelFolder,
        inventoryFile.LastWriteTimeUtc));
}

var sourceOfTruthFiles = fileRecords.Where(file => file.IsSourceOfTruth).ToList();
var nonSourceOfTruthFiles = fileRecords.Where(file => !file.IsSourceOfTruth).ToList();
var summary = BuildSummary(
    asisRoot,
    generatedAtUtc,
    inventoryPath,
    rolePath,
    inventoryGeneratedAtUtc,
    roleGeneratedAtUtc,
    goal01CSharpCount,
    fileRecords,
    symbols,
    parseFailures,
    parseDiagnosticFiles,
    sourceOfTruthFiles,
    nonSourceOfTruthFiles);

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

WriteUtf8(
    Path.Combine(outGenDir, "asis-csharp-file-index.json"),
    JsonSerializer.Serialize(new CSharpFileIndexDocument(
        "dcgo.as-is-csharp-file-index.v1",
        generatedAtUtc,
        "GOAL 04 AS-IS C# Symbol Inventory",
        inventoryPath,
        rolePath,
        inventoryGeneratedAtUtc,
        roleGeneratedAtUtc,
        asisRoot,
        goal01CSharpCount,
        fileRecords,
        parseFailures,
        sourceOfTruthFiles.Select(ToFileListItem).ToList(),
        nonSourceOfTruthFiles.Select(ToFileListItem).ToList()), jsonOptions) + Environment.NewLine);

WriteUtf8(
    Path.Combine(outGenDir, "asis-csharp-symbol-index.json"),
    JsonSerializer.Serialize(new CSharpSymbolIndexDocument(
        "dcgo.as-is-csharp-symbol-index.v1",
        generatedAtUtc,
        "GOAL 04 AS-IS C# Symbol Inventory",
        inventoryPath,
        rolePath,
        asisRoot,
        symbols), jsonOptions) + Environment.NewLine);

WriteUtf8(
    Path.Combine(outGenDir, "asis-csharp-symbol-summary.json"),
    JsonSerializer.Serialize(summary, jsonOptions) + Environment.NewLine);

WriteUtf8(
    Path.Combine(outDocDir, "GOAL_04_ASIS_CSHARP_SYMBOL_INVENTORY.md"),
    BuildDetailMarkdown(summary));
WriteUtf8(
    Path.Combine(outDocDir, "asis-csharp-symbol-inventory-summary.md"),
    BuildSummaryMarkdown(summary));

Console.WriteLine(JsonSerializer.Serialize(new
{
    goal01CSharpCount,
    fileIndexCount = fileRecords.Count,
    sourceOfTruthCSharpCount = sourceOfTruthFiles.Count,
    nonSourceOfTruthCSharpCount = nonSourceOfTruthFiles.Count,
    totalSymbolCount = symbols.Count,
    typeCount = summary.Totals.TypeCount,
    methodCount = summary.Totals.MethodCount,
    constructorCount = summary.Totals.ConstructorCount,
    propertyCount = summary.Totals.PropertyCount,
    fieldCount = summary.Totals.FieldCount,
    enumMemberCount = summary.Totals.EnumMemberCount,
    parseFailureCount = parseFailures.Count,
    parseDiagnosticFileCount = parseDiagnosticFiles.Count
}, jsonOptions));

Dictionary<string, RoleInfo> LoadRoleMap(JsonElement roleRoot)
{
    var map = new Dictionary<string, RoleInfo>(StringComparer.OrdinalIgnoreCase);
    foreach (var file in roleRoot.GetProperty("files").EnumerateArray())
    {
        var relativePath = file.GetProperty("relativePath").GetString() ?? "";
        var roles = file.GetProperty("roles")
            .EnumerateArray()
            .Select(item => item.GetString() ?? "")
            .Where(item => item.Length > 0)
            .ToArray();
        var primaryRole = file.GetProperty("primaryRole").GetString() ?? "Unknown";
        map[relativePath] = new RoleInfo(roles, primaryRole);
    }
    return map;
}

List<InventoryFileInfo> LoadCSharpInventoryFiles(JsonElement inventoryRoot)
{
    var result = new List<InventoryFileInfo>();
    foreach (var file in inventoryRoot.GetProperty("files").EnumerateArray())
    {
        var extension = file.GetProperty("extension").GetString() ?? "";
        if (!extension.Equals(".cs", StringComparison.OrdinalIgnoreCase))
        {
            continue;
        }

        result.Add(new InventoryFileInfo(
            file.GetProperty("path").GetString() ?? "",
            file.GetProperty("relativePath").GetString() ?? "",
            file.GetProperty("directory").GetString() ?? "",
            file.GetProperty("fileName").GetString() ?? "",
            file.GetProperty("sizeBytes").GetInt64(),
            file.GetProperty("topLevelFolder").GetString() ?? "",
            file.GetProperty("lastWriteTimeUtc").GetString() ?? ""));
    }
    return result;
}

void ProcessCompilationUnit(CompilationUnitSyntax root, FileParseContext context)
{
    AddAttributeSymbols(root.AttributeLists, context, "", "", "compilationUnit", null);
    foreach (var member in root.Members)
    {
        ProcessMember(member, context, "", "");
    }
}

void ProcessMember(MemberDeclarationSyntax member, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    switch (member)
    {
        case FileScopedNamespaceDeclarationSyntax fileScopedNamespace:
        {
            var ns = CombineNamespace(namespaceName, fileScopedNamespace.Name.ToString());
            AddNamespaceSymbol(fileScopedNamespace, context, ns);
            foreach (var child in fileScopedNamespace.Members)
            {
                ProcessMember(child, context, ns, containingTypeFullName);
            }
            break;
        }
        case NamespaceDeclarationSyntax namespaceDeclaration:
        {
            var ns = CombineNamespace(namespaceName, namespaceDeclaration.Name.ToString());
            AddNamespaceSymbol(namespaceDeclaration, context, ns);
            foreach (var child in namespaceDeclaration.Members)
            {
                ProcessMember(child, context, ns, containingTypeFullName);
            }
            break;
        }
        case ClassDeclarationSyntax classDeclaration:
            ProcessType(classDeclaration, context, namespaceName, containingTypeFullName, "class");
            break;
        case StructDeclarationSyntax structDeclaration:
            ProcessType(structDeclaration, context, namespaceName, containingTypeFullName, "struct");
            break;
        case InterfaceDeclarationSyntax interfaceDeclaration:
            ProcessType(interfaceDeclaration, context, namespaceName, containingTypeFullName, "interface");
            break;
        case EnumDeclarationSyntax enumDeclaration:
            ProcessEnum(enumDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case RecordDeclarationSyntax recordDeclaration:
            ProcessType(
                recordDeclaration,
                context,
                namespaceName,
                containingTypeFullName,
                recordDeclaration.ClassOrStructKeyword.IsKind(SyntaxKind.StructKeyword) ? "struct" : "class");
            break;
        case DelegateDeclarationSyntax delegateDeclaration:
            ProcessDelegate(delegateDeclaration, context, namespaceName, containingTypeFullName);
            break;
    }
}

void ProcessType(TypeDeclarationSyntax typeDeclaration, FileParseContext context, string namespaceName, string containingTypeFullName, string typeKind)
{
    var typeName = TypeNameWithGenerics(typeDeclaration.Identifier.ValueText, typeDeclaration.TypeParameterList);
    var fullName = CombineFullName(namespaceName, containingTypeFullName, typeName);
    var attributes = ExtractAttributes(typeDeclaration.AttributeLists, context.Tree);
    var baseTypes = BaseTypes(typeDeclaration.BaseList);
    var symbolId = SymbolId(context.File.RelativePath, "type", fullName, typeDeclaration.SpanStart);
    var modifiers = Modifiers(typeDeclaration.Modifiers);

    context.NamespaceDeclarations.Add(namespaceName);
    context.Symbols.Add(new SymbolRecord(
        symbolId,
        context.File.Path,
        context.File.RelativePath,
        context.Roles,
        context.PrimaryRole,
        namespaceName,
        containingTypeFullName,
        "type",
        typeKind,
        typeName,
        fullName,
        IsPartial(typeDeclaration.Modifiers),
        AccessModifier(typeDeclaration.Modifiers),
        modifiers,
        HasModifier(typeDeclaration.Modifiers, SyntaxKind.StaticKeyword),
        HasModifier(typeDeclaration.Modifiers, SyntaxKind.AbstractKeyword),
        false,
        false,
        false,
        TypeParameters(typeDeclaration.TypeParameterList),
        Array.Empty<ParameterRecord>(),
        "",
        "",
        "",
        baseTypes.FirstOrDefault() ?? "",
        typeKind == "class" ? baseTypes.Skip(1).ToArray() : baseTypes,
        attributes,
        "",
        ToLocationRecord(typeDeclaration, context.Tree)));

    AddAttributeSymbols(typeDeclaration.AttributeLists, context, namespaceName, fullName, "type", symbolId);

    foreach (var member in typeDeclaration.Members)
    {
        ProcessTypeMember(member, context, namespaceName, fullName);
    }
}

void ProcessEnum(EnumDeclarationSyntax enumDeclaration, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    var typeName = enumDeclaration.Identifier.ValueText;
    var fullName = CombineFullName(namespaceName, containingTypeFullName, typeName);
    var attributes = ExtractAttributes(enumDeclaration.AttributeLists, context.Tree);
    var baseTypes = BaseTypes(enumDeclaration.BaseList);
    var symbolId = SymbolId(context.File.RelativePath, "type", fullName, enumDeclaration.SpanStart);
    var modifiers = Modifiers(enumDeclaration.Modifiers);

    context.NamespaceDeclarations.Add(namespaceName);
    context.Symbols.Add(new SymbolRecord(
        symbolId,
        context.File.Path,
        context.File.RelativePath,
        context.Roles,
        context.PrimaryRole,
        namespaceName,
        containingTypeFullName,
        "type",
        "enum",
        typeName,
        fullName,
        false,
        AccessModifier(enumDeclaration.Modifiers),
        modifiers,
        false,
        false,
        false,
        false,
        false,
        Array.Empty<string>(),
        Array.Empty<ParameterRecord>(),
        "",
        "",
        "",
        baseTypes.FirstOrDefault() ?? "",
        Array.Empty<string>(),
        attributes,
        "",
        ToLocationRecord(enumDeclaration, context.Tree)));

    AddAttributeSymbols(enumDeclaration.AttributeLists, context, namespaceName, fullName, "type", symbolId);

    foreach (var member in enumDeclaration.Members)
    {
        var memberAttributes = ExtractAttributes(member.AttributeLists, context.Tree);
        var memberFullName = $"{fullName}.{member.Identifier.ValueText}";
        var enumMemberSymbolId = SymbolId(context.File.RelativePath, "enumMember", memberFullName, member.SpanStart);
        context.Symbols.Add(new SymbolRecord(
            enumMemberSymbolId,
            context.File.Path,
            context.File.RelativePath,
            context.Roles,
            context.PrimaryRole,
            namespaceName,
            fullName,
            "enumMember",
            "",
            member.Identifier.ValueText,
            memberFullName,
            false,
            "",
            Array.Empty<string>(),
            false,
            false,
            false,
            false,
            false,
            Array.Empty<string>(),
            Array.Empty<ParameterRecord>(),
            "",
            "",
            "",
            "",
            Array.Empty<string>(),
            memberAttributes,
            member.EqualsValue?.Value.ToString() ?? "",
            ToLocationRecord(member, context.Tree)));
        AddAttributeSymbols(member.AttributeLists, context, namespaceName, fullName, "enumMember", enumMemberSymbolId);
    }
}

void ProcessDelegate(DelegateDeclarationSyntax delegateDeclaration, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    var typeName = TypeNameWithGenerics(delegateDeclaration.Identifier.ValueText, delegateDeclaration.TypeParameterList);
    var fullName = CombineFullName(namespaceName, containingTypeFullName, typeName);
    var attributes = ExtractAttributes(delegateDeclaration.AttributeLists, context.Tree);
    var symbolId = SymbolId(context.File.RelativePath, "type", fullName, delegateDeclaration.SpanStart);
    var modifiers = Modifiers(delegateDeclaration.Modifiers);

    context.NamespaceDeclarations.Add(namespaceName);
    context.Symbols.Add(new SymbolRecord(
        symbolId,
        context.File.Path,
        context.File.RelativePath,
        context.Roles,
        context.PrimaryRole,
        namespaceName,
        containingTypeFullName,
        "type",
        "delegate",
        typeName,
        fullName,
        false,
        AccessModifier(delegateDeclaration.Modifiers),
        modifiers,
        false,
        false,
        false,
        false,
        false,
        TypeParameters(delegateDeclaration.TypeParameterList),
        Parameters(delegateDeclaration.ParameterList),
        delegateDeclaration.ReturnType.ToString(),
        "",
        "",
        "",
        Array.Empty<string>(),
        attributes,
        "",
        ToLocationRecord(delegateDeclaration, context.Tree)));
    AddAttributeSymbols(delegateDeclaration.AttributeLists, context, namespaceName, fullName, "type", symbolId);
}

void ProcessTypeMember(MemberDeclarationSyntax member, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    switch (member)
    {
        case ClassDeclarationSyntax classDeclaration:
            ProcessType(classDeclaration, context, namespaceName, containingTypeFullName, "class");
            break;
        case StructDeclarationSyntax structDeclaration:
            ProcessType(structDeclaration, context, namespaceName, containingTypeFullName, "struct");
            break;
        case InterfaceDeclarationSyntax interfaceDeclaration:
            ProcessType(interfaceDeclaration, context, namespaceName, containingTypeFullName, "interface");
            break;
        case EnumDeclarationSyntax enumDeclaration:
            ProcessEnum(enumDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case RecordDeclarationSyntax recordDeclaration:
            ProcessType(
                recordDeclaration,
                context,
                namespaceName,
                containingTypeFullName,
                recordDeclaration.ClassOrStructKeyword.IsKind(SyntaxKind.StructKeyword) ? "struct" : "class");
            break;
        case DelegateDeclarationSyntax delegateDeclaration:
            ProcessDelegate(delegateDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case MethodDeclarationSyntax methodDeclaration:
            AddMethod(methodDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case ConstructorDeclarationSyntax constructorDeclaration:
            AddConstructor(constructorDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case DestructorDeclarationSyntax destructorDeclaration:
            AddDestructor(destructorDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case OperatorDeclarationSyntax operatorDeclaration:
            AddOperator(operatorDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case ConversionOperatorDeclarationSyntax conversionOperatorDeclaration:
            AddConversionOperator(conversionOperatorDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case PropertyDeclarationSyntax propertyDeclaration:
            AddProperty(propertyDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case IndexerDeclarationSyntax indexerDeclaration:
            AddIndexer(indexerDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case FieldDeclarationSyntax fieldDeclaration:
            AddFields(fieldDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case EventFieldDeclarationSyntax eventFieldDeclaration:
            AddEventFields(eventFieldDeclaration, context, namespaceName, containingTypeFullName);
            break;
        case EventDeclarationSyntax eventDeclaration:
            AddEvent(eventDeclaration, context, namespaceName, containingTypeFullName);
            break;
    }
}

void AddMethod(MethodDeclarationSyntax method, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    var name = ExplicitName(method.ExplicitInterfaceSpecifier, TypeNameWithGenerics(method.Identifier.ValueText, method.TypeParameterList));
    AddCallable(
        method,
        context,
        namespaceName,
        containingTypeFullName,
        "method",
        "ordinary",
        name,
        method.ReturnType.ToString(),
        method.Modifiers,
        method.AttributeLists,
        TypeParameters(method.TypeParameterList),
        Parameters(method.ParameterList));
}

void AddConstructor(ConstructorDeclarationSyntax constructor, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    AddCallable(
        constructor,
        context,
        namespaceName,
        containingTypeFullName,
        "constructor",
        "constructor",
        constructor.Identifier.ValueText,
        "",
        constructor.Modifiers,
        constructor.AttributeLists,
        Array.Empty<string>(),
        Parameters(constructor.ParameterList));
}

void AddDestructor(DestructorDeclarationSyntax destructor, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    AddCallable(
        destructor,
        context,
        namespaceName,
        containingTypeFullName,
        "method",
        "destructor",
        $"~{destructor.Identifier.ValueText}",
        "",
        destructor.Modifiers,
        destructor.AttributeLists,
        Array.Empty<string>(),
        Parameters(destructor.ParameterList));
}

void AddOperator(OperatorDeclarationSyntax op, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    AddCallable(
        op,
        context,
        namespaceName,
        containingTypeFullName,
        "method",
        "operator",
        $"operator {op.OperatorToken.Text}",
        op.ReturnType.ToString(),
        op.Modifiers,
        op.AttributeLists,
        Array.Empty<string>(),
        Parameters(op.ParameterList));
}

void AddConversionOperator(ConversionOperatorDeclarationSyntax op, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    AddCallable(
        op,
        context,
        namespaceName,
        containingTypeFullName,
        "method",
        "conversionOperator",
        $"{op.ImplicitOrExplicitKeyword.Text} operator {op.Type}",
        op.Type.ToString(),
        op.Modifiers,
        op.AttributeLists,
        Array.Empty<string>(),
        Parameters(op.ParameterList));
}

void AddCallable(
    MemberDeclarationSyntax node,
    FileParseContext context,
    string namespaceName,
    string containingTypeFullName,
    string symbolKind,
    string symbolSubKind,
    string name,
    string returnType,
    SyntaxTokenList modifierTokens,
    SyntaxList<AttributeListSyntax> attributeLists,
    string[] genericParameters,
    ParameterRecord[] parameters)
{
    var fullName = $"{containingTypeFullName}.{name}";
    var attributes = ExtractAttributes(attributeLists, context.Tree);
    var symbolId = SymbolId(context.File.RelativePath, symbolKind, fullName, node.SpanStart);
    var modifiers = Modifiers(modifierTokens);
    context.Symbols.Add(new SymbolRecord(
        symbolId,
        context.File.Path,
        context.File.RelativePath,
        context.Roles,
        context.PrimaryRole,
        namespaceName,
        containingTypeFullName,
        symbolKind,
        "",
        name,
        fullName,
        false,
        AccessModifier(modifierTokens),
        modifiers,
        HasModifier(modifierTokens, SyntaxKind.StaticKeyword),
        HasModifier(modifierTokens, SyntaxKind.AbstractKeyword),
        HasModifier(modifierTokens, SyntaxKind.VirtualKeyword),
        HasModifier(modifierTokens, SyntaxKind.OverrideKeyword),
        HasModifier(modifierTokens, SyntaxKind.AsyncKeyword),
        genericParameters,
        parameters,
        returnType,
        "",
        "",
        "",
        Array.Empty<string>(),
        attributes,
        symbolSubKind,
        ToLocationRecord(node, context.Tree)));
    AddAttributeSymbols(attributeLists, context, namespaceName, containingTypeFullName, symbolKind, symbolId);
}

void AddProperty(PropertyDeclarationSyntax property, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    var name = ExplicitName(property.ExplicitInterfaceSpecifier, property.Identifier.ValueText);
    AddPropertyLike(
        property,
        context,
        namespaceName,
        containingTypeFullName,
        name,
        property.Type.ToString(),
        property.Modifiers,
        property.AttributeLists,
        "property");
}

void AddIndexer(IndexerDeclarationSyntax indexer, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    AddPropertyLike(
        indexer,
        context,
        namespaceName,
        containingTypeFullName,
        "this[]",
        indexer.Type.ToString(),
        indexer.Modifiers,
        indexer.AttributeLists,
        "indexer");
}

void AddPropertyLike(
    MemberDeclarationSyntax node,
    FileParseContext context,
    string namespaceName,
    string containingTypeFullName,
    string name,
    string propertyType,
    SyntaxTokenList modifierTokens,
    SyntaxList<AttributeListSyntax> attributeLists,
    string symbolSubKind)
{
    var fullName = $"{containingTypeFullName}.{name}";
    var attributes = ExtractAttributes(attributeLists, context.Tree);
    var symbolId = SymbolId(context.File.RelativePath, "property", fullName, node.SpanStart);
    var modifiers = Modifiers(modifierTokens);
    context.Symbols.Add(new SymbolRecord(
        symbolId,
        context.File.Path,
        context.File.RelativePath,
        context.Roles,
        context.PrimaryRole,
        namespaceName,
        containingTypeFullName,
        "property",
        "",
        name,
        fullName,
        false,
        AccessModifier(modifierTokens),
        modifiers,
        HasModifier(modifierTokens, SyntaxKind.StaticKeyword),
        HasModifier(modifierTokens, SyntaxKind.AbstractKeyword),
        HasModifier(modifierTokens, SyntaxKind.VirtualKeyword),
        HasModifier(modifierTokens, SyntaxKind.OverrideKeyword),
        false,
        Array.Empty<string>(),
        Array.Empty<ParameterRecord>(),
        propertyType,
        propertyType,
        "",
        "",
        Array.Empty<string>(),
        attributes,
        symbolSubKind,
        ToLocationRecord(node, context.Tree)));
    AddAttributeSymbols(attributeLists, context, namespaceName, containingTypeFullName, "property", symbolId);
}

void AddFields(FieldDeclarationSyntax field, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    foreach (var variable in field.Declaration.Variables)
    {
        var name = variable.Identifier.ValueText;
        var fullName = $"{containingTypeFullName}.{name}";
        var attributes = ExtractAttributes(field.AttributeLists, context.Tree);
        var symbolId = SymbolId(context.File.RelativePath, "field", fullName, variable.SpanStart);
        var modifiers = Modifiers(field.Modifiers);
        context.Symbols.Add(new SymbolRecord(
            symbolId,
            context.File.Path,
            context.File.RelativePath,
            context.Roles,
            context.PrimaryRole,
            namespaceName,
            containingTypeFullName,
            "field",
            "",
            name,
            fullName,
            false,
            AccessModifier(field.Modifiers),
            modifiers,
            HasModifier(field.Modifiers, SyntaxKind.StaticKeyword),
            false,
            false,
            false,
            false,
            Array.Empty<string>(),
            Array.Empty<ParameterRecord>(),
            "",
            "",
            field.Declaration.Type.ToString(),
            "",
            Array.Empty<string>(),
            attributes,
            variable.Initializer?.Value.ToString() ?? "",
            ToLocationRecord(variable, context.Tree)));
        AddAttributeSymbols(field.AttributeLists, context, namespaceName, containingTypeFullName, "field", symbolId);
    }
}

void AddEventFields(EventFieldDeclarationSyntax eventField, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    foreach (var variable in eventField.Declaration.Variables)
    {
        AddEventLike(
            variable,
            eventField,
            context,
            namespaceName,
            containingTypeFullName,
            variable.Identifier.ValueText,
            eventField.Declaration.Type.ToString(),
            eventField.Modifiers,
            eventField.AttributeLists,
            "eventField");
    }
}

void AddEvent(EventDeclarationSyntax eventDeclaration, FileParseContext context, string namespaceName, string containingTypeFullName)
{
    AddEventLike(
        eventDeclaration,
        eventDeclaration,
        context,
        namespaceName,
        containingTypeFullName,
        ExplicitName(eventDeclaration.ExplicitInterfaceSpecifier, eventDeclaration.Identifier.ValueText),
        eventDeclaration.Type.ToString(),
        eventDeclaration.Modifiers,
        eventDeclaration.AttributeLists,
        "event");
}

void AddEventLike(
    SyntaxNode locationNode,
    MemberDeclarationSyntax memberNode,
    FileParseContext context,
    string namespaceName,
    string containingTypeFullName,
    string name,
    string eventType,
    SyntaxTokenList modifierTokens,
    SyntaxList<AttributeListSyntax> attributeLists,
    string symbolSubKind)
{
    var fullName = $"{containingTypeFullName}.{name}";
    var attributes = ExtractAttributes(attributeLists, context.Tree);
    var symbolId = SymbolId(context.File.RelativePath, "event", fullName, locationNode.SpanStart);
    var modifiers = Modifiers(modifierTokens);
    context.Symbols.Add(new SymbolRecord(
        symbolId,
        context.File.Path,
        context.File.RelativePath,
        context.Roles,
        context.PrimaryRole,
        namespaceName,
        containingTypeFullName,
        "event",
        "",
        name,
        fullName,
        false,
        AccessModifier(modifierTokens),
        modifiers,
        HasModifier(modifierTokens, SyntaxKind.StaticKeyword),
        HasModifier(modifierTokens, SyntaxKind.AbstractKeyword),
        HasModifier(modifierTokens, SyntaxKind.VirtualKeyword),
        HasModifier(modifierTokens, SyntaxKind.OverrideKeyword),
        false,
        Array.Empty<string>(),
        Array.Empty<ParameterRecord>(),
        "",
        "",
        eventType,
        "",
        Array.Empty<string>(),
        attributes,
        symbolSubKind,
        ToLocationRecord(locationNode, context.Tree)));
    AddAttributeSymbols(attributeLists, context, namespaceName, containingTypeFullName, "event", symbolId);
}

void AddNamespaceSymbol(BaseNamespaceDeclarationSyntax ns, FileParseContext context, string namespaceName)
{
    context.NamespaceDeclarations.Add(namespaceName);
    var symbolId = SymbolId(context.File.RelativePath, "namespace", namespaceName, ns.SpanStart);
    context.Symbols.Add(new SymbolRecord(
        symbolId,
        context.File.Path,
        context.File.RelativePath,
        context.Roles,
        context.PrimaryRole,
        namespaceName,
        "",
        "namespace",
        "",
        namespaceName,
        namespaceName,
        false,
        "",
        Array.Empty<string>(),
        false,
        false,
        false,
        false,
        false,
        Array.Empty<string>(),
        Array.Empty<ParameterRecord>(),
        "",
        "",
        "",
        "",
        Array.Empty<string>(),
        Array.Empty<AttributeRecord>(),
        "",
        ToLocationRecord(ns, context.Tree)));
}

void AddAttributeSymbols(
    SyntaxList<AttributeListSyntax> attributeLists,
    FileParseContext context,
    string namespaceName,
    string containingTypeFullName,
    string targetKind,
    string? targetSymbolId)
{
    foreach (var attributeList in attributeLists)
    {
        var target = attributeList.Target?.Identifier.ValueText ?? "";
        foreach (var attribute in attributeList.Attributes)
        {
            var name = attribute.Name.ToString();
            var fullName = targetSymbolId is null
                ? $"{context.File.RelativePath}::{targetKind}::{name}@{attribute.SpanStart}"
                : $"{targetSymbolId}::{name}@{attribute.SpanStart}";
            context.Symbols.Add(new SymbolRecord(
                SymbolId(context.File.RelativePath, "attribute", fullName, attribute.SpanStart),
                context.File.Path,
                context.File.RelativePath,
                context.Roles,
                context.PrimaryRole,
                namespaceName,
                containingTypeFullName,
                "attribute",
                "",
                name,
                fullName,
                false,
                "",
                Array.Empty<string>(),
                false,
                false,
                false,
                false,
                false,
                Array.Empty<string>(),
                Array.Empty<ParameterRecord>(),
                "",
                "",
                "",
                "",
                Array.Empty<string>(),
                Array.Empty<AttributeRecord>(),
                $"targetKind={targetKind};target={target};targetSymbolId={targetSymbolId ?? ""};arguments={attribute.ArgumentList?.ToString() ?? ""}",
                ToLocationRecord(attribute, context.Tree)));
        }
    }
}

AttributeRecord[] ExtractAttributes(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTree tree)
{
    var result = new List<AttributeRecord>();
    foreach (var attributeList in attributeLists)
    {
        var target = attributeList.Target?.Identifier.ValueText ?? "";
        foreach (var attribute in attributeList.Attributes)
        {
            result.Add(new AttributeRecord(
                attribute.Name.ToString(),
                attribute.ArgumentList?.ToString() ?? "",
                target,
                ToLocationRecord(attribute, tree)));
        }
    }
    return result.ToArray();
}

ParameterRecord[] Parameters(ParameterListSyntax? parameterList)
{
    if (parameterList is null)
    {
        return Array.Empty<ParameterRecord>();
    }

    return parameterList.Parameters.Select(parameter => new ParameterRecord(
        parameter.Identifier.ValueText,
        parameter.Type?.ToString() ?? "",
        Modifiers(parameter.Modifiers),
        parameter.Default?.Value.ToString() ?? "",
        parameter.AttributeLists
            .SelectMany(list => list.Attributes.Select(attribute => new AttributeRecord(
                attribute.Name.ToString(),
                attribute.ArgumentList?.ToString() ?? "",
                list.Target?.Identifier.ValueText ?? "",
                new LocationRecord(0, 0, 0, 0))))
            .ToArray())).ToArray();
}

string[] TypeParameters(TypeParameterListSyntax? typeParameterList)
{
    return typeParameterList?.Parameters.Select(parameter => parameter.Identifier.ValueText).ToArray() ?? Array.Empty<string>();
}

string[] BaseTypes(BaseListSyntax? baseList)
{
    return baseList?.Types.Select(type => type.Type.ToString()).ToArray() ?? Array.Empty<string>();
}

string TypeNameWithGenerics(string name, TypeParameterListSyntax? typeParameterList)
{
    var parameters = TypeParameters(typeParameterList);
    return parameters.Length == 0 ? name : $"{name}<{string.Join(", ", parameters)}>";
}

string ExplicitName(ExplicitInterfaceSpecifierSyntax? explicitInterfaceSpecifier, string name)
{
    return explicitInterfaceSpecifier is null ? name : $"{explicitInterfaceSpecifier.Name}.{name}";
}

string CombineNamespace(string current, string next)
{
    if (string.IsNullOrWhiteSpace(current))
    {
        return next;
    }
    if (string.IsNullOrWhiteSpace(next))
    {
        return current;
    }
    return $"{current}.{next}";
}

string CombineFullName(string namespaceName, string containingTypeFullName, string name)
{
    if (!string.IsNullOrWhiteSpace(containingTypeFullName))
    {
        return $"{containingTypeFullName}.{name}";
    }
    return string.IsNullOrWhiteSpace(namespaceName) ? name : $"{namespaceName}.{name}";
}

bool IsPartial(SyntaxTokenList modifiers)
{
    return modifiers.Any(token => token.IsKind(SyntaxKind.PartialKeyword));
}

bool HasModifier(SyntaxTokenList modifiers, SyntaxKind kind)
{
    return modifiers.Any(token => token.IsKind(kind));
}

string[] Modifiers(SyntaxTokenList modifiers)
{
    return modifiers.Select(token => token.Text).Where(text => text.Length > 0).ToArray();
}

string AccessModifier(SyntaxTokenList modifiers)
{
    var hasPublic = HasModifier(modifiers, SyntaxKind.PublicKeyword);
    var hasPrivate = HasModifier(modifiers, SyntaxKind.PrivateKeyword);
    var hasProtected = HasModifier(modifiers, SyntaxKind.ProtectedKeyword);
    var hasInternal = HasModifier(modifiers, SyntaxKind.InternalKeyword);

    return (hasPublic, hasPrivate, hasProtected, hasInternal) switch
    {
        (true, _, _, _) => "public",
        (_, true, true, _) => "private protected",
        (_, true, _, _) => "private",
        (_, _, true, true) => "protected internal",
        (_, _, true, _) => "protected",
        (_, _, _, true) => "internal",
        _ => "default"
    };
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

ParseDiagnosticRecord ToParseDiagnosticRecord(Diagnostic diagnostic, SyntaxTree tree)
{
    var span = diagnostic.Location.GetLineSpan();
    return new ParseDiagnosticRecord(
        diagnostic.Id,
        diagnostic.Severity.ToString(),
        diagnostic.GetMessage(),
        span.StartLinePosition.Line + 1,
        span.StartLinePosition.Character + 1);
}

string SymbolId(string relativePath, string kind, string fullName, int spanStart)
{
    return $"{relativePath}#{kind}#{fullName}#{spanStart}";
}

CSharpSymbolSummaryDocument BuildSummary(
    string asisRoot,
    string generatedAtUtc,
    string inventoryPath,
    string rolePath,
    string inventoryGeneratedAtUtc,
    string roleGeneratedAtUtc,
    int goal01CSharpCount,
    IReadOnlyList<CSharpFileRecord> files,
    IReadOnlyList<SymbolRecord> symbols,
    IReadOnlyList<ParseFailureRecord> parseFailures,
    IReadOnlyList<ParseDiagnosticFileRecord> parseDiagnosticFiles,
    IReadOnlyList<CSharpFileRecord> sourceOfTruthFiles,
    IReadOnlyList<CSharpFileRecord> nonSourceOfTruthFiles)
{
    var typeSymbols = symbols.Where(symbol => symbol.SymbolKind == "type").ToList();
    var methodSymbols = symbols.Where(symbol => symbol.SymbolKind == "method").ToList();
    var constructorSymbols = symbols.Where(symbol => symbol.SymbolKind == "constructor").ToList();
    var propertySymbols = symbols.Where(symbol => symbol.SymbolKind == "property").ToList();
    var fieldSymbols = symbols.Where(symbol => symbol.SymbolKind == "field").ToList();
    var eventSymbols = symbols.Where(symbol => symbol.SymbolKind == "event").ToList();
    var enumMemberSymbols = symbols.Where(symbol => symbol.SymbolKind == "enumMember").ToList();
    var attributeSymbols = symbols.Where(symbol => symbol.SymbolKind == "attribute").ToList();

    return new CSharpSymbolSummaryDocument(
        "dcgo.as-is-csharp-symbol-summary.v1",
        generatedAtUtc,
        "GOAL 04 AS-IS C# Symbol Inventory",
        inventoryPath,
        rolePath,
        inventoryGeneratedAtUtc,
        roleGeneratedAtUtc,
        asisRoot,
        new CSharpSymbolTotals(
            goal01CSharpCount,
            files.Count,
            sourceOfTruthFiles.Count,
            nonSourceOfTruthFiles.Count,
            symbols.Count,
            typeSymbols.Count,
            typeSymbols.Count(symbol => symbol.TypeKind == "class"),
            typeSymbols.Count(symbol => symbol.TypeKind == "struct"),
            typeSymbols.Count(symbol => symbol.TypeKind == "interface"),
            typeSymbols.Count(symbol => symbol.TypeKind == "enum"),
            typeSymbols.Count(symbol => symbol.TypeKind == "delegate"),
            methodSymbols.Count,
            constructorSymbols.Count,
            propertySymbols.Count,
            fieldSymbols.Count,
            eventSymbols.Count,
            enumMemberSymbols.Count,
            attributeSymbols.Count,
            parseFailures.Count,
            parseDiagnosticFiles.Count),
        CountFileRoles(files),
        CountBy(files, file => file.PrimaryRole),
        CountBy(files, file => file.TopLevelFolder),
        CountBy(files, file => file.Directory),
        CountBy(symbols, symbol => symbol.SymbolKind),
        CountBy(typeSymbols, symbol => string.IsNullOrWhiteSpace(symbol.TypeKind) ? "(none)" : symbol.TypeKind),
        CountBy(methodSymbols.Concat(constructorSymbols), symbol => symbol.AccessModifier),
        CountBy(methodSymbols, symbol => string.IsNullOrWhiteSpace(symbol.SymbolSubKind) ? "ordinary" : symbol.SymbolSubKind),
        CountBool(methodSymbols.Concat(constructorSymbols), symbol => symbol.IsStatic, "static", "instance"),
        CountBool(methodSymbols.Concat(constructorSymbols), symbol => symbol.IsAsync, "async", "nonAsync"),
        parseFailures,
        parseDiagnosticFiles,
        sourceOfTruthFiles.Select(ToFileListItem).ToList(),
        nonSourceOfTruthFiles.Select(ToFileListItem).ToList(),
        new[]
        {
            "GOAL 05에서 SourceOfTruth C# symbol index를 기준으로 주요 runtime class와 card data class 범위 선별",
            "parsing diagnostic 파일이 실제 문법 문제인지 Unity conditional symbol 영향인지 별도 확인",
            "namespace/type/member 목록을 기반으로 파일-타입 매핑과 Unity assembly boundary 분석",
            "아직 함수 호출/call graph 분석은 하지 않고 symbol inventory를 다음 분석 입력으로 고정"
        });
}

FileListItem ToFileListItem(CSharpFileRecord file)
{
    return new FileListItem(file.RelativePath, file.Roles, file.PrimaryRole, file.ParseStatus, file.TypeCount, file.MethodCount, file.PropertyCount, file.FieldCount);
}

SortedDictionary<string, int> CountFileRoles(IEnumerable<CSharpFileRecord> files)
{
    var result = new SortedDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    foreach (var file in files)
    {
        foreach (var role in file.Roles)
        {
            result[role] = result.TryGetValue(role, out var count) ? count + 1 : 1;
        }
    }
    return result;
}

SortedDictionary<string, int> CountBy<T>(IEnumerable<T> source, Func<T, string> selector)
{
    var result = new SortedDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    foreach (var item in source)
    {
        var key = selector(item);
        if (string.IsNullOrWhiteSpace(key))
        {
            key = "(none)";
        }
        result[key] = result.TryGetValue(key, out var count) ? count + 1 : 1;
    }
    return result;
}

SortedDictionary<string, int> CountBool<T>(IEnumerable<T> source, Func<T, bool> selector, string trueName, string falseName)
{
    var result = new SortedDictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        [trueName] = 0,
        [falseName] = 0
    };
    foreach (var item in source)
    {
        result[selector(item) ? trueName : falseName] += 1;
    }
    return result;
}

IEnumerable<string[]> SortedRows(IReadOnlyDictionary<string, int> counts)
{
    return counts
        .OrderByDescending(item => item.Value)
        .ThenBy(item => item.Key, StringComparer.OrdinalIgnoreCase)
        .Select(item => new[] { item.Key, item.Value.ToString() });
}

string BuildDetailMarkdown(CSharpSymbolSummaryDocument summary)
{
    var generatedNote = "> 이 문서는 GOAL 01/02 JSON을 입력으로 사용해 전체 `.cs` 파일의 구문 선언 심볼을 목록화한 기준선이다. 파일 본문 의미 분석, 함수 호출 분석, call graph 분석, 구현 작업은 수행하지 않았다.";
    return string.Join(Environment.NewLine, new[]
    {
        "# GOAL 04 AS-IS C# Symbol Inventory",
        "",
        generatedNote,
        "",
        "## 입력 기준",
        "",
        $"- AS-IS root 경로: `{summary.AsisRoot}`",
        "- 입력 file inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`",
        "- 입력 role inventory: `docs/generated/as-is-restart/asis-role-reclassification.json`",
        $"- GOAL 01 생성 시각 UTC: `{summary.InputInventoryGeneratedAtUtc}`",
        $"- GOAL 02 생성 시각 UTC: `{summary.InputRoleGeneratedAtUtc}`",
        $"- GOAL 04 생성 시각 UTC: `{summary.GeneratedAtUtc}`",
        "- 파서: Roslyn CSharpSyntaxTree syntax-only parse",
        "- semantic model 생성: `false`",
        "- 함수 호출/call graph 분석: `false`",
        "",
        "## 전체 요약",
        "",
        MarkdownTable(new[] { "항목", "값" }, new[]
        {
            new[] { "GOAL 01 .cs 파일 수", summary.Totals.Goal01CSharpFileCount.ToString() },
            new[] { "file index .cs 파일 수", summary.Totals.FileIndexCSharpFileCount.ToString() },
            new[] { "SourceOfTruth .cs 파일 수", summary.Totals.SourceOfTruthCSharpFileCount.ToString() },
            new[] { "non-SourceOfTruth .cs 파일 수", summary.Totals.NonSourceOfTruthCSharpFileCount.ToString() },
            new[] { "전체 symbol 수", summary.Totals.TotalSymbolCount.ToString() },
            new[] { "type 수", summary.Totals.TypeCount.ToString() },
            new[] { "method 수", summary.Totals.MethodCount.ToString() },
            new[] { "constructor 수", summary.Totals.ConstructorCount.ToString() },
            new[] { "property 수", summary.Totals.PropertyCount.ToString() },
            new[] { "field 수", summary.Totals.FieldCount.ToString() },
            new[] { "event 수", summary.Totals.EventCount.ToString() },
            new[] { "enum member 수", summary.Totals.EnumMemberCount.ToString() },
            new[] { "attribute symbol 수", summary.Totals.AttributeSymbolCount.ToString() },
            new[] { "parsing 실패 파일 수", summary.Totals.ParseFailureCount.ToString() },
            new[] { "parse diagnostic 파일 수", summary.Totals.ParseDiagnosticFileCount.ToString() }
        }),
        "",
        "## 파일 role별 수",
        "",
        MarkdownTable(new[] { "Role", "파일 수" }, SortedRows(summary.FileRoleCounts)),
        "",
        "## 파일 primaryRole별 수",
        "",
        MarkdownTable(new[] { "PrimaryRole", "파일 수" }, SortedRows(summary.FilePrimaryRoleCounts)),
        "",
        "## 상위 폴더별 .cs 파일 수",
        "",
        MarkdownTable(new[] { "상위 폴더", "파일 수" }, SortedRows(summary.FileTopLevelFolderCounts)),
        "",
        "## Type kind별 수",
        "",
        MarkdownTable(new[] { "Type kind", "수" }, SortedRows(summary.TypeKindCounts)),
        "",
        "## Symbol kind별 수",
        "",
        MarkdownTable(new[] { "Symbol kind", "수" }, SortedRows(summary.SymbolKindCounts)),
        "",
        "## Method 요약",
        "",
        "constructor를 포함한 callable access/static/async 집계와 method sub-kind 집계다.",
        "",
        MarkdownTable(new[] { "Access", "수" }, SortedRows(summary.MethodAccessCounts)),
        "",
        MarkdownTable(new[] { "Method sub-kind", "수" }, SortedRows(summary.MethodSubKindCounts)),
        "",
        MarkdownTable(new[] { "Static 여부", "수" }, SortedRows(summary.MethodStaticCounts)),
        "",
        MarkdownTable(new[] { "Async 여부", "수" }, SortedRows(summary.MethodAsyncCounts)),
        "",
        "## Parsing 실패 파일",
        "",
        ParseFailureMarkdown(summary.ParseFailures),
        "",
        "## Parse diagnostic 파일",
        "",
        ParseDiagnosticMarkdown(summary.ParseDiagnosticFiles.Take(100)),
        "",
        "## SourceOfTruth C# 파일 목록",
        "",
        FileListMarkdown(summary.SourceOfTruthCSharpFiles),
        "",
        "## Non-SourceOfTruth C# 파일 목록",
        "",
        FileListMarkdown(summary.NonSourceOfTruthCSharpFiles),
        "",
        "## 다음 Goal 05 입력 후보",
        "",
        "- `docs/generated/as-is-restart/asis-csharp-file-index.json`",
        "- `docs/generated/as-is-restart/asis-csharp-symbol-index.json`",
        "- `docs/generated/as-is-restart/asis-csharp-symbol-summary.json`",
        "",
        "## 금지 범위 준수",
        "",
        "- `src/` 아래 C# 코드는 수정하지 않았다.",
        "- headless engine 구현은 수행하지 않았다.",
        "- AS-IS 원본은 수정하지 않았다.",
        "- CardEffect body 구현은 수행하지 않았다.",
        "- C0039 이후 card-porting은 실행하지 않았다.",
        "- 함수 호출/call graph 분석은 수행하지 않았다.",
        "- capability 분석/구현은 수행하지 않았다.",
        "- 기존 구현 Verified 판정은 수행하지 않았다.",
        "- commit/push는 수행하지 않았다.",
        ""
    });
}

string BuildSummaryMarkdown(CSharpSymbolSummaryDocument summary)
{
    var generatedNote = "> GOAL 01/02 JSON 기반 syntax-only C# symbol inventory 요약이다. 코드 의미, 함수 호출, call graph 분석은 수행하지 않았다.";
    return string.Join(Environment.NewLine, new[]
    {
        "# AS-IS C# Symbol Inventory Summary",
        "",
        generatedNote,
        "",
        "## 기준선",
        "",
        $"- AS-IS root 경로: `{summary.AsisRoot}`",
        $"- 전체 `.cs` 파일 수: `{summary.Totals.FileIndexCSharpFileCount}`",
        $"- SourceOfTruth `.cs` 파일 수: `{summary.Totals.SourceOfTruthCSharpFileCount}`",
        $"- 전체 type 수: `{summary.Totals.TypeCount}`",
        $"- method/property/field/enum member 수: `{summary.Totals.MethodCount}` / `{summary.Totals.PropertyCount}` / `{summary.Totals.FieldCount}` / `{summary.Totals.EnumMemberCount}`",
        $"- parsing 실패 수: `{summary.Totals.ParseFailureCount}`",
        "",
        "## Type kind별 수",
        "",
        MarkdownTable(new[] { "Type kind", "수" }, SortedRows(summary.TypeKindCounts)),
        "",
        "## Symbol kind별 수",
        "",
        MarkdownTable(new[] { "Symbol kind", "수" }, SortedRows(summary.SymbolKindCounts)),
        "",
        "## 파일 primaryRole별 수",
        "",
        MarkdownTable(new[] { "PrimaryRole", "파일 수" }, SortedRows(summary.FilePrimaryRoleCounts)),
        "",
        "## 다음 Goal 05 입력",
        "",
        "- `docs/generated/as-is-restart/asis-csharp-file-index.json`",
        "- `docs/generated/as-is-restart/asis-csharp-symbol-index.json`",
        "- `docs/generated/as-is-restart/asis-csharp-symbol-summary.json`",
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

string ParseFailureMarkdown(IEnumerable<ParseFailureRecord> failures)
{
    var list = failures.ToList();
    if (list.Count == 0)
    {
        return "- 없음";
    }
    return string.Join(Environment.NewLine, list.Select(item => $"- `{item.RelativePath}`: {item.Reason}"));
}

string ParseDiagnosticMarkdown(IEnumerable<ParseDiagnosticFileRecord> diagnostics)
{
    var list = diagnostics.ToList();
    if (list.Count == 0)
    {
        return "- 없음";
    }
    return string.Join(Environment.NewLine, list.Select(item => $"- `{item.RelativePath}`: diagnostics={item.DiagnosticCount}"));
}

string FileListMarkdown(IEnumerable<FileListItem> files)
{
    var list = files.ToList();
    if (list.Count == 0)
    {
        return "- 없음";
    }
    return string.Join(Environment.NewLine, list.Select(file =>
        $"- `{file.RelativePath}` ({file.PrimaryRole}; types={file.TypeCount}, methods={file.MethodCount}, properties={file.PropertyCount}, fields={file.FieldCount}, parse={file.ParseStatus})"));
}

void WriteUtf8(string filePath, string content)
{
    File.WriteAllText(filePath, content, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
}

public sealed record RoleInfo(string[] Roles, string PrimaryRole);

public sealed record InventoryFileInfo(
    string Path,
    string RelativePath,
    string Directory,
    string FileName,
    long SizeBytes,
    string TopLevelFolder,
    string LastWriteTimeUtc);

public sealed record FileParseContext(
    InventoryFileInfo File,
    string[] Roles,
    string PrimaryRole,
    SyntaxTree Tree,
    SortedSet<string> NamespaceDeclarations,
    List<SymbolRecord> Symbols);

public sealed record AttributeRecord(
    string Name,
    string Arguments,
    string Target,
    LocationRecord Location);

public sealed record ParameterRecord(
    string Name,
    string Type,
    string[] Modifiers,
    string DefaultValue,
    AttributeRecord[] Attributes);

public sealed record LocationRecord(
    int StartLine,
    int StartColumn,
    int EndLine,
    int EndColumn);

public sealed record SymbolRecord(
    string SymbolId,
    string Path,
    string RelativePath,
    string[] Roles,
    string PrimaryRole,
    string Namespace,
    string ContainingTypeFullName,
    string SymbolKind,
    string TypeKind,
    string Name,
    string FullName,
    bool IsPartial,
    string AccessModifier,
    string[] Modifiers,
    bool IsStatic,
    bool IsAbstract,
    bool IsVirtual,
    bool IsOverride,
    bool IsAsync,
    string[] GenericParameters,
    ParameterRecord[] Parameters,
    string ReturnType,
    string PropertyType,
    string FieldType,
    string BaseType,
    string[] ImplementedInterfaces,
    AttributeRecord[] Attributes,
    string SymbolSubKind,
    LocationRecord Location);

public sealed record ParseDiagnosticRecord(
    string Id,
    string Severity,
    string Message,
    int Line,
    int Column);

public sealed record ParseDiagnosticFileRecord(
    string RelativePath,
    int DiagnosticCount,
    IReadOnlyList<ParseDiagnosticRecord> Diagnostics);

public sealed record ParseFailureRecord(
    string RelativePath,
    string Reason);

public sealed record CSharpFileRecord(
    string Path,
    string RelativePath,
    string Directory,
    string FileName,
    long SizeBytes,
    string[] Roles,
    string PrimaryRole,
    bool IsSourceOfTruth,
    string[] NamespaceDeclarations,
    string ParseStatus,
    string? ParseFailureReason,
    int ParseDiagnosticCount,
    IReadOnlyList<ParseDiagnosticRecord> ParseDiagnostics,
    int NamespaceCount,
    int TypeCount,
    int DelegateCount,
    int MethodCount,
    int ConstructorCount,
    int PropertyCount,
    int FieldCount,
    int EventCount,
    int EnumMemberCount,
    int AttributeCount,
    string TopLevelFolder,
    string LastWriteTimeUtc);

public sealed record FileListItem(
    string RelativePath,
    string[] Roles,
    string PrimaryRole,
    string ParseStatus,
    int TypeCount,
    int MethodCount,
    int PropertyCount,
    int FieldCount);

public sealed record CSharpSymbolTotals(
    int Goal01CSharpFileCount,
    int FileIndexCSharpFileCount,
    int SourceOfTruthCSharpFileCount,
    int NonSourceOfTruthCSharpFileCount,
    int TotalSymbolCount,
    int TypeCount,
    int ClassCount,
    int StructCount,
    int InterfaceCount,
    int EnumCount,
    int DelegateCount,
    int MethodCount,
    int ConstructorCount,
    int PropertyCount,
    int FieldCount,
    int EventCount,
    int EnumMemberCount,
    int AttributeSymbolCount,
    int ParseFailureCount,
    int ParseDiagnosticFileCount);

public sealed record CSharpFileIndexDocument(
    string SchemaVersion,
    string GeneratedAtUtc,
    string Goal,
    string InputInventoryPath,
    string InputRolePath,
    string InputInventoryGeneratedAtUtc,
    string InputRoleGeneratedAtUtc,
    string AsisRoot,
    int Goal01CSharpFileCount,
    IReadOnlyList<CSharpFileRecord> Files,
    IReadOnlyList<ParseFailureRecord> ParseFailures,
    IReadOnlyList<FileListItem> SourceOfTruthCSharpFiles,
    IReadOnlyList<FileListItem> NonSourceOfTruthCSharpFiles);

public sealed record CSharpSymbolIndexDocument(
    string SchemaVersion,
    string GeneratedAtUtc,
    string Goal,
    string InputInventoryPath,
    string InputRolePath,
    string AsisRoot,
    IReadOnlyList<SymbolRecord> Symbols);

public sealed record CSharpSymbolSummaryDocument(
    string SchemaVersion,
    string GeneratedAtUtc,
    string Goal,
    string InputInventoryPath,
    string InputRolePath,
    string InputInventoryGeneratedAtUtc,
    string InputRoleGeneratedAtUtc,
    string AsisRoot,
    CSharpSymbolTotals Totals,
    IReadOnlyDictionary<string, int> FileRoleCounts,
    IReadOnlyDictionary<string, int> FilePrimaryRoleCounts,
    IReadOnlyDictionary<string, int> FileTopLevelFolderCounts,
    IReadOnlyDictionary<string, int> FileDirectoryCounts,
    IReadOnlyDictionary<string, int> SymbolKindCounts,
    IReadOnlyDictionary<string, int> TypeKindCounts,
    IReadOnlyDictionary<string, int> MethodAccessCounts,
    IReadOnlyDictionary<string, int> MethodSubKindCounts,
    IReadOnlyDictionary<string, int> MethodStaticCounts,
    IReadOnlyDictionary<string, int> MethodAsyncCounts,
    IReadOnlyList<ParseFailureRecord> ParseFailures,
    IReadOnlyList<ParseDiagnosticFileRecord> ParseDiagnosticFiles,
    IReadOnlyList<FileListItem> SourceOfTruthCSharpFiles,
    IReadOnlyList<FileListItem> NonSourceOfTruthCSharpFiles,
    IReadOnlyList<string> NextGoalCandidateScopes);
