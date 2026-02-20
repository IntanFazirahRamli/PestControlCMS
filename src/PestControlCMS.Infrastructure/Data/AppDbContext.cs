namespace PestControlCMS.Infrastructure.Data;

/// <summary>
/// Entity Framework Core database context for the Pest Control CMS.
/// Applies value conversions for enum flags and configures table/column constraints.
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>The contracts table.</summary>
    public DbSet<PestControlContract> Contracts => Set<PestControlContract>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PestControlContract>(entity =>
        {
            entity.ToTable("PestControlContracts");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                  .ValueGeneratedNever(); // GUID assigned in domain

            entity.Property(e => e.CustomerName)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.PropertyAddress)
                  .IsRequired()
                  .HasMaxLength(500);

            entity.Property(e => e.AnnualFee)
                  .HasPrecision(18, 2);

            entity.Property(e => e.ServiceFrequency)
                  .HasConversion<string>()
                  .HasMaxLength(20);

            // Store PestType flags as int in DB
            entity.Property(e => e.PestTypes)
                  .HasConversion<int>();

            entity.Property(e => e.SpecialInstructions)
                  .HasMaxLength(1000);

            entity.Property(e => e.CreatedAt)
                  .IsRequired();

            entity.Property(e => e.UpdatedAt)
                  .IsRequired();

            entity.HasIndex(e => e.CustomerName);
            entity.HasIndex(e => e.StartDate);
        });
    }
}
