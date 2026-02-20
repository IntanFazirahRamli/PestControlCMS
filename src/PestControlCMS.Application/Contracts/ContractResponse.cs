namespace PestControlCMS.Application.Contracts;

/// <summary>Outbound DTO representing a persisted contract.</summary>
public sealed record ContractResponse(
    Guid Id,
    string CustomerName,
    string PropertyAddress,
    DateTime StartDate,
    DateTime EndDate,
    decimal AnnualFee,
    ServiceFrequency ServiceFrequency,
    PestType PestTypes,
    string? SpecialInstructions,
    DateTime CreatedAt,
    DateTime UpdatedAt
)
{
    /// <summary>Maps a domain entity to a response DTO.</summary>
    public static ContractResponse FromEntity(PestControlContract c) => new(
        c.Id, c.CustomerName, c.PropertyAddress,
        c.StartDate, c.EndDate, c.AnnualFee,
        c.ServiceFrequency, c.PestTypes,
        c.SpecialInstructions, c.CreatedAt, c.UpdatedAt
    );
}
