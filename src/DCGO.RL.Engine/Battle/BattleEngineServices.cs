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
        SecurityCheckService securityCheckService,
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
        SecurityCheckService = securityCheckService;
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

    public SecurityCheckService SecurityCheckService { get; }

    public AttackService AttackService { get; }

    public ComplexMechanicService ComplexMechanicService { get; }

    public PhaseRunner PhaseRunner { get; }

    public RuleProcessor RuleProcessor { get; }

    public TurnRunner TurnRunner { get; }

    public ActionExecutor ActionExecutor { get; }

    public BattleEngineServiceGraphValidationReport ValidationReport =>
        Validate(this);

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
        EngineInvariantChecker? invariantChecker,
        SecurityCheckService? securityCheckService = null)
    {
        var issues = new List<BattleEngineServiceGraphIssue>();
        AddMissing(issues, nameof(BattleEngineServices), nameof(TriggerPipelineService), triggerPipelineService);
        AddMissing(issues, nameof(BattleEngineServices), nameof(IZoneMover), zoneMover);
        AddMissing(issues, nameof(BattleEngineServices), nameof(Tier1PrimitiveService), primitiveService);
        AddMissing(issues, nameof(BattleEngineServices), nameof(EngineInvariantChecker), invariantChecker);
        AddMissing(issues, nameof(BattleEngineServices), nameof(SecurityCheckService), securityCheckService);
        return new BattleEngineServiceGraphValidationReport(issues);
    }

    public static BattleEngineServiceGraphValidationReport Validate(BattleEngineServices? services)
    {
        if (services is null)
        {
            return new BattleEngineServiceGraphValidationReport(new[]
            {
                new BattleEngineServiceGraphIssue(
                    nameof(BattleEngineServices),
                    nameof(BattleEngineServices),
                    "Service graph is missing."),
            });
        }

        var issues = Validate(
            services.TriggerPipelineService,
            services.ZoneMover,
            services.PrimitiveService,
            services.InvariantChecker,
            services.SecurityCheckService).Issues.ToList();

        AddMismatch(
            issues,
            nameof(Tier1PrimitiveService),
            nameof(IZoneMover),
            services.ZoneMover,
            services.PrimitiveService.RuntimeZoneMover);
        AddMismatch(
            issues,
            nameof(PlayCardService),
            nameof(IZoneMover),
            services.ZoneMover,
            services.PlayCardService.RuntimeZoneMover);
        AddMismatch(
            issues,
            nameof(DigivolveService),
            nameof(IZoneMover),
            services.ZoneMover,
            services.DigivolveService.RuntimeZoneMover);
        AddMismatch(
            issues,
            nameof(SecurityCheckService),
            nameof(IZoneMover),
            services.ZoneMover,
            services.SecurityCheckService.RuntimeZoneMover);
        AddMismatch(
            issues,
            nameof(TriggerPipelineService),
            nameof(Tier1PrimitiveService),
            services.PrimitiveService,
            services.TriggerPipelineService.RuntimePrimitiveService);
        AddMismatch(
            issues,
            nameof(PlayCardService),
            nameof(Tier1PrimitiveService),
            services.PrimitiveService,
            services.PlayCardService.RuntimePrimitiveService);
        AddMismatch(
            issues,
            nameof(PlayCardService),
            nameof(TriggerPipelineService),
            services.TriggerPipelineService,
            services.PlayCardService.RuntimeTriggerPipelineService);
        AddMismatch(
            issues,
            nameof(DigivolveService),
            nameof(TriggerPipelineService),
            services.TriggerPipelineService,
            services.DigivolveService.RuntimeTriggerPipelineService);
        AddMismatch(
            issues,
            nameof(AttackService),
            nameof(TriggerPipelineService),
            services.TriggerPipelineService,
            services.AttackService.RuntimeTriggerPipelineService);
        AddMismatch(
            issues,
            nameof(PhaseRunner),
            nameof(TriggerPipelineService),
            services.TriggerPipelineService,
            services.PhaseRunner.RuntimeTriggerPipelineService);
        AddMismatch(
            issues,
            nameof(RuleProcessor),
            nameof(TriggerPipelineService),
            services.TriggerPipelineService,
            services.RuleProcessor.RuntimeTriggerPipelineService);
        AddMismatch(
            issues,
            nameof(Tier1PrimitiveService),
            nameof(SecurityCheckService),
            services.SecurityCheckService,
            services.PrimitiveService.RuntimeSecurityCheckService);
        AddMismatch(
            issues,
            nameof(Tier1PrimitiveService),
            nameof(PlayCardService),
            services.PlayCardService,
            services.PrimitiveService.RuntimePlayCardService);
        AddMismatch(
            issues,
            nameof(Tier1PrimitiveService),
            nameof(DigivolveService),
            services.DigivolveService,
            services.PrimitiveService.RuntimeDigivolveService);
        AddMismatch(
            issues,
            nameof(AttackService),
            nameof(SecurityCheckService),
            services.SecurityCheckService,
            services.AttackService.RuntimeSecurityCheckService);
        AddMissing(
            issues,
            nameof(SecurityCheckService),
            nameof(SecurityEffectExecutionService),
            services.SecurityCheckService.RuntimeSecurityEffectExecutionService);
        if (services.SecurityCheckService.RuntimeSecurityEffectExecutionService is { } securityEffectExecutionService)
        {
            AddMismatch(
                issues,
                nameof(SecurityEffectExecutionService),
                nameof(Tier1PrimitiveService),
                services.PrimitiveService,
                securityEffectExecutionService.RuntimePrimitiveService);
        }

        AddMismatch(
            issues,
            nameof(TurnRunner),
            nameof(PhaseRunner),
            services.PhaseRunner,
            services.TurnRunner.RuntimePhaseRunner);
        AddMismatch(
            issues,
            nameof(RuleProcessor),
            nameof(PhaseRunner),
            services.PhaseRunner,
            services.RuleProcessor.RuntimePhaseRunner);
        AddMismatch(
            issues,
            nameof(ActionExecutor),
            nameof(PlayCardService),
            services.PlayCardService,
            services.ActionExecutor.RuntimePlayCardService);
        AddMismatch(
            issues,
            nameof(ActionExecutor),
            nameof(DigivolveService),
            services.DigivolveService,
            services.ActionExecutor.RuntimeDigivolveService);
        AddMismatch(
            issues,
            nameof(ActionExecutor),
            nameof(AttackService),
            services.AttackService,
            services.ActionExecutor.RuntimeAttackService);
        AddMismatch(
            issues,
            nameof(ActionExecutor),
            nameof(PhaseRunner),
            services.PhaseRunner,
            services.ActionExecutor.RuntimePhaseRunner);
        AddMismatch(
            issues,
            nameof(ActionExecutor),
            nameof(RuleProcessor),
            services.RuleProcessor,
            services.ActionExecutor.RuntimeRuleProcessor);

        return new BattleEngineServiceGraphValidationReport(issues);
    }

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
        primitiveService.AttachRuntimeServices(securityCheckService, playCardService, digivolveService);
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
            securityCheckService,
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

    private static void AddMismatch(
        List<BattleEngineServiceGraphIssue> issues,
        string serviceName,
        string dependencyName,
        object? expected,
        object? actual)
    {
        if (expected is not null && actual is not null && ReferenceEquals(expected, actual))
        {
            return;
        }

        issues.Add(new BattleEngineServiceGraphIssue(
            serviceName,
            dependencyName,
            "Dependency is not the shared runtime graph instance."));
    }
}
