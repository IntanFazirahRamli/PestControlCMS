namespace PestControlCMS.Application.Contracts;

/// <summary>DTO for updating an existing pest control contract.</summary>
public sealed record UpdateContractRequest(
    Guid Id,
    string CustomerName,
    string PropertyAddress,
    DateTime StartDate,
    DateTime EndDate,
    decimal AnnualFee,
    ServiceFrequency ServiceFrequency,
    PestType PestTypes,
    string? SpecialInstructions
);
