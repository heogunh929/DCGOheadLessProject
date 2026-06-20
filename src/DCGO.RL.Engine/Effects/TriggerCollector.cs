namespace DCGO.RL.Engine.Effects;

public sealed record TriggerCollectionResult(
    IReadOnlyList<EffectResolution> QueuedEffects,
    IReadOnlyList<EffectResolution> BackgroundEffects)
{
    public IReadOnlyList<EffectResolution> All =>
        QueuedEffects.Concat(BackgroundEffects).ToArray();
}

public sealed class TriggerCollector
{
    private readonly List<EffectDescriptor> _registeredEffects = new();
    private readonly OptionalEffectBoundary _optionalBoundary;

    public TriggerCollector(OptionalEffectBoundary? optionalBoundary = null)
    {
        _optionalBoundary = optionalBoundary ?? new OptionalEffectBoundary();
    }

    public void Register(EffectDescriptor descriptor) => _registeredEffects.Add(descriptor);

    public IReadOnlyList<EffectDescriptor> RegisteredEffects => _registeredEffects;

    public TriggerCollectionResult Collect(EffectContext context) =>
        Collect(context, _registeredEffects);

    public TriggerCollectionResult Collect(
        EffectContext context,
        IEnumerable<EffectDescriptor> descriptors)
    {
        var queued = new List<EffectResolution>();
        var background = new List<EffectResolution>();

        foreach (var descriptor in descriptors)
        {
            if (!descriptor.Matches(context))
            {
                continue;
            }

            var resolution = new EffectResolution(
                descriptor.StableId,
                descriptor.Timing,
                descriptor.SourceCard,
                descriptor.SourcePermanent,
                descriptor.Controller,
                descriptor.IsBackground,
                descriptor.IsOptional,
                context,
                descriptor.IsOptional
                    ? _optionalBoundary.CreateRequest(descriptor, context)
                    : null,
                descriptor.CreateSelectionRequest?.Invoke(context),
                descriptor.SelectionContinuation,
                descriptor.SecurityExecutionMode,
                descriptor.IsOncePerTurn,
                descriptor.OncePerTurnKey,
                descriptor.CanActivate);

            if (descriptor.IsBackground)
            {
                background.Add(resolution);
            }
            else
            {
                queued.Add(resolution);
            }
        }

        return new TriggerCollectionResult(queued, background);
    }
}
