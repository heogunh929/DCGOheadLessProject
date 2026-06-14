using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Mechanics;

public sealed record MaterialCandidate(
    CardInstanceId Card,
    PlayerId Owner,
    Zone Zone,
    PermanentId? Permanent,
    string Label);

public sealed record EvolutionCandidate(
    EvolutionMode Mode,
    CardInstanceId Card,
    IReadOnlyList<PermanentId> SourcePermanents,
    PermanentId? TargetPermanent,
    PermanentId? BurstTamer,
    CardInstanceId? AppFusionLinkCard,
    int Cost);

public sealed record PlayCandidate(
    PlayMode Mode,
    CardInstanceId Card,
    int TargetFrameIndex,
    IReadOnlyList<CardInstanceId> Materials,
    int Cost);

public sealed record CostCandidate(
    Mechanic Mechanic,
    int BaseCost,
    int Reduction,
    int? FixedCost = null)
{
    public int FinalCost => Math.Max(0, (FixedCost ?? BaseCost) - Reduction);
}
