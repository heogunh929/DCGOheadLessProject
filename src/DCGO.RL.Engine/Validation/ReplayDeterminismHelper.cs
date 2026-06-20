using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed record ReplayDeterminismResult(
    bool Matches,
    string ExpectedFinalStateHash,
    string ActualFinalStateHash);

public sealed class ReplayDeterminismHelper
{
    private readonly ReplayRunner _replayRunner;

    public ReplayDeterminismHelper(ReplayRunner? replayRunner = null)
    {
        _replayRunner = replayRunner
            ?? throw new DomainException("ReplayDeterminismHelper requires a ReplayRunner from BattleEngineServices.");
    }

    public ReplayDeterminismResult ReplayAndCompare(GameState initialState, GameTrace trace, string expectedFinalStateHash)
    {
        var replay = _replayRunner.Replay(initialState, trace);
        var actualHash = replay.FinalState.ComputeStateHash();
        return new ReplayDeterminismResult(
            string.Equals(actualHash, expectedFinalStateHash, StringComparison.Ordinal),
            expectedFinalStateHash,
            actualHash);
    }
}
