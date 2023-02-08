using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

// Create an instance of this app with some pre-configured defaults
// Including the Kestrel Server - start Kestrel Server by executing dotnet run

var builder = WebApplication.CreateBuilder(args);

///////////////////////////////////////////////////////////////////////////////////////
// Add services to the container.

// Adds services to our controllers and classes etc. 
// Uses dependency injection to add services to other classes and use their functionality 

builder.Services.AddControllers();

// Set up database context. StoreContext is the name of the class in StoreContext.cs
// opt => is a lambda. Execute commands in between {} based on opt
builder.Services.AddDbContext<StoreContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// End of Services
///////////////////////////////////////////////////////////////////////////////////////

var app = builder.Build();


///////////////////////////////////////////////////////////////////////////////////////
// Middleware

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// Get hold of the DbContext service and migrate db in code

// using keyword to create a scope
// DbContext is a scoped service
// this allows us to get hold of it here when we don't have the ability to inject a service directly

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    // Asynchronously applies any migrations to the db. Will create the db if non exists
    await context.Database.MigrateAsync(); 
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();

// End of Middleware
///////////////////////////////////////////////////////////////////////////////////////
