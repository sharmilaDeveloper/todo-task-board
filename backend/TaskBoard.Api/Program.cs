using TaskBoard.Api.Middleware; 
using TaskBoard.Application;
using TaskBoard.Infrastructure;
using TaskBoard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Api.Middleware.ExceptionMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Remove(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", policy => {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:4200");
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await DbInitializer.SeedAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration or seeding.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>(); 
app.UseHttpsRedirection();
app.UseCors("CorsPolicy"); 
app.UseAuthorization();
try
{
    app.MapControllers();
}
catch (System.Reflection.ReflectionTypeLoadException ex)
{
    foreach (var loaderEx in ex.LoaderExceptions)
    {
        System.Diagnostics.Debug.WriteLine($"!!! LOADER ERROR: {loaderEx?.Message}");
    }
    throw;
}

app.Run();
