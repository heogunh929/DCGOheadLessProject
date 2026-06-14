using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Setup;

public sealed record DrawResult(
    PlayerId Player,
    IReadOnlyList<CardInstanceId> DrawnCards,
    bool RequestedMoreThanAvailable);

public sealed record DrawPhaseResult(
    PlayerId Player,
    bool SkippedFirstTurnDraw,
    bool DeckOutLoss,
    DrawResult Draw);

public sealed class DrawService
{
    private readonly IZoneMover _zoneMover;
    private readonly WinConditionChecker _winConditionChecker;

    public DrawService(IZoneMover? zoneMover = null, WinConditionChecker? winConditionChecker = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
        _winConditionChecker = winConditionChecker ?? new WinConditionChecker();
    }

    public DrawResult DrawCards(GameState state, PlayerId playerId, int count, GameTrace? trace = null)
    {
        if (count < 0)
        {
            throw new DomainException("Draw count must not be negative.");
        }

        var player = state.GetPlayer(playerId);
        var drawnCards = new List<CardInstanceId>();

        for (var i = 0; i < count; i++)
        {
            if (player.Deck.Count == 0)
            {
                return new DrawResult(playerId, drawnCards, RequestedMoreThanAvailable: true);
            }

            var card = player.Deck[0];
            var command = new MoveCardCommand(card, Zone.Deck, Zone.Hand, MoveReason.Draw);
            var before = trace is null ? null : state.Clone();
            var result = _zoneMover.MoveCard(state, command);
            trace?.AddMove($"draw:{playerId.Value}:{drawnCards.Count}", before!, state, command, result);
            drawnCards.Add(card);
        }

        return new DrawResult(playerId, drawnCards, RequestedMoreThanAvailable: false);
    }

    public DrawPhaseResult ExecuteDrawPhase(GameState state, GameTrace? trace = null)
    {
        var player = state.TurnPlayerId;
        state.Phase = Phase.Draw;

        if (state.TurnCount == 1)
        {
            return new DrawPhaseResult(player, SkippedFirstTurnDraw: true, DeckOutLoss: false, Draw: new DrawResult(player, Array.Empty<CardInstanceId>(), false));
        }

        if (state.GetPlayer(player).Deck.Count == 0)
        {
            _winConditionChecker.ApplyDeckOutLoss(state, player);
            trace?.AddStateSnapshot($"draw-phase:deck-out:{player.Value}", state);
            return new DrawPhaseResult(player, SkippedFirstTurnDraw: false, DeckOutLoss: true, Draw: new DrawResult(player, Array.Empty<CardInstanceId>(), true));
        }

        var draw = DrawCards(state, player, 1, trace);
        return new DrawPhaseResult(player, SkippedFirstTurnDraw: false, DeckOutLoss: false, Draw: draw);
    }
}
