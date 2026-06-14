using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Setup;

public sealed class WinConditionChecker
{
    public const string DeckOutReason = "DeckOut";
    public const string DirectAttackReason = "DirectAttack";

    public bool ApplyDeckOutLoss(GameState state, PlayerId losingPlayer)
    {
        if (state.IsGameOver)
        {
            return false;
        }

        _ = state.GetPlayer(losingPlayer);
        var winner = state.Players.First(player => player.Id != losingPlayer).Id;
        state.Result = GameResult.Win(winner, DeckOutReason);
        return true;
    }

    public bool ApplyDirectAttackWin(GameState state, PlayerId winningPlayer)
    {
        if (state.IsGameOver)
        {
            return false;
        }

        _ = state.GetPlayer(winningPlayer);
        state.Result = GameResult.Win(winningPlayer, DirectAttackReason);
        return true;
    }
}
