using BlazingPizza.Data;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Services;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<OrderState>();

// Register PizzaStoreContext with SQLite
builder.Services.AddDbContext<PizzaStoreContext>(options =>
    options.UseSqlite("Data Source=blazingpizza.db"));

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Map controllers (so /orders works)
app.MapControllers();

app.Run();

