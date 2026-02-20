namespace PestControlCMS.Infrastructure.Repositories;

/// <summary>
/// EF Core implementation of <see cref="IContractRepository"/>.
/// </summary>
public sealed class ContractRepository(AppDbContext db) : IContractRepository
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<PestControlContract>> GetAllAsync(CancellationToken ct = default) =>
        await db.Contracts
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(ct);

    /// <inheritdoc/>
    public async Task<PestControlContract?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await db.Contracts.FindAsync([id], ct);

    /// <inheritdoc/>
    public async Task<PestControlContract> CreateAsync(PestControlContract contract, CancellationToken ct = default)
    {
        db.Contracts.Add(contract);
        await db.SaveChangesAsync(ct);
        return contract;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(PestControlContract contract, CancellationToken ct = default)
    {
        db.Contracts.Update(contract);
        var rows = await db.SaveChangesAsync(ct);
        return rows > 0;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var rows = await db.Contracts
                           .Where(c => c.Id == id)
                           .ExecuteDeleteAsync(ct);
        return rows > 0;
    }
}
