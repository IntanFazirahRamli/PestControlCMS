namespace PestControlCMS.Application.Contracts;

/// <summary>DTO for creating a new pest control contract.</summary>
public sealed record CreateContractRequest(
    string CustomerName,
    string PropertyAddress,
    DateTime StartDate,
    DateTime EndDate,
    decimal AnnualFee,
    ServiceFrequency ServiceFrequency,
    PestType PestTypes,
    string? SpecialInstructions
);
