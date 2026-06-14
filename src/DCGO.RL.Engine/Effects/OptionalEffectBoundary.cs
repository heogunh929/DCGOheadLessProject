using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public sealed class OptionalEffectBoundary
{
    public SelectionRequest CreateRequest(EffectDescriptor descriptor, EffectContext context)
    {
        var player = descriptor.Controller
            ?? context.Player
            ?? throw new DomainException($"Optional effect '{descriptor.StableId}' requires a controller/player.");

        return new SelectionRequest(
            id: $"optional:{descriptor.StableId}:{context.Timing}",
            player: player,
            selectionKind: SelectionKind.SelectYesNo,
            targetKind: SelectionTargetKind.Boolean,
            minCount: 1,
            maxCount: 1,
            canSkip: false,
            canEndNotMax: false,
            candidates: new[]
            {
                new SelectableTarget(SelectionTargetKind.Boolean, "yes", player, Label: "Use", OptionValue: "true"),
                new SelectableTarget(SelectionTargetKind.Boolean, "no", player, Label: "Skip", OptionValue: "false"),
            },
            prompt: $"Use optional effect {descriptor.StableId}");
    }
}
