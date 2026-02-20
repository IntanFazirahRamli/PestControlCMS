var builder = WebApplication.CreateBuilder(args);

// ── Blazor Interactive Auto ────────────────────────────────────────────────
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// ── HttpClient for Blazor components ──────────────────────────────────────
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7001/") });

// ── EF Core / SQL Server ───────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("PestControlCMS.Infrastructure")));

// ── Repositories & Services ────────────────────────────────────────────────
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<ContractService>();
builder.Services.AddScoped<IValidator<CreateContractRequest>, CreateContractValidator>();

// ── Minimal API / OpenAPI ──────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new() { Title = "PestControl CMS API", Version = "v1" }));

var app = builder.Build();

// ── Middleware ─────────────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// ── Minimal API endpoints ──────────────────────────────────────────────────
app.MapContractEndpoints();

// ── Blazor ────────────────────────────────────────────────────────────────
app.MapRazorComponents<PestControlCMS.Web.Components.App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

// ── Auto-migrate on startup (dev only) ────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

await app.RunAsync();
