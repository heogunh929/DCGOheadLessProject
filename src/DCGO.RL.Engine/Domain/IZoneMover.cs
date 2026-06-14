namespace DCGO.RL.Engine.Domain;

public interface IZoneMover
{
    MoveCardResult MoveCard(GameState state, MoveCardCommand command);
    PermanentZoneMoveResult MovePermanent(GameState state, PermanentZoneMoveCommand command);
    DigivolveCardResult DigivolveCard(GameState state, DigivolveCardCommand command);
}
