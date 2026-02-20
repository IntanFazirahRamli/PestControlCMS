namespace PestControlCMS.Core.Entities;

/// <summary>
/// Represents a pest control service contract between the company and a customer.
/// </summary>
public sealed class PestControlContract
{
    /// <summary>Unique identifier for the contract (GUID).</summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Full legal name of the customer.</summary>
    public required string CustomerName { get; set; }

    /// <summary>Physical address of the property to be serviced.</summary>
    public required string PropertyAddress { get; set; }

    /// <summary>Date the contract becomes active.</summary>
    public required DateTime StartDate { get; set; }

    /// <summary>Date the contract expires. Must be after <see cref="StartDate"/>.</summary>
    public required DateTime EndDate { get; set; }

    /// <summary>Total annual fee charged for the contracted services.</summary>
    public required decimal AnnualFee { get; set; }

    /// <summary>How often service visits are scheduled.</summary>
    public required ServiceFrequency ServiceFrequency { get; set; }

    /// <summary>
    /// Bit-flagged combination of pest types covered under this contract.
    /// </summary>
    public required PestType PestTypes { get; set; }

    /// <summary>Optional notes or special handling instructions for service technicians.</summary>
    public string? SpecialInstructions { get; set; }

    /// <summary>Timestamp when the record was created (UTC).</summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    /// <summary>Timestamp of the last update (UTC).</summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
