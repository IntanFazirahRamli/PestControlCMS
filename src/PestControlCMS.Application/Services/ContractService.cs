namespace PestControlCMS.Application.Services;

/// <summary>
/// Application service orchestrating contract CRUD operations.
/// Uses the repository pattern and returns <see cref="Result{T}"/> for all operations.
/// </summary>
public sealed class ContractService(
    IContractRepository repository,
    IValidator<CreateContractRequest> createValidator)
{
    /// <summary>Retrieves all contracts.</summary>
    public async Task<Result<IReadOnlyList<ContractResponse>>> GetAllAsync(CancellationToken ct = default)
    {
        var contracts = await repository.GetAllAsync(ct);
        return Result<IReadOnlyList<ContractResponse>>.Success(
            contracts.Select(ContractResponse.FromEntity).ToList());
    }

    /// <summary>Retrieves a single contract by ID.</summary>
    public async Task<Result<ContractResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var contract = await repository.GetByIdAsync(id, ct);
        return contract is null
            ? Result<ContractResponse>.Failure($"Contract {id} not found.")
            : Result<ContractResponse>.Success(ContractResponse.FromEntity(contract));
    }

    /// <summary>Creates a new contract after validation.</summary>
    public async Task<Result<ContractResponse>> CreateAsync(
        CreateContractRequest request, CancellationToken ct = default)
    {
        var validation = await createValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
            return Result<ContractResponse>.Failure(
                string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

        var entity = new PestControlContract
        {
            CustomerName = request.CustomerName,
            PropertyAddress = request.PropertyAddress,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            AnnualFee = request.AnnualFee,
            ServiceFrequency = request.ServiceFrequency,
            PestTypes = request.PestTypes,
            SpecialInstructions = request.SpecialInstructions
        };

        var created = await repository.CreateAsync(entity, ct);
        return Result<ContractResponse>.Success(ContractResponse.FromEntity(created));
    }

    /// <summary>Updates an existing contract.</summary>
    public async Task<Result<ContractResponse>> UpdateAsync(
        UpdateContractRequest request, CancellationToken ct = default)
    {
        var existing = await repository.GetByIdAsync(request.Id, ct);
        if (existing is null)
            return Result<ContractResponse>.Failure($"Contract {request.Id} not found.");

        existing.CustomerName = request.CustomerName;
        existing.PropertyAddress = request.PropertyAddress;
        existing.StartDate = request.StartDate;
        existing.EndDate = request.EndDate;
        existing.AnnualFee = request.AnnualFee;
        existing.ServiceFrequency = request.ServiceFrequency;
        existing.PestTypes = request.PestTypes;
        existing.SpecialInstructions = request.SpecialInstructions;
        existing.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(existing, ct);
        return Result<ContractResponse>.Success(ContractResponse.FromEntity(existing));
    }

    /// <summary>Deletes a contract by ID.</summary>
    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var deleted = await repository.DeleteAsync(id, ct);
        return deleted
            ? Result.Success()
            : Result.Failure($"Contract {id} not found.");
    }
}
