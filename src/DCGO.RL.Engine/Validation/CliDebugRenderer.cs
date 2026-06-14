using System.Text;

namespace DCGO.RL.Engine.Validation;

public sealed class CliDebugRenderer
{
    public string RenderScenarioResult(ScenarioResult result)
    {
        var builder = new StringBuilder();
        builder.Append("Scenario: ").Append(result.ScenarioName).AppendLine();
        builder.Append("Status: ").Append(result.Status).AppendLine();
        builder.Append("FinalHash: ").Append(result.FinalStateHash).AppendLine();
        builder.Append("TraceEvents: ").Append(result.Trace.Events.Count).AppendLine();
        builder.Append("InvariantChecks: ").Append(result.InvariantReports.Count).AppendLine();

        if (result.MaxTurnAbort is not null)
        {
            builder.Append("MaxActions: ").Append(result.MaxTurnAbort.MaxActions).AppendLine();
            builder.Append("ActionsExecuted: ").Append(result.MaxTurnAbort.ActionsExecuted).AppendLine();
        }

        return builder.ToString();
    }

    public string RenderTrace(GameTrace trace)
    {
        var builder = new StringBuilder();
        foreach (var traceEvent in trace.Events)
        {
            builder
                .Append(traceEvent.Index)
                .Append(' ')
                .Append(traceEvent.Kind)
                .Append(' ')
                .Append(traceEvent.Label)
                .Append(' ')
                .Append(traceEvent.StateHashAfter)
                .AppendLine();
        }

        return builder.ToString();
    }
}
