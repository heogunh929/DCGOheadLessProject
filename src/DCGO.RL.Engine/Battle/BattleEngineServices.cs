using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Mechanics;
using DCGO.RL.Engine.Primitives;
using DCGO.RL.Engine.Setup;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed record BattleEngineServiceGraphIssue(
    string ServiceName,
    string DependencyName,
    string Message);

public sealed record BattleEngineServiceGraphValidationReport(
    IReadOnlyList<BattleEngineServiceGraphIssue> Issues)
{
    public bool IsValid => Issues.Count == 0;

    public void ThrowIfInvalid()
    {
        if (IsValid)
        {
            return;
        }

        var message = string.Join(
            "; ",
            Issues.Select(issue => $"{issue.ServiceName}.{issue.DependencyName}: {issue.Message}"));
        throw new DomainException($"Battle engine service graph is invalid: {message}");
    }
}

public sealed class BattleEngineServices
{
    private BattleEngineServices(
        IZoneMover zoneMover,
        TriggerPipelineService triggerPipelineService,
        Tier1PrimitiveService primitiveService,
        EngineInvariantChecker invariantChecker,
        HatchService hatchService,
        MoveFromBreedingService moveFromBreedingService,
        PlayCardService playCardService,
        DigivolveService digivolveService,
        AttackService attackService,
        ComplexMechanicService complexMechanicService,
        PhaseRunner phaseRunner,
        RuleProcessor ruleProcessor,
        TurnRunner turnRunner,
        ActionExecutor actionExecutor)
    {
        ZoneMover = zoneMover;
        TriggerPipelineService = triggerPipelineService;
        PrimitiveService = primitiveService;
        InvariantChecker = invariantChecker;
        HatchService = hatchService;
        MoveFromBreedingService = moveFromBreedingService;
        PlayCardService = playCardService;
        DigivolveService = digivolveService;
        AttackService = attackService;
        ComplexMechanicService = complexMechanicService;
        PhaseRunner = phaseRunner;
        RuleProcessor = ruleProcessor;
        TurnRunner = turnRunner;
        ActionExecutor = actionExecutor;
    }

    public IZoneMover ZoneMover { get; }

    public TriggerPipelineService TriggerPipelineService { get; }

    public Tier1PrimitiveService PrimitiveService { get; }

    public EngineInvariantChecker InvariantChecker { get; }

    public HatchService HatchService { get; }

    public MoveFromBreedingService MoveFromBreedingService { get; }

    public PlayCardService PlayCardService { get; }

    public DigivolveService DigivolveService { get; }

    public AttackService AttackService { get; }

    public ComplexMechanicService ComplexMechanicService { get; }

    public PhaseRunner PhaseRunner { get; }

    public RuleProcessor RuleProcessor { get; }

    public TurnRunner TurnRunner { get; }

    public ActionExecutor ActionExecutor { get; }

    public BattleEngineServiceGraphValidationReport ValidationReport =>
        Validate(TriggerPipelineService, ZoneMover, PrimitiveService, InvariantChecker);

    public static BattleEngineServices Create(
        ICardScriptRegistry cardScriptRegistry,
        IDecisionProvider? decisionProvider = null)
    {
        ArgumentNullException.ThrowIfNull(cardScriptRegistry);
        return CreateCore(cardScriptRegistry, decisionProvider);
    }

    public static BattleEngineServiceGraphValidationReport Validate(
        TriggerPipelineService? triggerPipelineService,
        IZoneMover? zoneMover,
        Tier1PrimitiveService? primitiveService,
        EngineInvariantChecker? invariantChecker)
    {
        var issues = new List<BattleEngineServiceGraphIssue>();
        AddMissing(issues, nameof(BattleEngineServices), nameof(TriggerPipelineService), triggerPipelineService);
        AddMissing(issues, nameof(BattleEngineServices), nameof(IZoneMover), zoneMover);
        AddMissing(issues, nameof(BattleEngineServices), nameof(Tier1PrimitiveService), primitiveService);
        AddMissing(issues, nameof(BattleEngineServices), nameof(EngineInvariantChecker), invariantChecker);
        return new BattleEngineServiceGraphValidationReport(issues);
    }

    internal static BattleEngineServices CreateLegacyDefault() =>
        CreateCore(new LegacyNoEffectCardScriptRegistry(), decisionProvider: null);

    private static BattleEngineServices CreateCore(
        ICardScriptRegistry cardScriptRegistry,
        IDecisionProvider? decisionProvider)
    {
        var zoneMover = new ZoneMover();
        var invariantChecker = new EngineInvariantChecker();
        var selectionApplicator = new SelectionResultApplicator(invariantChecker);
        var drawService = new DrawService(zoneMover);
        var effectiveStats = new EffectiveStatService(cardScriptRegistry);
        var keywordService = new BattleKeywordService(effectiveStats);
        var durationCleanupService = new DurationCleanupService();
        var battleResolver = new BattleResolver(zoneMover, keywordService, effectiveStats);
        var primitiveService = new Tier1PrimitiveService(
            zoneMover,
            drawService,
            battleResolver,
            securityCheckService: null,
            playCardService: null,
            digivolveService: null);
        var triggerPipelineService = new TriggerPipelineService(
            cardScriptRegistry,
            decisionProvider,
            selectionApplicator: selectionApplicator,
            primitives: primitiveService,
            invariantChecker: invariantChecker);
        var securityEffectExecutionService = new SecurityEffectExecutionService(
            cardScriptRegistry,
            decisionProvider,
            selectionApplicator,
            primitiveService);
        var securityCheckService = new SecurityCheckService(
            zoneMover,
            battleResolver,
            keywordService,
            durationCleanupService,
            securityEffectExecutionService,
            effectiveStats);
        var playCardService = new PlayCardService(
            triggerPipelineService,
            zoneMover,
            selectionApplicator,
            primitiveService,
            invariantChecker);
        var digivolveService = new DigivolveService(zoneMover, drawService, triggerPipelineService);
        primitiveService.AttachRuntimeServices(playCardService, digivolveService);
        var hatchService = new HatchService(zoneMover);
        var moveFromBreedingService = new MoveFromBreedingService(zoneMover);
        var attackService = new AttackService(
            battleResolver,
            securityCheckService,
            new WinConditionChecker(),
            keywordService,
            durationCleanupService,
            triggerPipelineService,
            effectiveStats);
        var complexMechanicService = new ComplexMechanicService(zoneMover, drawService);
        var phaseRunner = new PhaseRunner(
            drawService,
            keywordService,
            durationCleanupService,
            triggerPipelineService);
        var ruleProcessor = new RuleProcessor(
            phaseRunner,
            battleResolver,
            keywordService,
            durationCleanupService,
            invariantChecker,
            triggerPipelineService: triggerPipelineService,
            effectiveStats: effectiveStats);
        var turnRunner = new TurnRunner(phaseRunner);
        var actionExecutor = new ActionExecutor(
            hatchService,
            moveFromBreedingService,
            playCardService,
            digivolveService,
            attackService,
            complexMechanicService,
            phaseRunner,
            ruleProcessor);

        var services = new BattleEngineServices(
            zoneMover,
            triggerPipelineService,
            primitiveService,
            invariantChecker,
            hatchService,
            moveFromBreedingService,
            playCardService,
            digivolveService,
            attackService,
            complexMechanicService,
            phaseRunner,
            ruleProcessor,
            turnRunner,
            actionExecutor);
        services.ValidationReport.ThrowIfInvalid();
        return services;
    }

    private static void AddMissing(
        List<BattleEngineServiceGraphIssue> issues,
        string serviceName,
        string dependencyName,
        object? dependency)
    {
        if (dependency is not null)
        {
            return;
        }

        issues.Add(new BattleEngineServiceGraphIssue(
            serviceName,
            dependencyName,
            "Required production runtime dependency is missing."));
    }

    private sealed class LegacyNoEffectCardScriptRegistry : ICardScriptRegistry
    {
        public IReadOnlyCollection<CardEffectPortingRecord> PortingRecords => Array.Empty<CardEffectPortingRecord>();

        public bool TryGetScript(CardDefinition definition, out ICardScript script)
        {
            ArgumentNullException.ThrowIfNull(definition);
            script = new NoEffectCardScript(
                definition.CardId,
                definition.CardEffectClassName,
                "Legacy default runtime graph treats unspecified card scripts as explicit no-effect.");
            return true;
        }

        public ICardScript GetScript(CardDefinition definition)
        {
            TryGetScript(definition, out var script);
            return script;
        }
    }
}
