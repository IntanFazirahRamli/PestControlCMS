namespace PestControlCMS.Core.Interfaces;

/// <summary>Persistence contract for <see cref="PestControlContract"/> aggregate root.</summary>
public interface IContractRepository
{
    /// <summary>Returns all contracts, ordered by creation date descending.</summary>
    Task<IReadOnlyList<PestControlContract>> GetAllAsync(CancellationToken ct = default);

    /// <summary>Returns the contract with the specified <paramref name="id"/>, or <c>null</c>.</summary>
    Task<PestControlContract?> GetByIdAsync(Guid id, CancellationToken ct = default);

    /// <summary>Persists a new contract and returns the saved entity.</summary>
    Task<PestControlContract> CreateAsync(PestControlContract contract, CancellationToken ct = default);

    /// <summary>Applies changes to an existing contract.</summary>
    Task<bool> UpdateAsync(PestControlContract contract, CancellationToken ct = default);

    /// <summary>Removes the contract with <paramref name="id"/>. Returns <c>false</c> if not found.</summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
