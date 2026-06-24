using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Primitives;

namespace DCGO.RL.Engine.Battle;

public sealed class MoveFromBreedingService
{
    private readonly Tier1PrimitiveService _primitives;

    public MoveFromBreedingService(IZoneMover? zoneMover = null, Tier1PrimitiveService? primitiveService = null)
    {
        var mover = zoneMover ?? new ZoneMover();
        _primitives = primitiveService ?? new Tier1PrimitiveService(mover);
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

        _primitives.MovePermanentWithEvents(
            state,
            new PermanentZoneMoveCommand(permanentId, Zone.BreedingArea, Zone.BattleArea, MoveReason.MoveFromBreeding, targetFrameIndex));

        return permanent;
    }
}
