namespace DCGO.RL.Engine.Domain;

public enum Phase
{
    Active,
    Draw,
    Breeding,
    Main,
    End,
    None,
}

public enum Zone
{
    None = 0,
    Deck = 1,
    DigiEggDeck = 2,
    Hand = 3,
    Trash = 4,
    Lost = 5,
    Security = 6,
    Executing = 7,
    BattleArea = 8,
    BreedingArea = 9,
    EvolutionSources = 10,
    DigivolutionCards = EvolutionSources,
    LinkedCards = 11,
    Revealed = 12,
    OutsideGame = 13,
}

public enum MoveReason
{
    Setup,
    Draw,
    SecuritySetup,
    Play,
    Digivolve,
    Hatch,
    MoveFromBreeding,
    Effect,
    Battle,
    Trash,
    ReturnToDeck,
    Link,
    Reveal,
    ComplexMechanic,
}

public enum CardKind
{
    Digimon,
    Tamer,
    Option,
    DigiEgg,
}

public enum CardColor
{
    Red,
    Blue,
    Yellow,
    Green,
    White,
    Black,
    Purple,
    None,
}

public enum CardRarity
{
    C,
    U,
    R,
    SR,
    UR,
    SEC,
    P,
    None,
}

public enum GameResultKind
{
    Ongoing,
    Win,
    Draw,
}

public enum DecisionKind
{
    LegalAction,
    Selection,
}

public enum SelectionKind
{
    ChooseAction,
    SelectCard,
    SelectPermanent,
    SelectSecurity,
    SelectFieldSlot,
    SelectCount,
    SelectYesNo,
    SelectOrder,
}

public enum LegalActionKind
{
    Hatch,
    MoveFromBreeding,
    PlayCard,
    Digivolve,
    Jogress,
    BurstDigivolve,
    AppFusion,
    DigiXrosPlay,
    AssemblyPlay,
    Link,
    DelayOption,
    Attack,
    ActivateEffect,
    Pass,
    Mulligan,
    Selection,
}

public enum Mechanic
{
    Normal,
    Jogress,
    BurstDigivolution,
    AppFusion,
    DigiXros,
    Assembly,
    Link,
    DelayOption,
    AceOverflow,
}

public enum BattleKeyword
{
    Blocker,
    SecurityAttack,
    Piercing,
    Jamming,
    Rush,
    Reboot,
    Retaliation,
    Decoy,
    Collision,
}

public enum PlayMode
{
    Normal,
    DigiXros,
    Assembly,
    DelayOption,
}

public enum EvolutionMode
{
    Normal,
    Jogress,
    BurstDigivolution,
    AppFusion,
}

public enum SelectionTargetKind
{
    Card,
    Permanent,
    Security,
    FieldSlot,
    Count,
    Boolean,
    Option,
}
