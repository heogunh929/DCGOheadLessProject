namespace DCGO.RL.Engine.Domain;

public sealed class UnsupportedMechanicException : DomainException
{
    public UnsupportedMechanicException(string mechanic)
        : base($"Mechanic '{mechanic}' is not supported by the current RL.Engine implementation.")
    {
        Mechanic = mechanic;
    }

    public string Mechanic { get; }
}
