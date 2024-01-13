using Microsoft.EntityFrameworkCore;
using Wpm.Web.Dal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddInMemoryWpmDb();

var app = builder.Build();

app.Services.EnsureWpmDbIsCreated();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.MapGet("/api/pets", async (WpmDbContext db, int count = 1) =>
{
    var pets = await db.Pets
    .Include(p => p.Breed)
       .ThenInclude(b => b.Species)
    .Take(count)
    .ToListAsync();
    return pets;
});

app.Run();