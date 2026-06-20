using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Battle;

public sealed class TurnRunner
{
    private readonly PhaseRunner _phaseRunner;

    public TurnRunner(PhaseRunner? phaseRunner = null)
    {
        _phaseRunner = phaseRunner ?? throw new DomainException("TurnRunner requires a PhaseRunner from BattleEngineServices.");
    }

    internal PhaseRunner RuntimePhaseRunner => _phaseRunner;

    public void RunToMainPhase(GameState state)
    {
        _phaseRunner.RunActivePhase(state);
        if (state.IsGameOver)
        {
            return;
        }

        _phaseRunner.RunDrawPhase(state);
        if (state.IsGameOver)
        {
            return;
        }

        _phaseRunner.RunBreedingPhase(state);
        _phaseRunner.RunMainPhase(state);
    }
}
