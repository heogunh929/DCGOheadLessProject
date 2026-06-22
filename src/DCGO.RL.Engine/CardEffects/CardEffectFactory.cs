using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

public static partial class CardEffectFactory
{
    public static ContinuousEffectDescriptor ChangeSelfDPStaticEffect(
        ContinuousEffectScriptContext context,
        int changeValue,
        Func<ContinuousEffectEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        ChangeDPStaticEffect(
            context,
            ContinuousEffectTargetKind.SelfPermanent,
            changeValue,
            condition,
            effectName,
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousEffectDescriptor ChangeSelfDPStaticEffect(
        ContinuousEffectScriptContext context,
        Func<ContinuousEffectEvaluationContext, int> changeValue,
        Func<ContinuousEffectEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        ChangeDPStaticEffect(
            context,
            ContinuousEffectTargetKind.SelfPermanent,
            changeValue,
            condition,
            effectName,
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousEffectDescriptor ChangeDPStaticEffect(
        ContinuousEffectScriptContext context,
        ContinuousEffectTargetKind appliesTo,
        int changeValue,
        Func<ContinuousEffectEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null)
    {
        if (changeValue == 0)
        {
            throw new DomainException("DP change amount must not be zero.");
        }

        var debugLabel = string.IsNullOrWhiteSpace(effectName)
            ? $"DP {(changeValue > 0 ? "+" : string.Empty)}{changeValue}"
            : effectName.Trim();

        return ContinuousModifierStaticEffect(
            context,
            appliesTo,
            ContinuousModifierKind.DP,
            _ => changeValue,
            condition,
            debugLabel,
            AmountStableToken(changeValue),
            sourceMetadataCriteria,
            targetMetadataCriteria);
    }

    public static ContinuousEffectDescriptor ChangeDPStaticEffect(
        ContinuousEffectScriptContext context,
        ContinuousEffectTargetKind appliesTo,
        Func<ContinuousEffectEvaluationContext, int> changeValue,
        Func<ContinuousEffectEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null)
    {
        ArgumentNullException.ThrowIfNull(changeValue);

        var debugLabel = string.IsNullOrWhiteSpace(effectName)
            ? "DP dynamic"
            : effectName.Trim();

        return ContinuousModifierStaticEffect(
            context,
            appliesTo,
            ContinuousModifierKind.DP,
            changeValue,
            condition,
            debugLabel,
            "dynamic",
            sourceMetadataCriteria,
            targetMetadataCriteria);
    }

    public static ContinuousKeywordDescriptor BlockerSelfStaticEffect(
        ContinuousEffectScriptContext context,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        BlockerStaticEffect(
            context,
            ContinuousEffectTargetKind.SelfPermanent,
            condition,
            effectName,
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousKeywordDescriptor BlockerStaticEffect(
        ContinuousEffectScriptContext context,
        ContinuousEffectTargetKind appliesTo,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        KeywordStaticEffect(
            context,
            BattleKeyword.Blocker,
            appliesTo,
            condition,
            effectName ?? "Blocker",
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousKeywordDescriptor RushSelfStaticEffect(
        ContinuousEffectScriptContext context,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        RushStaticEffect(
            context,
            ContinuousEffectTargetKind.SelfPermanent,
            condition,
            effectName,
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousKeywordDescriptor RushStaticEffect(
        ContinuousEffectScriptContext context,
        ContinuousEffectTargetKind appliesTo,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        KeywordStaticEffect(
            context,
            BattleKeyword.Rush,
            appliesTo,
            condition,
            effectName ?? "Rush",
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousKeywordDescriptor RebootSelfStaticEffect(
        ContinuousEffectScriptContext context,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        RebootStaticEffect(
            context,
            ContinuousEffectTargetKind.SelfPermanent,
            condition,
            effectName,
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousKeywordDescriptor RebootStaticEffect(
        ContinuousEffectScriptContext context,
        ContinuousEffectTargetKind appliesTo,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        KeywordStaticEffect(
            context,
            BattleKeyword.Reboot,
            appliesTo,
            condition,
            effectName ?? "Reboot",
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousKeywordDescriptor CollisionSelfStaticEffect(
        ContinuousEffectScriptContext context,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        CollisionStaticEffect(
            context,
            ContinuousEffectTargetKind.SelfPermanent,
            condition,
            effectName,
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousKeywordDescriptor CollisionStaticEffect(
        ContinuousEffectScriptContext context,
        ContinuousEffectTargetKind appliesTo,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        KeywordStaticEffect(
            context,
            BattleKeyword.Collision,
            appliesTo,
            condition,
            effectName ?? "Collision",
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousKeywordDescriptor JammingSelfStaticEffect(
        ContinuousEffectScriptContext context,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        JammingStaticEffect(
            context,
            ContinuousEffectTargetKind.SelfPermanent,
            condition,
            effectName,
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousKeywordDescriptor JammingStaticEffect(
        ContinuousEffectScriptContext context,
        ContinuousEffectTargetKind appliesTo,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null) =>
        KeywordStaticEffect(
            context,
            BattleKeyword.Jamming,
            appliesTo,
            condition,
            effectName ?? "Jamming",
            sourceMetadataCriteria,
            targetMetadataCriteria);

    public static ContinuousKeywordDescriptor KeywordStaticEffect(
        ContinuousEffectScriptContext context,
        BattleKeyword keyword,
        ContinuousEffectTargetKind appliesTo,
        Func<ContinuousKeywordEvaluationContext, bool>? condition = null,
        string? effectName = null,
        CardMetadataCriteria? sourceMetadataCriteria = null,
        CardMetadataCriteria? targetMetadataCriteria = null)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (!Enum.IsDefined(keyword))
        {
            throw new DomainException($"Battle keyword '{keyword}' is not supported by CardEffectFactory.");
        }

        if (!IsSupportedStaticKeywordFactoryKeyword(keyword))
        {
            throw new UnsupportedMechanicException(
                $"Battle keyword '{keyword}' does not have a static keyword factory descriptor mapping.");
        }

        var debugLabel = string.IsNullOrWhiteSpace(effectName)
            ? keyword.ToString()
            : effectName.Trim();

        return new ContinuousKeywordDescriptor(
            StableKeywordId(context, keyword, appliesTo, debugLabel),
            context.SourceCard,
            context.SourcePermanent,
            context.Controller,
            keyword,
            appliesTo,
            condition,
            debugLabel,
            sourceMetadataCriteria,
            targetMetadataCriteria);
    }

    private static bool IsSupportedStaticKeywordFactoryKeyword(BattleKeyword keyword) =>
        keyword is BattleKeyword.Blocker
            or BattleKeyword.Rush
            or BattleKeyword.Reboot
            or BattleKeyword.Collision
            or BattleKeyword.Jamming;

    public static StaticEvolutionRequirementDescriptor AddSelfDigivolutionRequirementStaticEffect(
        ContinuousEffectScriptContext context,
        Func<StaticEvolutionRequirementEvaluationContext, bool>? permanentCondition,
        int digivolutionCost,
        bool ignoreDigivolutionRequirement,
        Func<StaticEvolutionRequirementEvaluationContext, bool>? condition = null,
        string? effectName = null,
        Func<StaticEvolutionRequirementEvaluationContext, bool>? cardCondition = null,
        Func<StaticEvolutionRequirementEvaluationContext, int>? costEquation = null,
        CardColor cardColor = CardColor.None,
        int level = -1,
        int minLevel = -1,
        int maxLevel = -1) =>
        AddDigivolutionRequirementStaticEffect(
            context,
            permanentCondition,
            cardCondition,
            ignoreDigivolutionRequirement,
            digivolutionCost,
            condition,
            effectName ?? "Can digivolve to this card",
            costEquation,
            cardColor,
            level,
            minLevel,
            maxLevel);

    public static StaticEvolutionRequirementDescriptor AddDigivolutionRequirementStaticEffect(
        ContinuousEffectScriptContext context,
        Func<StaticEvolutionRequirementEvaluationContext, bool>? permanentCondition,
        Func<StaticEvolutionRequirementEvaluationContext, bool>? cardCondition,
        bool ignoreDigivolutionRequirement,
        int digivolutionCost,
        Func<StaticEvolutionRequirementEvaluationContext, bool>? condition,
        string? effectName,
        Func<StaticEvolutionRequirementEvaluationContext, int>? costEquation = null,
        CardColor cardColor = CardColor.None,
        int level = -1,
        int minLevel = -1,
        int maxLevel = -1)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (digivolutionCost < 0 && costEquation is null)
        {
            throw new DomainException("Digivolution cost must not be negative without a cost equation.");
        }

        var effectiveMinLevel = level >= 0 ? -1 : minLevel;
        var effectiveMaxLevel = level >= 0 ? -1 : maxLevel;
        var debugLabel = string.IsNullOrWhiteSpace(effectName)
            ? "Can digivolve to this card"
            : effectName.Trim();

        return new StaticEvolutionRequirementDescriptor(
            StableEvolutionRequirementId(
                context,
                debugLabel,
                digivolutionCost,
                cardColor,
                level,
                effectiveMinLevel,
                effectiveMaxLevel),
            context.SourceCard,
            context.SourcePermanent,
            context.Controller,
            digivolutionCost,
            cardColor,
            level,
            effectiveMinLevel,
            effectiveMaxLevel,
            ignoreDigivolutionRequirement,
            CombineSourceConditions(condition, cardCondition),
            permanentCondition,
            costEquation,
            debugLabel);
    }

    private static Func<StaticEvolutionRequirementEvaluationContext, bool>? CombineSourceConditions(
        Func<StaticEvolutionRequirementEvaluationContext, bool>? condition,
        Func<StaticEvolutionRequirementEvaluationContext, bool>? cardCondition)
    {
        if (condition is null && cardCondition is null)
        {
            return null;
        }

        return context =>
            (condition?.Invoke(context) ?? true)
            && (cardCondition?.Invoke(context) ?? true);
    }

    private static string StableEvolutionRequirementId(
        ContinuousEffectScriptContext context,
        string debugLabel,
        int cost,
        CardColor cardColor,
        int level,
        int minLevel,
        int maxLevel) =>
        $"{context.SourceCard.Value}:{context.SourceKind}:add-digivolution-requirement:{StableToken(debugLabel)}:{cost}:{cardColor}:{level}:{minLevel}:{maxLevel}";

    private static string StableKeywordId(
        ContinuousEffectScriptContext context,
        BattleKeyword keyword,
        ContinuousEffectTargetKind appliesTo,
        string debugLabel) =>
        $"{context.SourceCard.Value}:{context.SourceKind}:keyword:{keyword}:{appliesTo}:{StableToken(debugLabel)}";

    private static ContinuousEffectDescriptor ContinuousModifierStaticEffect(
        ContinuousEffectScriptContext context,
        ContinuousEffectTargetKind appliesTo,
        ContinuousModifierKind modifierKind,
        Func<ContinuousEffectEvaluationContext, int> amount,
        Func<ContinuousEffectEvaluationContext, bool>? condition,
        string debugLabel,
        string amountStableToken,
        CardMetadataCriteria? sourceMetadataCriteria,
        CardMetadataCriteria? targetMetadataCriteria)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(amount);

        if (!Enum.IsDefined(appliesTo))
        {
            throw new DomainException($"Continuous effect target '{appliesTo}' is not supported by CardEffectFactory.");
        }

        return new ContinuousEffectDescriptor(
            StableContinuousModifierId(context, modifierKind, appliesTo, debugLabel, amountStableToken),
            context.SourceCard,
            context.SourcePermanent,
            context.Controller,
            appliesTo,
            modifierKind,
            amount,
            condition,
            debugLabel,
            sourceMetadataCriteria,
            targetMetadataCriteria);
    }

    private static string StableContinuousModifierId(
        ContinuousEffectScriptContext context,
        ContinuousModifierKind modifierKind,
        ContinuousEffectTargetKind appliesTo,
        string debugLabel,
        string amountStableToken) =>
        $"{context.SourceCard.Value}:{context.SourceKind}:continuous:{modifierKind}:{appliesTo}:{StableToken(debugLabel)}:{StableToken(amountStableToken)}";

    private static string AmountStableToken(int amount) =>
        amount > 0 ? $"plus-{amount}" : $"minus-{-(long)amount}";

    private static string StableToken(string value)
    {
        var token = new string(value
            .Trim()
            .Select(character => char.IsLetterOrDigit(character) ? char.ToLowerInvariant(character) : '-')
            .ToArray());

        while (token.Contains("--", StringComparison.Ordinal))
        {
            token = token.Replace("--", "-", StringComparison.Ordinal);
        }

        token = token.Trim('-');
        return token.Length == 0 ? "effect" : token;
    }
}
