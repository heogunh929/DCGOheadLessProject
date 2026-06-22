using System.Security.Cryptography;
using System.Text;

namespace DCGO.RL.Engine.Domain;

public sealed class GameState
{
    private int _turnCount;

    public GameState(GameConfig config)
    {
        Config = config;
    }

    public GameConfig Config { get; }
    public int Memory { get; set; }
    public Phase Phase { get; set; } = Phase.None;
    public int TurnCount
    {
        get => _turnCount;
        set
        {
            _turnCount = value;
            RuntimeRules.ClearOncePerTurnBefore(value);
        }
    }

    public PlayerId TurnPlayerId { get; set; } = PlayerId.Player0;
    public PlayerId FirstPlayerId { get; set; } = PlayerId.Player0;
    public GameResult Result { get; set; } = GameResult.Ongoing;
    public RuntimeRuleState RuntimeRules { get; } = new();
    public List<PlayerState> Players { get; } = new();
    public List<CardInstanceId> ActiveCardIds { get; } = new();
    public List<TemporaryModifier> TemporaryModifiers { get; } = new();
    public Dictionary<string, CardDefinition> CardDefinitions { get; } = new(StringComparer.Ordinal);
    public Dictionary<CardInstanceId, CardInstance> Cards { get; } = new();

    public PlayerId TurnPlayer
    {
        get => TurnPlayerId;
        set => TurnPlayerId = value;
    }

    public PlayerId FirstPlayer
    {
        get => FirstPlayerId;
        set => FirstPlayerId = value;
    }

    public PlayerId NonTurnPlayerId => Players.First(player => player.Id != TurnPlayerId).Id;
    public PlayerId NonTurnPlayer => NonTurnPlayerId;
    public bool IsGameOver => Result.Kind != GameResultKind.Ongoing;
    public PlayerId? WinnerPlayerId => Result.Winner;

    public static GameState CreateDefault(GameConfig? config = null)
    {
        var state = new GameState(config ?? new GameConfig());
        state.Players.Add(new PlayerState(PlayerId.Player0));
        state.Players.Add(new PlayerState(PlayerId.Player1));
        return state;
    }

    public PlayerState GetPlayer(PlayerId id) =>
        Players.FirstOrDefault(player => player.Id == id)
        ?? throw new DomainException($"Player '{id}' does not exist.");

    public GameState Clone()
    {
        var clone = new GameState(Config)
        {
            Memory = Memory,
            Phase = Phase,
            TurnCount = TurnCount,
            TurnPlayerId = TurnPlayerId,
            FirstPlayerId = FirstPlayerId,
            Result = Result,
        };

        clone.ActiveCardIds.AddRange(ActiveCardIds);
        clone.TemporaryModifiers.AddRange(TemporaryModifiers);
        clone.RuntimeRules.RestoreFrom(RuntimeRules);

        foreach (var player in Players)
        {
            clone.Players.Add(player.Clone());
        }

        foreach (var definition in CardDefinitions)
        {
            clone.CardDefinitions.Add(definition.Key, definition.Value);
        }

        foreach (var card in Cards)
        {
            clone.Cards.Add(card.Key, card.Value.Clone());
        }

        return clone;
    }

    public void RestoreFrom(GameState snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        if (!ReferenceEquals(Config, snapshot.Config))
        {
            throw new DomainException("Cannot restore GameState from a snapshot with a different config instance.");
        }

        Memory = snapshot.Memory;
        Phase = snapshot.Phase;
        TurnCount = snapshot.TurnCount;
        TurnPlayerId = snapshot.TurnPlayerId;
        FirstPlayerId = snapshot.FirstPlayerId;
        Result = snapshot.Result;

        ActiveCardIds.Clear();
        ActiveCardIds.AddRange(snapshot.ActiveCardIds);

        TemporaryModifiers.Clear();
        TemporaryModifiers.AddRange(snapshot.TemporaryModifiers);

        RuntimeRules.RestoreFrom(snapshot.RuntimeRules);

        Players.Clear();
        Players.AddRange(snapshot.Players.Select(player => player.Clone()));

        CardDefinitions.Clear();
        foreach (var definition in snapshot.CardDefinitions)
        {
            CardDefinitions.Add(definition.Key, definition.Value);
        }

        Cards.Clear();
        foreach (var card in snapshot.Cards)
        {
            Cards.Add(card.Key, card.Value.Clone());
        }
    }

    public string ComputeStateHash()
    {
        var builder = new StringBuilder();
        AppendHashData(builder);
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(builder.ToString()));
        return Convert.ToHexString(bytes);
    }

    private void AppendHashData(StringBuilder builder)
    {
        builder.Append("config:")
            .Append(Config.Seed).Append('|')
            .Append(Config.InitialHandSize).Append('|')
            .Append(Config.InitialSecurityCount).Append('|')
            .Append(Config.FieldSlotCount).AppendLine();

        builder.Append("game:")
            .Append(Memory).Append('|')
            .Append(Phase).Append('|')
            .Append(TurnCount).Append('|')
            .Append(TurnPlayerId.Value).Append('|')
            .Append(FirstPlayerId.Value).Append('|')
            .Append(Result.Kind).Append('|')
            .Append(Result.Winner?.Value.ToString() ?? "-").Append('|')
            .Append(Result.Reason).AppendLine();

        AppendIds(builder, "active", ActiveCardIds);

        foreach (var modifier in TemporaryModifiers.OrderBy(modifier => modifier.StableId, StringComparer.Ordinal))
        {
            builder.Append("temporary-modifier:")
                .Append(modifier.StableId).Append('|')
                .Append(modifier.SourceCardId?.Value.ToString() ?? "-").Append('|')
                .Append(modifier.SourcePermanentId?.Value.ToString() ?? "-").Append('|')
                .Append(modifier.ControllerPlayerId.Value).Append('|')
                .Append(modifier.TargetPermanentId?.Value.ToString() ?? "-").Append('|')
                .Append(modifier.TargetPlayerId?.Value.ToString() ?? "-").Append('|')
                .Append(modifier.ModifierKind).Append('|')
                .Append(modifier.Amount).Append('|')
                .Append(modifier.DurationScope).Append('|')
                .Append(modifier.CreatedTurnCount).Append('|')
                .Append(modifier.CreatedPhase).Append('|')
                .Append(modifier.ExpiresAtTurnPlayerId?.Value.ToString() ?? "-").Append('|')
                .Append(modifier.DebugLabel).AppendLine();
        }

        foreach (var use in RuntimeRules.OncePerTurnUses
            .OrderBy(use => use.TurnCount)
            .ThenBy(use => use.Player.Value)
            .ThenBy(use => use.EffectStableId, StringComparer.Ordinal)
            .ThenBy(use => use.SourceCard?.Value ?? -1))
        {
            builder.Append("runtime-once-per-turn:")
                .Append(use.TurnCount).Append('|')
                .Append(use.Player.Value).Append('|')
                .Append(use.EffectStableId).Append('|')
                .Append(use.SourceCard?.Value.ToString() ?? "-").AppendLine();
        }

        for (var index = 0; index < RuntimeRules.PendingRuleEvents.Count; index++)
        {
            var ruleEvent = RuntimeRules.PendingRuleEvents[index];
            builder.Append("runtime-pending-rule-event:")
                .Append(index).Append('|')
                .Append(ruleEvent.Timing).Append('|')
                .Append(ruleEvent.Player.Value).Append('|');

            foreach (var pair in ruleEvent.Values.OrderBy(pair => pair.Key, StringComparer.Ordinal))
            {
                builder.Append(pair.Key)
                    .Append('=')
                    .Append(FormatRuntimeRuleValue(pair.Value))
                    .Append(';');
            }

            builder.AppendLine();
        }

        if (RuntimeRules.Attack is { } attack)
        {
            builder.Append("runtime-attack:")
                .Append(attack.Attacker.Value).Append('|')
                .Append(attack.Defender?.Value.ToString() ?? "-").Append('|')
                .Append(attack.State).Append('|')
                .Append(attack.IsBlocking ? "1" : "0").Append('|')
                .Append(attack.Blocker?.Value.ToString() ?? "-").Append('|')
                .Append(attack.IsEndAttack ? "1" : "0").Append('|')
                .Append(attack.AttackerTopCardWhenDeclared?.Value.ToString() ?? "-").Append('|')
                .AppendJoin(",", attack.CounterSources.Select(card => card.Value)).Append('|')
                .Append(attack.CounterGroup).Append('|')
                .Append(attack.CounterUsed ? "1" : "0").Append('|')
                .AppendJoin(",", attack.AttemptedCounterCandidates.OrderBy(candidate => candidate, StringComparer.Ordinal)).Append('|');

            if (attack.TargetSwitchQueue.Count > 0)
            {
                builder.AppendJoin(
                    ";",
                    attack.TargetSwitchQueue.Select(pendingSwitch =>
                        string.Join(
                            "/",
                            pendingSwitch.OldDefender?.Value.ToString() ?? "-",
                            pendingSwitch.NewDefender?.Value.ToString() ?? "-",
                            pendingSwitch.IsBlocking ? "1" : "0",
                            pendingSwitch.Blocker?.Value.ToString() ?? "-",
                            pendingSwitch.SourceEffectStableId ?? "-")));
            }
            else
            {
                builder.Append("-");
            }

            builder.AppendLine();
        }

        foreach (var definition in CardDefinitions.OrderBy(pair => pair.Key, StringComparer.Ordinal))
        {
            builder.Append("definition:")
                .Append(definition.Key).Append('|')
                .Append(definition.Value.CardId).Append('|')
                .Append(definition.Value.CardIndex).Append('|')
                .Append(definition.Value.VariantKey).Append('|')
                .Append(definition.Value.DefinitionStableId).Append('|')
                .Append(definition.Value.CardNameEnglish).Append('|')
                .Append(definition.Value.CardNameJapanese).Append('|')
                .Append(string.Join(",", definition.Value.CardKinds)).Append('|')
                .Append(string.Join(",", definition.Value.CardColors)).Append('|')
                .Append(string.Join(",", definition.Value.EvoCosts.Select(evoCost =>
                    $"{evoCost.CardColor}:{evoCost.Level}:{evoCost.MemoryCost}"))).Append('|')
                .Append(definition.Value.Level).Append('|')
                .Append(definition.Value.PlayCost).Append('|')
                .Append(definition.Value.DP).Append('|')
                .Append(definition.Value.Rarity).Append('|')
                .Append(definition.Value.CardEffectClassName).Append('|')
                .Append(definition.Value.MaxCountInDeck).Append('|')
                .Append(definition.Value.OverflowMemory).Append('|')
                .Append(definition.Value.LinkDP).Append('|')
                .Append(definition.Value.SecurityAttackModifier).Append('|')
                .Append(string.Join(",", definition.Value.OptionCardColorRequirements)).Append('|')
                .Append(string.Join(",", definition.Value.Mechanics)).Append('|')
                .Append(string.Join(";", definition.Value.EvolutionRequirements.Select(FormatEvolutionRequirement))).Append('|')
                .Append(string.Join(";", definition.Value.PlayRequirements.Select(FormatPlayRequirement))).Append('|')
                .Append(string.Join(",", definition.Value.BattleKeywords)).Append('|')
                .Append(definition.Value.LinkedMax).Append('|')
                .Append(definition.Value.IsAce ? "1" : "0").Append('|')
                .Append(definition.Value.IsDualCard ? "1" : "0").AppendLine();
        }

        foreach (var card in Cards.OrderBy(pair => pair.Key.Value))
        {
            builder.Append("card:")
                .Append(card.Key.Value).Append('|')
                .Append(card.Value.DefinitionId).Append('|')
                .Append(card.Value.Owner.Value).Append('|')
                .Append(card.Value.Zone).Append('|')
                .Append(card.Value.IsFaceUp ? "1" : "0").Append('|')
                .Append(card.Value.PermanentId?.Value.ToString() ?? "-").AppendLine();
        }

        foreach (var player in Players.OrderBy(player => player.Id.Value))
        {
            builder.Append("player:").Append(player.Id.Value).AppendLine();
            AppendIds(builder, "deck", player.Deck);
            AppendIds(builder, "digitama", player.DigiEggDeck);
            AppendIds(builder, "hand", player.Hand);
            AppendIds(builder, "trash", player.Trash);
            AppendIds(builder, "lost", player.Lost);
            AppendIds(builder, "security", player.Security);
            AppendIds(builder, "executing", player.Executing);
            AppendIds(builder, "revealed", player.Revealed);
            AppendIds(builder, "outside", player.OutsideGame);

            foreach (var permanent in player.FieldPermanents.OrderBy(permanent => permanent.FrameIndex).ThenBy(permanent => permanent.Id.Value))
            {
                builder.Append("permanent:")
                    .Append(permanent.Id.Value).Append('|')
                    .Append(permanent.OwnerPlayerId.Value).Append('|')
                    .Append(permanent.ControllerPlayerId.Value).Append('|')
                    .Append(permanent.FrameIndex).Append('|')
                    .Append(permanent.IsBreedingArea ? "1" : "0").Append('|')
                    .Append(permanent.IsSuspended ? "1" : "0").Append('|')
                    .Append(permanent.EnterFieldTurnCount).Append('|')
                    .Append(permanent.DpModifier).Append('|')
                    .Append(permanent.IsBurstDigivolved ? "1" : "0").Append('|')
                    .Append(permanent.IsAppFusion ? "1" : "0").Append('|')
                    .Append(permanent.IsDelayOption ? "1" : "0").Append('|')
                    .Append(permanent.SecurityAttackModifier).Append('|')
                    .Append(string.Join(",", permanent.BattleKeywords)).Append('|')
                    .Append(permanent.TopCardId.Value).AppendLine();
                AppendIds(builder, "sources", permanent.SourceCardIds);
                AppendIds(builder, "links", permanent.LinkedCards);
            }
        }
    }

    private static void AppendIds(StringBuilder builder, string label, IEnumerable<CardInstanceId> ids)
    {
        builder.Append(label).Append(':').AppendJoin(',', ids.Select(id => id.Value)).AppendLine();
    }

    private static string FormatEvolutionRequirement(EvolutionRequirement requirement) =>
        $"{requirement.Mode}:{requirement.Cost}:{string.Join(",", requirement.Materials.Select(FormatMaterialRequirement))}:{FormatMaterialRequirement(requirement.TargetRequirement)}:{FormatMaterialRequirement(requirement.BurstTamerRequirement)}:{FormatMaterialRequirement(requirement.AppFusionLinkRequirement)}";

    private static string FormatPlayRequirement(PlayRequirement requirement) =>
        $"{requirement.Mode}:{requirement.FixedCost?.ToString() ?? "-"}:{requirement.ReduceCost}:{requirement.ReduceCostPerMaterial}:{requirement.LinkCost}:{requirement.MaxMaterials}:{string.Join(",", requirement.Materials.Select(FormatMaterialRequirement))}:{FormatMaterialRequirement(requirement.LinkTargetRequirement)}";

    private static string FormatRuntimeRuleValue(object? value) =>
        value switch
        {
            null => "-",
            CardInstanceId card => $"card:{card.Value}",
            PermanentId permanent => $"permanent:{permanent.Value}",
            PlayerId player => $"player:{player.Value}",
            Zone zone => $"zone:{zone}",
            bool boolean => boolean ? "true" : "false",
            string text => text,
            IEnumerable<CardInstanceId> cards => $"cards:{string.Join(",", cards.Select(card => card.Value))}",
            IEnumerable<PermanentId> permanents => $"permanents:{string.Join(",", permanents.Select(permanent => permanent.Value))}",
            IEnumerable<PlayerId> players => $"players:{string.Join(",", players.Select(player => player.Value))}",
            IEnumerable<Zone> zones => $"zones:{string.Join(",", zones)}",
            _ => value.ToString() ?? string.Empty,
        };

    private static string FormatMaterialRequirement(MaterialRequirement? requirement)
    {
        if (requirement is null)
        {
            return "-";
        }

        return string.Join(
            "/",
            requirement.Label,
            requirement.Count,
            string.Join(".", requirement.DefinitionIds),
            string.Join(".", requirement.CardKinds),
            string.Join(".", requirement.CardColors),
            string.Join(".", requirement.Levels),
            string.Join(".", requirement.Zones),
            requirement.AllowBattleArea ? "B" : "-",
            requirement.AllowHand ? "H" : "-",
            requirement.AllowTrash ? "T" : "-",
            requirement.AllowEvolutionSources ? "E" : "-",
            requirement.AllowLinkedCards ? "L" : "-");
    }
}
