using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Setup;

public sealed record GameSetupRequest
{
    public GameSetupRequest(
        GameConfig config,
        IEnumerable<PlayerDeckList> playerDecks,
        PlayerId? firstPlayer = null)
    {
        Config = config;
        PlayerDecks = playerDecks?.ToArray() ?? Array.Empty<PlayerDeckList>();
        FirstPlayer = firstPlayer;
    }

    public GameConfig Config { get; }
    public IReadOnlyList<PlayerDeckList> PlayerDecks { get; }
    public PlayerId? FirstPlayer { get; }
}
