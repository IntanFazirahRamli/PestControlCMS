namespace PestControlCMS.Web.Endpoints;

/// <summary>
/// Registers the <c>/api/contracts</c> Minimal API endpoint group.
/// All endpoints return RFC 7807 Problem Details on failure.
/// </summary>
public static class ContractEndpoints
{
    /// <summary>Extension method to wire up all contract endpoints on the <paramref name="app"/>.</summary>
    public static WebApplication MapContractEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/contracts")
                       .WithTags("Contracts")
                       .WithOpenApi();

        // GET /api/contracts
        group.MapGet("/", async (ContractService svc, CancellationToken ct) =>
        {
            var result = await svc.GetAllAsync(ct);
            return Results.Ok(result.Value);
        })
        .WithName("GetAllContracts")
        .WithSummary("Returns all pest control contracts.");

        // GET /api/contracts/{id}
        group.MapGet("/{id:guid}", async (Guid id, ContractService svc, CancellationToken ct) =>
        {
            var result = await svc.GetByIdAsync(id, ct);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        })
        .WithName("GetContractById")
        .WithSummary("Returns a single contract by ID.");

        // POST /api/contracts
        group.MapPost("/", async (CreateContractRequest request, ContractService svc, CancellationToken ct) =>
        {
            var result = await svc.CreateAsync(request, ct);
            return result.IsSuccess
                ? Results.CreatedAtRoute("GetContractById", new { id = result.Value!.Id }, result.Value)
                : Results.UnprocessableEntity(new { error = result.Error });
        })
        .WithName("CreateContract")
        .WithSummary("Creates a new pest control contract.");

        // PUT /api/contracts/{id}
        group.MapPut("/{id:guid}", async (Guid id, UpdateContractRequest request, ContractService svc, CancellationToken ct) =>
        {
            if (id != request.Id)
                return Results.BadRequest(new { error = "Route ID and body ID must match." });

            var result = await svc.UpdateAsync(request, ct);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        })
        .WithName("UpdateContract")
        .WithSummary("Updates an existing pest control contract.");

        // DELETE /api/contracts/{id}
        group.MapDelete("/{id:guid}", async (Guid id, ContractService svc, CancellationToken ct) =>
        {
            var result = await svc.DeleteAsync(id, ct);
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        })
        .WithName("DeleteContract")
        .WithSummary("Deletes a pest control contract.");

        return app;
    }
}
