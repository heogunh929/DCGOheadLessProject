using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.Mechanics;

public sealed class CostResolver
{
    private readonly StaticEffectService? _staticEffects;
    private readonly StaticRequirementService? _staticRequirements;

    public CostResolver(
        StaticEffectService? staticEffects = null,
        StaticRequirementService? staticRequirements = null)
    {
        _staticEffects = staticEffects;
        _staticRequirements = staticRequirements;
    }

    public CostCandidate ResolveNormalPlay(GameState state, CardInstanceId card)
    {
        var definition = Battle.BattleRules.Definition(state, card);
        var cost = _staticEffects?.ApplyCostModifiers(
            state,
            card,
            Math.Max(0, definition.PlayCost),
            StaticCostKind.Play)
            ?? Math.Max(0, definition.PlayCost);
        return new CostCandidate(Mechanic.Normal, cost, 0);
    }

    public CostCandidate ResolveNormalDigivolve(GameState state, CardInstanceId card, PermanentState target)
    {
        if (!Battle.BattleRules.CanDigivolve(state, card, target, out var cost, _staticRequirements, _staticEffects))
        {
            throw new DomainException($"Card '{card}' cannot digivolve onto permanent '{target.Id}'.");
        }

        return new CostCandidate(Mechanic.Normal, cost, 0);
    }

    public CostCandidate ResolveJogress(EvolutionRequirement requirement) =>
        new(Mechanic.Jogress, requirement.Cost, 0);

    public CostCandidate ResolveBurst(EvolutionRequirement requirement) =>
        new(Mechanic.BurstDigivolution, requirement.Cost, 0);

    public CostCandidate ResolveAppFusion(EvolutionRequirement requirement) =>
        new(Mechanic.AppFusion, requirement.Cost, 0);

    public CostCandidate ResolveDigiXros(GameState state, CardInstanceId card, PlayRequirement requirement, int selectedMaterialCount)
    {
        var baseCost = requirement.FixedCost ?? Math.Max(0, Battle.BattleRules.Definition(state, card).PlayCost);
        var cappedCount = requirement.MaxMaterials <= 0
            ? selectedMaterialCount
            : Math.Min(selectedMaterialCount, requirement.MaxMaterials);
        return new CostCandidate(Mechanic.DigiXros, baseCost, cappedCount * requirement.ReduceCostPerMaterial, requirement.FixedCost);
    }

    public CostCandidate ResolveAssembly(GameState state, CardInstanceId card, PlayRequirement requirement, int selectedMaterialCount)
    {
        var baseCost = requirement.FixedCost ?? Math.Max(0, Battle.BattleRules.Definition(state, card).PlayCost);
        var requiredCount = requirement.Materials.Sum(material => Math.Max(1, material.Count));
        var reduction = selectedMaterialCount >= requiredCount ? requirement.ReduceCost : 0;
        return new CostCandidate(Mechanic.Assembly, baseCost, reduction, requirement.FixedCost);
    }

    public CostCandidate ResolveLink(GameState state, CardInstanceId card, PermanentState target, PlayRequirement requirement) =>
        ResolveLink(state, card, target, requirement.LinkCost);

    public CostCandidate ResolveLink(GameState state, CardInstanceId card, PermanentState target, int baseCost)
    {
        var normalizedBaseCost = Math.Max(0, baseCost);
        var cost = _staticEffects?.ApplyCostModifiers(
            state,
            card,
            normalizedBaseCost,
            StaticCostKind.Link,
            target,
            Mechanic.Link)
            ?? normalizedBaseCost;
        return new CostCandidate(Mechanic.Link, cost, 0);
    }
}
