// Create an instance of this app with some pre-configured defaults
// Including the Kestrel Server - start Kestrel Server by executing dotnet run
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

app.Run();

// End of Middleware
///////////////////////////////////////////////////////////////////////////////////////
