using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Battle;

public sealed class MoveFromBreedingService
{
    private readonly IZoneMover _zoneMover;

    public MoveFromBreedingService(IZoneMover? zoneMover = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
    }

    public PermanentState Move(GameState state, PlayerId playerId, PermanentId permanentId, int targetFrameIndex)
    {
        if (playerId != state.TurnPlayerId)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can move from breeding.");
        }

        var permanent = BattleRules.Permanent(state, permanentId);
        if (permanent.ControllerPlayerId != playerId)
        {
            throw new DomainException($"Permanent '{permanentId}' is not controlled by player '{playerId}'.");
        }

        if (!BattleRules.CanMoveFromBreeding(state, permanent))
        {
            throw new DomainException($"Permanent '{permanentId}' cannot move from breeding.");
        }

        if (!BattleRules.IsEmptyBattleFrame(state.GetPlayer(playerId), targetFrameIndex))
        {
            throw new DomainException($"Battle frame '{targetFrameIndex}' is not empty.");
        }

        _zoneMover.MovePermanent(
            state,
            new PermanentZoneMoveCommand(permanentId, Zone.BreedingArea, Zone.BattleArea, MoveReason.MoveFromBreeding, targetFrameIndex));

        return permanent;
    }
}
