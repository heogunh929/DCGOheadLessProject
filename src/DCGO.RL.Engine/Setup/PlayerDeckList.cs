using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Setup;

public sealed record PlayerDeckList
{
    public PlayerDeckList(PlayerId player, IEnumerable<string> mainDeck, IEnumerable<string>? digiEggDeck = null)
    {
        Player = player;
        MainDeck = mainDeck?.ToArray() ?? Array.Empty<string>();
        DigiEggDeck = digiEggDeck?.ToArray() ?? Array.Empty<string>();
    }

    public PlayerId Player { get; }
    public IReadOnlyList<string> MainDeck { get; }
    public IReadOnlyList<string> DigiEggDeck { get; }
}
