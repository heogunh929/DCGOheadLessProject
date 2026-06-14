using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Infrastructure;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Setup;

public sealed class GameSetupService
{
    private readonly DeckInstantiationService _deckInstantiationService;
    private readonly DrawService _drawService;
    private readonly SecuritySetupService _securitySetupService;
    private readonly FirstPlayerSelector _firstPlayerSelector;
    private readonly EngineInvariantChecker _invariantChecker;

    public GameSetupService(
        DeckInstantiationService? deckInstantiationService = null,
        DrawService? drawService = null,
        SecuritySetupService? securitySetupService = null,
        FirstPlayerSelector? firstPlayerSelector = null,
        EngineInvariantChecker? invariantChecker = null)
    {
        _deckInstantiationService = deckInstantiationService ?? new DeckInstantiationService();
        _drawService = drawService ?? new DrawService();
        _securitySetupService = securitySetupService ?? new SecuritySetupService();
        _firstPlayerSelector = firstPlayerSelector ?? new FirstPlayerSelector();
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
    }

    public GameSetupResult SetupGame(GameSetupRequest request, ICardDatabase cardDatabase)
    {
        var state = GameState.CreateDefault(request.Config);
        var trace = new GameTrace();
        var rng = new XorShiftDeterministicRng(request.Config.Seed);

        var deckReport = _deckInstantiationService.InstantiateDecks(state, request.PlayerDecks, cardDatabase);
        _deckInstantiationService.ShuffleDecks(state, rng);
        trace.AddStateSnapshot("setup:after-shuffle", state);

        var firstPlayer = _firstPlayerSelector.SelectFirstPlayer(state, rng, request.FirstPlayer);
        state.FirstPlayerId = firstPlayer;
        state.TurnPlayerId = firstPlayer;
        state.TurnCount = 0;
        state.Memory = 0;
        state.Phase = Phase.None;

        foreach (var player in PlayersInSetupOrder(state, firstPlayer))
        {
            _drawService.DrawCards(state, player.Id, state.Config.InitialHandSize, trace);
        }

        foreach (var player in PlayersInSetupOrder(state, firstPlayer))
        {
            _securitySetupService.AddSecurityFromDeck(state, player.Id, state.Config.InitialSecurityCount, trace);
        }

        _invariantChecker.ThrowIfInvalid(state);
        trace.AddStateSnapshot("setup:complete", state);
        return new GameSetupResult(state, trace, deckReport);
    }

    private static IReadOnlyList<PlayerState> PlayersInSetupOrder(GameState state, PlayerId firstPlayer)
    {
        var first = state.GetPlayer(firstPlayer);
        var second = state.Players.First(player => player.Id != firstPlayer);
        return new[] { first, second };
    }
}
