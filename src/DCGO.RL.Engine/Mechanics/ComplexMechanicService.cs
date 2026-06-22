using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Setup;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Mechanics;

public sealed class ComplexMechanicService
{
    private readonly IZoneMover _zoneMover;
    private readonly DrawService _drawService;
    private readonly ComplexMechanicMatcher _matcher;
    private readonly CostResolver _costResolver;
    private readonly StaticRequirementService? _staticRequirements;
    private readonly StaticEffectService? _staticEffects;

    public ComplexMechanicService(
        IZoneMover? zoneMover = null,
        DrawService? drawService = null,
        ComplexMechanicMatcher? matcher = null,
        CostResolver? costResolver = null,
        StaticRequirementService? staticRequirementService = null,
        StaticEffectService? staticEffectService = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
        _drawService = drawService ?? new DrawService(_zoneMover);
        _matcher = matcher ?? new ComplexMechanicMatcher();
        _costResolver = costResolver ?? new CostResolver(staticEffectService, staticRequirementService);
        _staticRequirements = staticRequirementService;
        _staticEffects = staticEffectService;
    }

    internal StaticRequirementService? RuntimeStaticRequirementService => _staticRequirements;
    internal StaticEffectService? RuntimeStaticEffectService => _staticEffects;

    public IReadOnlyList<LegalAction> GenerateActions(GameState state, PlayerId playerId)
    {
        var player = state.GetPlayer(playerId);
        var actions = new List<LegalAction>();

        foreach (var card in player.Hand)
        {
            var definition = BattleRules.Definition(state, card);
            actions.AddRange(GenerateEvolutionActions(state, player, card, definition));
            actions.AddRange(GeneratePlayActions(state, player, card, definition));
        }

        actions.AddRange(GenerateLinkActionsFromBattleArea(state, player));
        return actions;
    }

    public PermanentState ExecuteJogress(GameState state, JogressAction action, GameTrace? trace = null)
    {
        var requirement = RequireEvolutionRequirement(state, action.Card, EvolutionMode.Jogress);
        if (action.SourcePermanents.Count != 2)
        {
            throw new DomainException("Jogress requires exactly two source permanents.");
        }

        var player = state.GetPlayer(action.Actor);
        if (!player.Hand.Contains(action.Card))
        {
            throw new DomainException($"Jogress card '{action.Card}' is not in player '{action.Actor}' hand.");
        }

        var sources = action.SourcePermanents.Select(id => BattleRules.Permanent(state, id)).ToArray();
        if (sources.Any(permanent => permanent.ControllerPlayerId != action.Actor || permanent.IsBreedingArea))
        {
            throw new DomainException("Jogress sources must be actor-controlled battle area permanents.");
        }

        ValidateMaterialPermanents(state, sources, requirement.Materials);
        PayCost(state, action.Actor, _costResolver.ResolveJogress(requirement).FinalCost);

        var targetFrame = sources[0].FrameIndex;
        var materialCards = sources.SelectMany(permanent => permanent.StackCardIds).ToArray();
        var linkedCards = sources.SelectMany(permanent => permanent.LinkedCards).ToArray();

        foreach (var linked in linkedCards)
        {
            MoveFromCurrentZone(state, linked, Zone.Trash, MoveReason.Trash);
        }

        foreach (var material in materialCards)
        {
            MoveFromCurrentZone(state, material, Zone.OutsideGame, MoveReason.ComplexMechanic);
        }

        var newPermanentId = new PermanentId(BattleRules.NextPermanentId(state));
        _zoneMover.MoveCard(
            state,
            new MoveCardCommand(
                action.Card,
                Zone.Hand,
                Zone.BattleArea,
                MoveReason.Digivolve,
                DestinationPermanent: newPermanentId,
                DestinationFrameIndex: targetFrame));

        var result = BattleRules.Permanent(state, newPermanentId);
        result.EnterFieldTurnCount = -1;

        foreach (var material in materialCards)
        {
            _zoneMover.MoveCard(
                state,
                new MoveCardCommand(
                    material,
                    Zone.OutsideGame,
                    Zone.EvolutionSources,
                    MoveReason.Digivolve,
                    DestinationPermanent: result.Id,
                    ToTop: false));
        }

        _drawService.DrawCards(state, action.Actor, 1, trace);
        return result;
    }

    public PermanentState ExecuteBurstDigivolve(GameState state, BurstDigivolveAction action, GameTrace? trace = null)
    {
        var requirement = RequireEvolutionRequirement(state, action.Card, EvolutionMode.BurstDigivolution);
        var target = BattleRules.Permanent(state, action.TargetPermanent);
        var tamer = BattleRules.Permanent(state, action.BurstTamer);

        if (target.ControllerPlayerId != action.Actor || tamer.ControllerPlayerId != action.Actor)
        {
            throw new DomainException("Burst target and tamer must be actor-controlled.");
        }

        if (!_matcher.MatchesPermanent(state, target, requirement.TargetRequirement)
            || !_matcher.MatchesPermanent(state, tamer, requirement.BurstTamerRequirement))
        {
            throw new DomainException("Burst digivolution requirements are not satisfied.");
        }

        if (tamer.SourceCardIds.Count > 0 || tamer.LinkedCards.Count > 0)
        {
            throw new UnsupportedMechanicException("Burst tamer bounce with stack or linked cards");
        }

        PayCost(state, action.Actor, _costResolver.ResolveBurst(requirement).FinalCost);
        _zoneMover.MoveCard(state, new MoveCardCommand(tamer.TopCardId, Zone.BattleArea, Zone.Hand, MoveReason.ComplexMechanic, SourcePermanent: tamer.Id));
        _zoneMover.DigivolveCard(state, new DigivolveCardCommand(action.Card, Zone.Hand, action.TargetPermanent, MoveReason.Digivolve));
        target.IsBurstDigivolved = true;
        _drawService.DrawCards(state, action.Actor, 1, trace);
        return target;
    }

    public PermanentState ExecuteAppFusion(GameState state, AppFusionAction action, GameTrace? trace = null)
    {
        var requirement = RequireEvolutionRequirement(state, action.Card, EvolutionMode.AppFusion);
        var target = BattleRules.Permanent(state, action.TargetPermanent);
        if (target.ControllerPlayerId != action.Actor)
        {
            throw new DomainException("App Fusion target must be actor-controlled.");
        }

        if (!target.LinkedCards.Contains(action.LinkCard))
        {
            throw new DomainException($"Card '{action.LinkCard}' is not linked to permanent '{target.Id}'.");
        }

        if (!_matcher.MatchesPermanent(state, target, requirement.TargetRequirement)
            || !_matcher.MatchesCard(state, action.LinkCard, requirement.AppFusionLinkRequirement))
        {
            throw new DomainException("App Fusion requirements are not satisfied.");
        }

        PayCost(state, action.Actor, _costResolver.ResolveAppFusion(requirement).FinalCost);
        _zoneMover.MoveCard(
            state,
            new MoveCardCommand(
                action.LinkCard,
                Zone.LinkedCards,
                Zone.EvolutionSources,
                MoveReason.Link,
                SourcePermanent: target.Id,
                DestinationPermanent: target.Id,
                ToTop: true));
        _zoneMover.DigivolveCard(state, new DigivolveCardCommand(action.Card, Zone.Hand, action.TargetPermanent, MoveReason.Digivolve));
        target.IsAppFusion = true;
        _drawService.DrawCards(state, action.Actor, 1, trace);
        return target;
    }

    public PermanentState ExecuteDigiXrosPlay(GameState state, DigiXrosPlayAction action)
    {
        var requirement = RequirePlayRequirement(state, action.Card, PlayMode.DigiXros);
        var player = state.GetPlayer(action.Actor);
        if (!player.Hand.Contains(action.Card))
        {
            throw new DomainException($"DigiXros card '{action.Card}' is not in hand.");
        }

        ValidateMaterialCards(state, action.Actor, action.Materials, requirement.Materials);
        ThrowIfCannotPutFieldFromHand(state, action.Actor, action.Card);
        PayCost(state, action.Actor, _costResolver.ResolveDigiXros(state, action.Card, requirement, action.Materials.Count).FinalCost);

        var permanent = PlayPermanentFromHand(state, action.Actor, action.Card, action.TargetFrameIndex, MoveReason.Play);
        MoveMaterialsToSources(state, permanent.Id, action.Materials);
        return permanent;
    }

    public PermanentState ExecuteAssemblyPlay(GameState state, AssemblyPlayAction action)
    {
        var requirement = RequirePlayRequirement(state, action.Card, PlayMode.Assembly);
        var player = state.GetPlayer(action.Actor);
        if (!player.Hand.Contains(action.Card))
        {
            throw new DomainException($"Assembly card '{action.Card}' is not in hand.");
        }

        ValidateMaterialCards(state, action.Actor, action.Materials, requirement.Materials);
        ThrowIfCannotPutFieldFromHand(state, action.Actor, action.Card);
        PayCost(state, action.Actor, _costResolver.ResolveAssembly(state, action.Card, requirement, action.Materials.Count).FinalCost);

        var permanent = PlayPermanentFromHand(state, action.Actor, action.Card, action.TargetFrameIndex, MoveReason.Play);
        MoveMaterialsToSources(state, permanent.Id, action.Materials);
        return permanent;
    }

    public void ExecuteLink(GameState state, LinkAction action)
    {
        var requirement = FindLinkPlayRequirement(state, action.LinkCard);
        var target = BattleRules.Permanent(state, action.TargetPermanent);
        if (target.ControllerPlayerId != action.Actor || !BattleRules.IsDigimon(state, target.TopCardId))
        {
            throw new DomainException("Link target must be an actor-controlled Digimon.");
        }

        var staticRequirement = _staticRequirements?.FirstLinkRequirement(state, action.LinkCard, target, _staticEffects);
        var definitionRequirementMatches = requirement is not null
            && _matcher.MatchesPermanent(state, target, requirement.LinkTargetRequirement);
        if (!definitionRequirementMatches && staticRequirement is null)
        {
            throw new DomainException("Link target requirement is not satisfied.");
        }

        if (target.LinkedCards.Count >= LinkMax(state, target))
        {
            throw new DomainException($"Permanent '{target.Id}' has no open linked card slots.");
        }

        var baseCost = definitionRequirementMatches
            ? requirement!.LinkCost
            : staticRequirement!.Cost;
        var cost = _costResolver.ResolveLink(state, action.LinkCard, target, baseCost).FinalCost;
        PayCost(state, action.Actor, cost);
        MoveFromCurrentZone(state, action.LinkCard, Zone.LinkedCards, MoveReason.Link, target.Id);
    }

    public PermanentState ExecuteDelayOptionPlay(GameState state, DelayOptionPlayAction action)
    {
        var requirement = RequirePlayRequirement(state, action.Card, PlayMode.DelayOption);
        var player = state.GetPlayer(action.Actor);
        if (!player.Hand.Contains(action.Card))
        {
            throw new DomainException($"Delay option '{action.Card}' is not in hand.");
        }

        var definition = BattleRules.Definition(state, action.Card);
        if (!definition.CardKinds.Contains(CardKind.Option))
        {
            throw new DomainException("Delay option play requires an Option card.");
        }

        ThrowIfCannotPutFieldFromHand(state, action.Actor, action.Card);
        PayCost(state, action.Actor, requirement.FixedCost ?? Math.Max(0, definition.PlayCost));
        var permanent = PlayPermanentFromHand(state, action.Actor, action.Card, action.TargetFrameIndex, MoveReason.Play);
        permanent.IsDelayOption = true;
        return permanent;
    }

    public void ApplyAceOverflow(GameState state, IEnumerable<CardInstanceId> cards)
    {
        foreach (var card in cards)
        {
            var definition = BattleRules.Definition(state, card);
            if (definition.OverflowMemory > 0)
            {
                var owner = state.Cards[card].Owner;
                var delta = owner == state.TurnPlayerId ? -definition.OverflowMemory : definition.OverflowMemory;
                state.Memory = Math.Clamp(state.Memory + delta, -10, 10);
            }
        }
    }

    private IEnumerable<LegalAction> GenerateEvolutionActions(GameState state, PlayerState player, CardInstanceId card, CardDefinition definition)
    {
        foreach (var requirement in definition.EvolutionRequirements)
        {
            switch (requirement.Mode)
            {
                case EvolutionMode.Jogress:
                    foreach (var pair in JogressPairs(state, player, requirement))
                    {
                        var cost = _costResolver.ResolveJogress(requirement).FinalCost;
                        yield return new LegalAction(
                            $"jogress:{card.Value}:{pair[0].Id.Value}:{pair[1].Id.Value}",
                            LegalActionKind.Jogress,
                            new JogressAction(player.Id, card, pair.Select(permanent => permanent.Id).ToArray()),
                            $"Jogress {definition.CardId}",
                            pair.Select(permanent => PermanentTarget(state, permanent)).ToArray());
                    }
                    break;

                case EvolutionMode.BurstDigivolution:
                    foreach (var target in player.BattleAreaPermanents.Where(permanent => _matcher.MatchesPermanent(state, permanent, requirement.TargetRequirement)))
                    {
                        foreach (var tamer in player.BattleAreaPermanents.Where(permanent => _matcher.MatchesPermanent(state, permanent, requirement.BurstTamerRequirement)))
                        {
                            yield return new LegalAction(
                                $"burst:{card.Value}:{target.Id.Value}:{tamer.Id.Value}",
                                LegalActionKind.BurstDigivolve,
                                new BurstDigivolveAction(player.Id, card, target.Id, tamer.Id),
                                $"Burst Digivolve {definition.CardId}",
                                new[] { PermanentTarget(state, target), PermanentTarget(state, tamer) });
                        }
                    }
                    break;

                case EvolutionMode.AppFusion:
                    foreach (var target in player.BattleAreaPermanents.Where(permanent => _matcher.MatchesPermanent(state, permanent, requirement.TargetRequirement)))
                    {
                        foreach (var linked in target.LinkedCards.Where(link => _matcher.MatchesCard(state, link, requirement.AppFusionLinkRequirement)))
                        {
                            yield return new LegalAction(
                                $"app-fusion:{card.Value}:{target.Id.Value}:{linked.Value}",
                                LegalActionKind.AppFusion,
                                new AppFusionAction(player.Id, card, target.Id, linked),
                                $"App Fusion {definition.CardId}",
                                new[] { PermanentTarget(state, target), CardTarget(state, linked) });
                        }
                    }
                    break;
            }
        }
    }

    private IEnumerable<LegalAction> GeneratePlayActions(GameState state, PlayerState player, CardInstanceId card, CardDefinition definition)
    {
        foreach (var requirement in definition.PlayRequirements)
        {
            switch (requirement.Mode)
            {
                case PlayMode.DigiXros:
                    if (!CanPutFieldFromHand(state, player.Id, card))
                    {
                        break;
                    }

                    foreach (var frame in EmptyFrames(player, state.Config.FieldSlotCount))
                    {
                        var materials = PickMaterials(state, player.Id, requirement.Materials, allowPartial: true);
                        if (materials.Count > 0)
                        {
                            yield return new LegalAction(
                                $"digixros:{card.Value}:{frame}:{string.Join(".", materials.Select(material => material.Value))}",
                                LegalActionKind.DigiXrosPlay,
                                new DigiXrosPlayAction(player.Id, card, frame, materials),
                                $"DigiXros {definition.CardId}",
                                materials.Select(material => CardTarget(state, material)).ToArray());
                        }
                    }
                    break;

                case PlayMode.Assembly:
                    if (!CanPutFieldFromHand(state, player.Id, card))
                    {
                        break;
                    }

                    foreach (var frame in EmptyFrames(player, state.Config.FieldSlotCount))
                    {
                        var materials = PickMaterials(state, player.Id, requirement.Materials, allowPartial: false);
                        if (materials.Count == requirement.Materials.Sum(material => Math.Max(1, material.Count)))
                        {
                            yield return new LegalAction(
                                $"assembly:{card.Value}:{frame}:{string.Join(".", materials.Select(material => material.Value))}",
                                LegalActionKind.AssemblyPlay,
                                new AssemblyPlayAction(player.Id, card, frame, materials),
                                $"Assembly {definition.CardId}",
                                materials.Select(material => CardTarget(state, material)).ToArray());
                        }
                    }
                    break;

                case PlayMode.DelayOption:
                    if (!CanPutFieldFromHand(state, player.Id, card))
                    {
                        break;
                    }

                    foreach (var frame in EmptyFrames(player, state.Config.FieldSlotCount))
                    {
                        yield return new LegalAction(
                            $"delay-option:{card.Value}:{frame}",
                            LegalActionKind.DelayOption,
                            new DelayOptionPlayAction(player.Id, card, frame),
                            $"Place Delay {definition.CardId}",
                            new[] { CardTarget(state, card) });
                    }
                    break;
            }

            if (requirement.LinkTargetRequirement is not null)
            {
                foreach (var target in player.BattleAreaPermanents.Where(permanent => permanent.TopCardId != card && _matcher.MatchesPermanent(state, permanent, requirement.LinkTargetRequirement)))
                {
                    if (target.LinkedCards.Count < LinkMax(state, target))
                    {
                        yield return new LegalAction(
                            $"link:{card.Value}:{target.Id.Value}",
                            LegalActionKind.Link,
                            new LinkAction(player.Id, card, target.Id),
                            $"Link {definition.CardId}",
                            new[] { CardTarget(state, card), PermanentTarget(state, target) });
                    }
                }
            }
        }

        foreach (var action in GenerateStaticLinkActions(state, player, card, sourcePermanent: null))
        {
            yield return action;
        }
    }

    private bool CanPutFieldFromHand(GameState state, PlayerId player, CardInstanceId card) =>
        _staticEffects?.HasCardRestriction(
            state,
            card,
            StaticCardRestrictionKind.CannotPutField,
            new StaticCardRestrictionCause(
                EffectSourceCardId: null,
                EffectSourcePermanentId: null,
                ControllerPlayerId: player,
                MoveReason: MoveReason.Play)) != true;

    private void ThrowIfCannotPutFieldFromHand(GameState state, PlayerId player, CardInstanceId card)
    {
        if (!CanPutFieldFromHand(state, player, card))
        {
            throw new DomainException($"Permanent card '{card}' cannot be played by a static effect.");
        }
    }

    private IEnumerable<LegalAction> GenerateLinkActionsFromBattleArea(GameState state, PlayerState player)
    {
        foreach (var sourcePermanent in player.BattleAreaPermanents)
        {
            var card = sourcePermanent.TopCardId;
            var definition = BattleRules.Definition(state, card);
            foreach (var requirement in definition.PlayRequirements.Where(requirement => requirement.LinkTargetRequirement is not null))
            {
                foreach (var target in player.BattleAreaPermanents.Where(permanent => permanent.Id != sourcePermanent.Id && _matcher.MatchesPermanent(state, permanent, requirement.LinkTargetRequirement)))
                {
                    if (target.LinkedCards.Count < LinkMax(state, target))
                    {
                        yield return new LegalAction(
                            $"link-field:{card.Value}:{target.Id.Value}",
                            LegalActionKind.Link,
                            new LinkAction(player.Id, card, target.Id),
                            $"Link {definition.CardId}",
                            new[] { CardTarget(state, card), PermanentTarget(state, target) });
                    }
                }
            }

            foreach (var action in GenerateStaticLinkActions(state, player, card, sourcePermanent))
            {
                yield return action;
            }
        }
    }

    private IEnumerable<LegalAction> GenerateStaticLinkActions(
        GameState state,
        PlayerState player,
        CardInstanceId card,
        PermanentState? sourcePermanent)
    {
        if (_staticRequirements is null)
        {
            yield break;
        }

        foreach (var target in player.BattleAreaPermanents
            .Where(permanent => sourcePermanent is null || permanent.Id != sourcePermanent.Id)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => permanent.LinkedCards.Count < LinkMax(state, permanent)))
        {
            foreach (var evaluation in _staticRequirements.EvaluateLinkRequirements(state, card, target, _staticEffects))
            {
                var definition = BattleRules.Definition(state, card);
                var stableId = StableActionId(evaluation.Descriptor.StableId);
                yield return new LegalAction(
                    sourcePermanent is null
                        ? $"link-static:{card.Value}:{target.Id.Value}:{stableId}"
                        : $"link-static-field:{card.Value}:{target.Id.Value}:{stableId}",
                    LegalActionKind.Link,
                    new LinkAction(player.Id, card, target.Id)
                    {
                        Metadata = new GameActionMetadata
                        {
                            StableId = evaluation.Descriptor.StableId,
                            DebugLabel = evaluation.Descriptor.DebugLabel,
                            Tags = new Dictionary<string, string>(StringComparer.Ordinal)
                            {
                                ["staticRequirement"] = "link",
                            },
                        },
                    },
                    $"Link {definition.CardId}",
                    new[] { CardTarget(state, card), PermanentTarget(state, target) });
            }
        }
    }

    private IReadOnlyList<CardInstanceId> PickMaterials(GameState state, PlayerId player, IReadOnlyList<MaterialRequirement> requirements, bool allowPartial)
    {
        var selected = new List<CardInstanceId>();
        foreach (var requirement in requirements)
        {
            var needed = Math.Max(1, requirement.Count);
            var candidates = _matcher.FindMaterialCandidates(state, player, requirement)
                .Where(candidate => !selected.Contains(candidate.Card))
                .Take(needed)
                .Select(candidate => candidate.Card)
                .ToArray();

            if (!allowPartial && candidates.Length < needed)
            {
                return Array.Empty<CardInstanceId>();
            }

            selected.AddRange(candidates);
        }

        return selected;
    }

    private static IEnumerable<PermanentState[]> JogressPairs(GameState state, PlayerState player, EvolutionRequirement requirement)
    {
        var materials = requirement.Materials;
        if (materials.Count != 2)
        {
            yield break;
        }

        var first = player.BattleAreaPermanents.Where(permanent => new ComplexMechanicMatcher().MatchesPermanent(state, permanent, materials[0])).ToArray();
        var second = player.BattleAreaPermanents.Where(permanent => new ComplexMechanicMatcher().MatchesPermanent(state, permanent, materials[1])).ToArray();

        foreach (var left in first)
        {
            foreach (var right in second)
            {
                if (left.Id != right.Id)
                {
                    yield return new[] { left, right };
                }
            }
        }
    }

    private void ValidateMaterialPermanents(GameState state, IReadOnlyList<PermanentState> permanents, IReadOnlyList<MaterialRequirement> requirements)
    {
        if (requirements.Count != permanents.Count)
        {
            throw new DomainException($"Material count mismatch. Required '{requirements.Count}', actual '{permanents.Count}'.");
        }

        for (var i = 0; i < requirements.Count; i++)
        {
            if (!_matcher.MatchesPermanent(state, permanents[i], requirements[i]))
            {
                throw new DomainException($"Permanent '{permanents[i].Id}' does not satisfy material requirement '{i}'.");
            }
        }
    }

    private void ValidateMaterialCards(GameState state, PlayerId player, IReadOnlyList<CardInstanceId> cards, IReadOnlyList<MaterialRequirement> requirements)
    {
        var remaining = cards.ToList();
        foreach (var requirement in requirements)
        {
            var needed = Math.Max(1, requirement.Count);
            for (var i = 0; i < needed; i++)
            {
                var match = remaining.FirstOrDefault(card => state.Cards[card].Owner == player && _matcher.MatchesCard(state, card, requirement));
                if (match.Value == 0)
                {
                    throw new DomainException($"Material requirement '{requirement.Label}' is not satisfied.");
                }

                remaining.Remove(match);
            }
        }
    }

    private PermanentState PlayPermanentFromHand(GameState state, PlayerId player, CardInstanceId card, int frame, MoveReason reason)
    {
        var definition = BattleRules.Definition(state, card);
        if (!definition.IsPermanent && !definition.CardKinds.Contains(CardKind.Option))
        {
            throw new UnsupportedMechanicException($"Playing card kind '{string.Join(",", definition.CardKinds)}'");
        }

        if (!BattleRules.IsEmptyBattleFrame(state.GetPlayer(player), frame))
        {
            throw new DomainException($"Battle frame '{frame}' is not empty.");
        }

        var permanentId = new PermanentId(BattleRules.NextPermanentId(state));
        _zoneMover.MoveCard(
            state,
            new MoveCardCommand(
                card,
                Zone.Hand,
                Zone.BattleArea,
                reason,
                DestinationPermanent: permanentId,
                DestinationFrameIndex: frame));

        var permanent = BattleRules.Permanent(state, permanentId);
        permanent.EnterFieldTurnCount = state.TurnCount;
        return permanent;
    }

    private void MoveMaterialsToSources(GameState state, PermanentId destinationPermanent, IEnumerable<CardInstanceId> materials)
    {
        foreach (var material in materials)
        {
            MoveFromCurrentZone(state, material, Zone.EvolutionSources, MoveReason.ComplexMechanic, destinationPermanent, toTop: false);
        }
    }

    private MoveCardResult MoveFromCurrentZone(
        GameState state,
        CardInstanceId card,
        Zone destination,
        MoveReason reason,
        PermanentId? destinationPermanent = null,
        bool toTop = true)
    {
        var instance = state.Cards[card];
        return _zoneMover.MoveCard(
            state,
            new MoveCardCommand(
                card,
                instance.CurrentZone,
                destination,
                reason,
                SourcePermanent: instance.PermanentId,
                DestinationPermanent: destinationPermanent,
                ToTop: toTop));
    }

    private static EvolutionRequirement RequireEvolutionRequirement(GameState state, CardInstanceId card, EvolutionMode mode) =>
        BattleRules.Definition(state, card).EvolutionRequirements.FirstOrDefault(requirement => requirement.Mode == mode)
        ?? throw new UnsupportedMechanicException($"{mode} requirement for card '{card}'");

    private static PlayRequirement RequirePlayRequirement(GameState state, CardInstanceId card, PlayMode mode, Mechanic? mechanic = null)
    {
        var definition = BattleRules.Definition(state, card);
        var requirement = definition.PlayRequirements.FirstOrDefault(requirement =>
            requirement.Mode == mode
            && (mechanic is null || definition.Mechanics.Contains(mechanic.Value) || requirement.LinkTargetRequirement is not null));

        return requirement ?? throw new UnsupportedMechanicException($"{mechanic?.ToString() ?? mode.ToString()} requirement for card '{card}'");
    }

    private static PlayRequirement? FindLinkPlayRequirement(GameState state, CardInstanceId card)
    {
        var definition = BattleRules.Definition(state, card);
        return definition.PlayRequirements.FirstOrDefault(requirement =>
            requirement.Mode == PlayMode.Normal
            && (definition.Mechanics.Contains(Mechanic.Link) || requirement.LinkTargetRequirement is not null)
            && requirement.LinkTargetRequirement is not null);
    }

    private static void PayCost(GameState state, PlayerId actor, int cost) =>
        BattleRules.PayMemory(state, actor, cost);

    private static int LinkMax(GameState state, PermanentState permanent) =>
        Math.Max(0, BattleRules.Definition(state, permanent.TopCardId).LinkedMax);

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

    private static SelectableTarget CardTarget(GameState state, CardInstanceId card) =>
        new(
            SelectionTargetKind.Card,
            $"card:{card.Value}",
            state.Cards[card].Owner,
            Card: card,
            Label: BattleRules.Definition(state, card).CardId);

    private static SelectableTarget PermanentTarget(GameState state, PermanentState permanent) =>
        new(
            SelectionTargetKind.Permanent,
            $"permanent:{permanent.Id.Value}",
            permanent.ControllerPlayerId,
            Permanent: permanent.Id,
            Label: BattleRules.Definition(state, permanent.TopCardId).CardId);

    private static string StableActionId(string stableId) =>
        string.Join(
            "_",
            stableId.Select(character => char.IsLetterOrDigit(character) ? character : '_'));
}
