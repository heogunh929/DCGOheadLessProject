namespace DCGO.RL.Engine.Domain;

public sealed record GameResult(GameResultKind Kind, PlayerId? Winner, string Reason)
{
    public static GameResult Ongoing { get; } = new(GameResultKind.Ongoing, null, string.Empty);
    public static GameResult Win(PlayerId winner, string reason) => new(GameResultKind.Win, winner, reason);
}
