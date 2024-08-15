using LogDNA.Data; // Veritabanı kontekstinizi ehtiva edən namespace
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecondAppContext")
        ?? throw new InvalidOperationException("Connection string 'SecondAppContext' not found.")));

// Add any other services or configurations here if needed
// builder.Services.AddScoped<IMyService, MyService>(); // Example of adding a custom service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Add HTTP Strict Transport Security
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve static files

app.UseRouting();

app.UseAuthorization(); // Authorization middleware

app.MapRazorPages(); // Map Razor Pages routes

app.Run(); // Run the application
