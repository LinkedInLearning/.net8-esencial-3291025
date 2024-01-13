using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Wpm.Web.Dal;
using Wpm.Web.Handlers;
using Wpm.Web.Identity;
using Wpm.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenTelemetry()
    .WithMetrics(builder =>
    {
        builder.AddPrometheusExporter();

        builder.AddMeter("Microsoft.AspNetCore.Hosting",
                         "Microsoft.AspNetCore.Server.Kestrel");
        builder.AddView("http.server.request.duration",
            new ExplicitBucketHistogramConfiguration
            {
                Boundaries = [ 0, 0.005, 0.01, 0.025, 0.05,
                       0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 ]
            });
    });

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddInMemoryWpmDb();
builder.Services.AddKeyedSingleton<IStorageService, AzureStorageService>("Azure");
builder.Services.AddKeyedSingleton<IStorageService, AwsStorageService>("Aws");
builder.Services.AddDbContext<WpmIdentityDbContext>(options =>
{
    options.UseInMemoryDatabase("WpmIdentity"); 
});
builder.Services.AddIdentityApiEndpoints<WpmIdentityUser>()
                .AddEntityFrameworkStores<WpmIdentityDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<WpmExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();
app.Services.EnsureWpmDbIsCreated();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.MapGroup("account")
   .MapIdentityApi<WpmIdentityUser>();

app.MapGet("/api/pets", async (WpmDbContext db, int count = 1) =>
{
    var pets = await db.Pets
    .Include(p => p.Breed)
       .ThenInclude(b => b.Species)
    .Take(count)
    .ToListAsync();
    return pets;
}).RequireAuthorization();

app.Run();