using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Mechanics;

public sealed class CostResolver
{
    public CostCandidate ResolveNormalPlay(GameState state, CardInstanceId card)
    {
        var definition = Battle.BattleRules.Definition(state, card);
        return new CostCandidate(Mechanic.Normal, Math.Max(0, definition.PlayCost), 0);
    }

    public CostCandidate ResolveNormalDigivolve(GameState state, CardInstanceId card, PermanentState target)
    {
        if (!Battle.BattleRules.CanDigivolve(state, card, target, out var cost))
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

    public CostCandidate ResolveLink(PlayRequirement requirement) =>
        new(Mechanic.Link, requirement.LinkCost, 0);
}
