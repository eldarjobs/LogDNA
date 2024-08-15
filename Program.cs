using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.LogDNA; // LogDNA sink üçün namespace
using LogDNA.Data; // DbContext üçün namespace

var builder = WebApplication.CreateBuilder(args);

// Serilog konfiqurasiyası
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console() // Console sink-i əlavə edin
    .WriteTo.LogDNA("50d2b7121f1ff8d3e3e83948aba45839")  // Mezmo (LogDNA) üçün sink
    .CreateLogger();

builder.Host.UseSerilog();

// Verilənlər bazası konfiqurasiyası
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecondAppContext")
        ?? throw new InvalidOperationException("Connection string 'SecondAppContext' not found.")));

// Razor Pages xidmətlərinin əlavə edilməsi
builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Razor Pages üçün route-ların xəritələnməsi
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
