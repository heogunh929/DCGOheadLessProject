using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Primitives;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.CardEffects;

public sealed record CardScriptContext(
    GameState State,
    CardInstanceId SourceCard,
    PermanentId? SourcePermanent = null,
    PlayerId? Controller = null);

public sealed class CardScriptExecutionContext
{
    private readonly GameState _state;

    public CardScriptExecutionContext(
        GameState state,
        EffectResolution resolution,
        Tier1PrimitiveService primitives,
        GameTrace? trace = null)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        Resolution = resolution ?? throw new ArgumentNullException(nameof(resolution));
        Primitives = primitives ?? throw new ArgumentNullException(nameof(primitives));
        Trace = trace;
    }

    public EffectResolution Resolution { get; }
    public Tier1PrimitiveService Primitives { get; }
    public GameTrace? Trace { get; }

    public void WithState(Action<GameState, Tier1PrimitiveService> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        operation(_state, Primitives);
    }

    public TResult WithState<TResult>(Func<GameState, Tier1PrimitiveService, TResult> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        return operation(_state, Primitives);
    }
}
