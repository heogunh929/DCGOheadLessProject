using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed class DeclarationEffectService
{
    private static readonly TriggerSourceZone DeclarationSourceZones =
        TriggerSourceZone.FieldTop
        | TriggerSourceZone.Inherited
        | TriggerSourceZone.Linked
        | TriggerSourceZone.Hand
        | TriggerSourceZone.Trash;

    private readonly TriggerPipelineService _triggerPipelineService;

    public DeclarationEffectService(TriggerPipelineService triggerPipelineService)
    {
        _triggerPipelineService = triggerPipelineService
            ?? throw new ArgumentNullException(nameof(triggerPipelineService));
    }

    internal TriggerPipelineService RuntimeTriggerPipelineService => _triggerPipelineService;

    public IReadOnlyList<LegalAction> GenerateLegalActions(GameState state, PlayerId playerId)
    {
        ArgumentNullException.ThrowIfNull(state);
        if (state.IsGameOver || state.Phase != Phase.Main || state.TurnPlayerId != playerId)
        {
            return Array.Empty<LegalAction>();
        }

        var prepared = PrepareDeclarationGroup(state, playerId);
        return prepared.QueuedEffects
            .Where(resolution => IsDeclarationCandidate(state, resolution, playerId))
            .Select(resolution => CreateLegalAction(state, playerId, resolution))
            .ToArray();
    }

    public TriggerPipelineResult ExecuteWithResult(
        GameState state,
        DeclareEffectAction action,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(action);

        if (state.Phase != Phase.Main)
        {
            throw new DomainException("Declared effects can only be activated during the main phase.");
        }

        if (state.TurnPlayerId != action.Actor)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can declare effects.");
        }

        var prepared = PrepareDeclarationGroup(
            state,
            action.Actor,
            action.SourceCard,
            action.SourcePermanent,
            descriptor => string.Equals(descriptor.StableId, action.EffectStableId, StringComparison.Ordinal));

        var candidates = prepared.QueuedEffects
            .Where(resolution => IsMatchingActionCandidate(state, resolution, action))
            .ToArray();

        if (candidates.Length == 0)
        {
            throw new DomainException(
                $"Declared effect '{action.EffectStableId}' from source card '{action.SourceCard}' is not available.");
        }

        if (candidates.Length > 1)
        {
            throw new DomainException(
                $"Declared effect '{action.EffectStableId}' from source card '{action.SourceCard}' is ambiguous.");
        }

        var selected = candidates[0];
        var selectedGroup = new PreparedTriggerGroup(
            prepared.Context,
            new[] { selected },
            Array.Empty<EffectResolution>(),
            prepared.Options);

        return _triggerPipelineService.RunPrepared(state, selectedGroup, trace);
    }

    private PreparedTriggerGroup PrepareDeclarationGroup(
        GameState state,
        PlayerId playerId,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        Func<EffectDescriptor, bool>? descriptorFilter = null) =>
        _triggerPipelineService.Prepare(
            state,
            EffectTiming.OnDeclaration,
            playerId,
            sourceCard,
            sourcePermanent,
            options: new TriggerPipelineOptions(
                SourceZones: DeclarationSourceZones,
                ResolveAfterEffectsActivate: true,
                UseMultipleSkillsOrdering: false),
            descriptorFilter: descriptorFilter);

    private static LegalAction CreateLegalAction(
        GameState state,
        PlayerId playerId,
        EffectResolution resolution)
    {
        var sourceCard = resolution.SourceCard!.Value;
        var snapshot = resolution.SourceSnapshot
            ?? throw new DomainException($"Declared effect '{resolution.StableId}' requires a source snapshot.");
        var actionId = StableActionId(playerId, resolution, snapshot);
        var definitionId = state.Cards[sourceCard].DefinitionId;
        var label = $"Declare {definitionId}";
        var action = new DeclareEffectAction(
            playerId,
            resolution.StableId,
            sourceCard,
            resolution.SourcePermanent,
            snapshot.SourceZone,
            snapshot.Role,
            resolution.OncePerTurnKey)
        {
            Metadata = new GameActionMetadata
            {
                StableId = actionId,
                DebugLabel = label,
                Tags = new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    ["effectStableId"] = resolution.StableId,
                    ["sourceCard"] = sourceCard.Value.ToString(),
                    ["sourcePermanent"] = resolution.SourcePermanent?.Value.ToString() ?? string.Empty,
                    ["sourceZone"] = snapshot.SourceZone.ToString(),
                    ["sourceRole"] = snapshot.Role.ToString(),
                    ["oncePerTurnKey"] = resolution.OncePerTurnKey ?? string.Empty,
                },
            },
        };

        var target = resolution.SourcePermanent is { } permanent
            ? PermanentTarget(permanent, playerId, label)
            : CardTarget(sourceCard, playerId, definitionId, snapshot.SourceZone);

        return new LegalAction(
            actionId,
            LegalActionKind.ActivateEffect,
            action,
            label,
            new[] { target });
    }

    private static bool IsMatchingActionCandidate(
        GameState state,
        EffectResolution resolution,
        DeclareEffectAction action)
    {
        if (!IsDeclarationCandidate(state, resolution, action.Actor))
        {
            return false;
        }

        if (!string.Equals(resolution.StableId, action.EffectStableId, StringComparison.Ordinal)
            || resolution.SourceCard != action.SourceCard
            || resolution.SourcePermanent != action.SourcePermanent)
        {
            return false;
        }

        var snapshot = resolution.SourceSnapshot;
        if (snapshot is null
            || snapshot.Role != action.SourceRole
            || snapshot.SourceZone != action.SourceZone)
        {
            return false;
        }

        return string.Equals(
            resolution.OncePerTurnKey ?? string.Empty,
            action.OncePerTurnKey ?? string.Empty,
            StringComparison.Ordinal);
    }

    private static bool IsDeclarationCandidate(
        GameState state,
        EffectResolution resolution,
        PlayerId playerId)
    {
        if (resolution.SourceCard is null || resolution.SourceSnapshot is null)
        {
            return false;
        }

        if (ControllerOf(resolution) != playerId)
        {
            return false;
        }

        if (!IsAllowedDeclarationRole(resolution.SourceSnapshot.Role))
        {
            return false;
        }

        if (resolution.CanActivate?.Invoke(resolution.Context) == false)
        {
            return false;
        }

        if (!resolution.IsOncePerTurn)
        {
            return true;
        }

        var key = resolution.OncePerTurnKey ?? resolution.StableId;
        return state.RuntimeRules.CanUseOncePerTurn(
            state.TurnCount,
            playerId,
            key,
            resolution.SourceCard);
    }

    private static bool IsAllowedDeclarationRole(TriggerSourceRole role) =>
        role is TriggerSourceRole.FieldTop
            or TriggerSourceRole.Inherited
            or TriggerSourceRole.Linked
            or TriggerSourceRole.Hand
            or TriggerSourceRole.Trash;

    private static string StableActionId(
        PlayerId playerId,
        EffectResolution resolution,
        TriggerSourceSnapshot snapshot) =>
        $"declare:{playerId.Value}:{snapshot.Role}:{snapshot.SourceZone}:{resolution.SourceCard!.Value}:{resolution.SourcePermanent?.Value.ToString() ?? "none"}:{resolution.StableId}";

    private static PlayerId ControllerOf(EffectResolution resolution) =>
        resolution.Controller ?? resolution.Context.Player ?? PlayerId.Player0;

    private static SelectableTarget CardTarget(
        CardInstanceId card,
        PlayerId owner,
        string label,
        Zone sourceZone) =>
        new(
            SelectionTargetKind.Card,
            $"card:{card.Value}",
            owner,
            Card: card,
            Label: label,
            Zone: sourceZone);

    private static SelectableTarget PermanentTarget(
        PermanentId permanent,
        PlayerId owner,
        string label) =>
        new(
            SelectionTargetKind.Permanent,
            $"permanent:{permanent.Value}",
            owner,
            Permanent: permanent,
            Label: label,
            Zone: Zone.BattleArea);
}
