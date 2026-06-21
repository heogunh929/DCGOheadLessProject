// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_085.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_085Script : ICardScript, IContinuousCardScript
{
    private const string StartTurnStableId = "BT1-085:on-start-turn:set-memory-to-three";
    private const string SecurityStableId = "BT1-085:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-085",
        "BT1_085",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnStartTurn set-memory-to-3, owner-turn red Digimon SecurityAttack +1 with 4+ sources, and SecuritySkill play-self Tamer are implemented. CanAddMemory replacement/restriction remains blocked.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            Bt1ScriptSupport.CreateSetMemoryTo3TamerDescriptor(
                StartTurnStableId,
                context.SourceCard,
                context.SourcePermanent,
                controller),
            Bt1ScriptSupport.CreatePlaySelfTamerSecurityDescriptor(
                SecurityStableId,
                context.SourceCard,
                context.SourcePermanent,
                controller),
        };
    }

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.FieldTop)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        bool Condition(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.TargetPermanent is { } permanent
            && evaluation.State.TurnPlayerId == context.Controller
            && Bt1ScriptSupport.IsTopSourceInBattleArea(
                evaluation.State,
                context.SourceCard,
                context.SourcePermanent)
            && BattleRules.Definition(evaluation.State, permanent.TopCardId).CardColors.Contains(CardColor.Red)
            && permanent.SourceCardIds.Count >= 4;

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT1-085:continuous:red-security-attack:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.OwnerBattleAreaDigimon,
                ContinuousModifierKind.SecurityAttack,
                _ => 1,
                Condition,
                "BT1-085 owner-turn red Digimon SecurityAttack +1 with 4+ sources")
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == StartTurnStableId)
        {
            context.WithState((state, _) =>
            {
                var controller = context.Resolution.Controller
                    ?? Bt1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                Bt1ScriptSupport.SetMemoryTo3IfLow(state, controller);
            });
            return;
        }

        if (context.Resolution.StableId == SecurityStableId)
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? Bt1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                Bt1ScriptSupport.PlaySelfTamerFromExecuting(
                    state,
                    primitives,
                    controller,
                    context.Resolution.SourceCard);
            });
            return;
        }

        throw new DomainException($"BT1-085 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }
}
