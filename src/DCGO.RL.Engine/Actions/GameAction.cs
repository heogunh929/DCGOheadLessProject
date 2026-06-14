using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Actions;

public sealed record GameActionMetadata
{
    public static GameActionMetadata Empty { get; } = new();

    public string StableId { get; init; } = string.Empty;
    public string DebugLabel { get; init; } = string.Empty;
    public IReadOnlyDictionary<string, string> Tags { get; init; } = new Dictionary<string, string>(StringComparer.Ordinal);
}

public abstract record GameAction(PlayerId Actor)
{
    public GameActionMetadata Metadata { get; init; } = GameActionMetadata.Empty;
    public string DebugLabel => string.IsNullOrWhiteSpace(Metadata.DebugLabel)
        ? GetType().Name
        : Metadata.DebugLabel;
}

public sealed record HatchAction(PlayerId Actor) : GameAction(Actor);

public sealed record MoveFromBreedingAction(
    PlayerId Actor,
    PermanentId Permanent,
    int TargetFrameIndex) : GameAction(Actor);

public sealed record PlayCardAction(
    PlayerId Actor,
    CardInstanceId Card,
    int TargetFrameIndex) : GameAction(Actor);

public sealed record DigivolveAction(
    PlayerId Actor,
    CardInstanceId Card,
    PermanentId TargetPermanent) : GameAction(Actor);

public sealed record JogressAction(
    PlayerId Actor,
    CardInstanceId Card,
    IReadOnlyList<PermanentId> SourcePermanents) : GameAction(Actor);

public sealed record BurstDigivolveAction(
    PlayerId Actor,
    CardInstanceId Card,
    PermanentId TargetPermanent,
    PermanentId BurstTamer) : GameAction(Actor);

public sealed record AppFusionAction(
    PlayerId Actor,
    CardInstanceId Card,
    PermanentId TargetPermanent,
    CardInstanceId LinkCard) : GameAction(Actor);

public sealed record DigiXrosPlayAction(
    PlayerId Actor,
    CardInstanceId Card,
    int TargetFrameIndex,
    IReadOnlyList<CardInstanceId> Materials) : GameAction(Actor);

public sealed record AssemblyPlayAction(
    PlayerId Actor,
    CardInstanceId Card,
    int TargetFrameIndex,
    IReadOnlyList<CardInstanceId> Materials) : GameAction(Actor);

public sealed record LinkAction(
    PlayerId Actor,
    CardInstanceId LinkCard,
    PermanentId TargetPermanent) : GameAction(Actor);

public sealed record DelayOptionPlayAction(
    PlayerId Actor,
    CardInstanceId Card,
    int TargetFrameIndex) : GameAction(Actor);

public sealed record AttackAction(
    PlayerId Actor,
    PermanentId Attacker,
    PermanentId? Defender) : GameAction(Actor);

public sealed record PassAction(PlayerId Actor) : GameAction(Actor);

public sealed record SelectionAction(
    PlayerId Actor,
    SelectionResult Result) : GameAction(Actor);
