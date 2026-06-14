using System.Globalization;

namespace DCGO.RL.Engine.Domain;

public readonly record struct PlayerId(int Value)
{
    public static PlayerId Player0 { get; } = new(0);
    public static PlayerId Player1 { get; } = new(1);

    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}

public readonly record struct CardInstanceId(int Value)
{
    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}

public readonly record struct PermanentId(int Value)
{
    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}
