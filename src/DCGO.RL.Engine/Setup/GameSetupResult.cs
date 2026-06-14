using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Setup;

public sealed record GameSetupResult(
    GameState State,
    GameTrace Trace,
    DeckValidationReport DeckValidationReport);
