namespace PestControlCMS.Core.Enums;

/// <summary>
/// Bit-flag enumeration of pest types covered under a contract.
/// Multiple values can be combined using bitwise OR.
/// </summary>
[Flags]
public enum PestType
{
    /// <summary>No pest types selected.</summary>
    None = 0,

    /// <summary>Rats, mice, and other rodents.</summary>
    Rodents = 1 << 0,

    /// <summary>Subterranean and drywood termites.</summary>
    Termites = 1 << 1,

    /// <summary>Ants, cockroaches, spiders, and general insects.</summary>
    Insects = 1 << 2,

    /// <summary>Bed bug treatment and prevention.</summary>
    Bedbugs = 1 << 3
}
