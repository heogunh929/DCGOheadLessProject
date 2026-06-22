using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Mechanics;

namespace DCGO.RL.Engine.Battle;

public sealed class LegalActionGenerator
{
    private readonly ComplexMechanicService _complexMechanicService;
    private readonly BattleKeywordService _keywordService;
    private readonly EffectiveStatService _effectiveStats;
    private readonly StaticRequirementService? _staticRequirements;
    private readonly StaticEffectService? _staticEffects;

    public LegalActionGenerator(
        ComplexMechanicService? complexMechanicService = null,
        BattleKeywordService? keywordService = null,
        EffectiveStatService? effectiveStats = null,
        StaticRequirementService? staticRequirements = null,
        StaticEffectService? staticEffects = null)
    {
        _effectiveStats = effectiveStats ?? EffectiveStatService.NoContinuous;
        _staticEffects = staticEffects;
        _keywordService = keywordService ?? new BattleKeywordService(_effectiveStats, _staticEffects);
        _staticRequirements = staticRequirements;
        _complexMechanicService = complexMechanicService
            ?? new ComplexMechanicService(staticRequirementService: staticRequirements);
    }

    internal ComplexMechanicService RuntimeComplexMechanicService => _complexMechanicService;

    internal StaticRequirementService? RuntimeStaticRequirementService => _staticRequirements;

    internal StaticEffectService? RuntimeStaticEffectService => _staticEffects;

    public IReadOnlyList<LegalAction> Generate(GameState state, PlayerId playerId)
    {
        if (state.IsGameOver || state.TurnPlayerId != playerId)
        {
            return Array.Empty<LegalAction>();
        }

        return state.Phase switch
        {
            Phase.Breeding => GenerateBreedingActions(state, playerId),
            Phase.Main => GenerateMainActions(state, playerId),
            _ => Array.Empty<LegalAction>(),
        };
    }

    private static IReadOnlyList<LegalAction> GenerateBreedingActions(GameState state, PlayerId playerId)
    {
        var player = state.GetPlayer(playerId);
        var actions = new List<LegalAction>();
        if (player.DigiEggDeck.Count > 0 && player.BreedingAreaPermanent is null)
        {
            actions.Add(new LegalAction(
                $"hatch:{playerId.Value}",
                LegalActionKind.Hatch,
                new HatchAction(playerId),
                "Hatch",
                Array.Empty<SelectableTarget>()));
        }

        var breeding = player.BreedingAreaPermanent;
        if (breeding is not null && BattleRules.CanMoveFromBreeding(state, breeding))
        {
            var frame = BattleRules.FirstEmptyBattleFrame(player, state.Config.FieldSlotCount);
            actions.Add(new LegalAction(
                $"move-from-breeding:{breeding.Id.Value}:{frame}",
                LegalActionKind.MoveFromBreeding,
                new MoveFromBreedingAction(playerId, breeding.Id, frame),
                "Move from breeding",
                new[] { PermanentTarget(breeding, playerId) }));
        }

        return actions;
    }

    private IReadOnlyList<LegalAction> GenerateMainActions(GameState state, PlayerId playerId)
    {
        var player = state.GetPlayer(playerId);
        var actions = new List<LegalAction>();

        foreach (var card in player.Hand)
        {
            var definition = BattleRules.Definition(state, card);
            if (definition.PlayCost >= 0)
            {
                if (definition.CardKinds.Contains(CardKind.Option))
                {
                    if (BattleRules.MatchesOptionColorRequirement(state, playerId, card, _staticEffects)
                        && _staticEffects?.HasCardRestriction(state, card, StaticCardRestrictionKind.CannotPlay) != true)
                    {
                        actions.Add(new LegalAction(
                            $"play-option:{card.Value}",
                            LegalActionKind.PlayCard,
                            new PlayCardAction(playerId, card, -1),
                            $"Use {definition.CardId}",
                            new[] { CardTarget(card, playerId, definition.CardId) }));
                    }
                }
                else if (definition.CardKinds.Contains(CardKind.Digimon) || definition.CardKinds.Contains(CardKind.Tamer))
                {
                    if (_staticEffects?.HasCardRestriction(
                        state,
                        card,
                        StaticCardRestrictionKind.CannotPutField,
                        new StaticCardRestrictionCause(
                            EffectSourceCardId: null,
                            EffectSourcePermanentId: null,
                            ControllerPlayerId: playerId,
                            MoveReason: MoveReason.Play)) != true)
                    {
                        foreach (var frame in EmptyFrames(player, state.Config.FieldSlotCount))
                        {
                            actions.Add(new LegalAction(
                                $"play:{card.Value}:{frame}",
                                LegalActionKind.PlayCard,
                                new PlayCardAction(playerId, card, frame),
                                $"Play {definition.CardId}",
                                new[] { CardTarget(card, playerId, definition.CardId) }));
                        }
                    }
                }
            }

            if (definition.CardKinds.Contains(CardKind.Digimon))
            {
                foreach (var permanent in player.FieldPermanents)
                {
                    if (BattleRules.CanDigivolve(state, card, permanent, out _, _staticRequirements, _staticEffects))
                    {
                        actions.Add(new LegalAction(
                            $"digivolve:{card.Value}:{permanent.Id.Value}",
                            LegalActionKind.Digivolve,
                            new DigivolveAction(playerId, card, permanent.Id),
                            $"Digivolve {definition.CardId}",
                            new[] { PermanentTarget(permanent, playerId) }));
                    }
                }
            }
        }

        actions.AddRange(_complexMechanicService.GenerateActions(state, playerId));

        foreach (var attacker in player.BattleAreaPermanents.Where(permanent => BattleRules.CanAttack(state, permanent, _keywordService, _effectiveStats, _staticEffects)))
        {
            actions.Add(new LegalAction(
                $"attack-security:{attacker.Id.Value}",
                LegalActionKind.Attack,
                new AttackAction(playerId, attacker.Id, null),
                "Attack security",
                new[] { PermanentTarget(attacker, playerId) }));

            foreach (var defender in state.Players
                .Where(candidate => candidate.Id != playerId)
                .SelectMany(candidate => candidate.BattleAreaPermanents)
                .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId)))
            {
                actions.Add(new LegalAction(
                    $"attack:{attacker.Id.Value}:{defender.Id.Value}",
                    LegalActionKind.Attack,
                    new AttackAction(playerId, attacker.Id, defender.Id),
                    "Attack Digimon",
                    new[] { PermanentTarget(defender, defender.ControllerPlayerId) }));
            }
        }

        actions.Add(new LegalAction(
            $"pass:{playerId.Value}",
            LegalActionKind.Pass,
            new PassAction(playerId),
            "Pass",
            Array.Empty<SelectableTarget>()));

        return actions;
    }

    private static IEnumerable<int> EmptyFrames(PlayerState player, int fieldSlotCount)
    {
        for (var i = 0; i < fieldSlotCount; i++)
        {
            if (BattleRules.IsEmptyBattleFrame(player, i))
            {
                yield return i;
            }
        }
    }

    private static SelectableTarget CardTarget(CardInstanceId card, PlayerId owner, string label) =>
        new(SelectionTargetKind.Card, $"card:{card.Value}", owner, Card: card, Label: label);

    private static SelectableTarget PermanentTarget(PermanentState permanent, PlayerId owner) =>
        new(SelectionTargetKind.Permanent, $"permanent:{permanent.Id.Value}", owner, Permanent: permanent.Id, Label: $"Permanent {permanent.Id.Value}");
}
