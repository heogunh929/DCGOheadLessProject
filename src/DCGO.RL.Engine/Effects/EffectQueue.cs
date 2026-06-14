namespace DCGO.RL.Engine.Effects;

public sealed class EffectQueue
{
    private readonly Queue<EffectResolution> _pending = new();

    public int Count => _pending.Count;
    public bool HasPending => _pending.Count > 0;
    public IReadOnlyList<EffectResolution> Pending => _pending.ToArray();

    public void Enqueue(EffectResolution resolution) => _pending.Enqueue(resolution);

    public void EnqueueRange(IEnumerable<EffectResolution> resolutions)
    {
        foreach (var resolution in resolutions)
        {
            Enqueue(resolution);
        }
    }

    public EffectResolution Dequeue()
    {
        if (_pending.Count == 0)
        {
            throw new InvalidOperationException("EffectQueue is empty.");
        }

        return _pending.Dequeue();
    }

    public IReadOnlyList<EffectResolution> DrainTiming(EffectTiming timing)
    {
        var retained = new Queue<EffectResolution>();
        var drained = new List<EffectResolution>();

        while (_pending.Count > 0)
        {
            var resolution = _pending.Dequeue();
            if (resolution.Timing == timing)
            {
                drained.Add(resolution);
            }
            else
            {
                retained.Enqueue(resolution);
            }
        }

        while (retained.Count > 0)
        {
            _pending.Enqueue(retained.Dequeue());
        }

        return drained;
    }
}
