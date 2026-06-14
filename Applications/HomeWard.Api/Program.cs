using HomeWard.Application.Auth;
using HomeWard.Application.Repositories;
using HomeWard.Infrastructure.Auth;
using HomeWard.Infrastructure.Persistence;
using HomeWard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<HomeWardDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<HomeWardDbContext>(
    options =>
        options.UseNpgsql(
            builder.Configuration
                .GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        // Point to the default .NET 10 native OpenAPI route
        options.SwaggerEndpoint("/openapi/v1.json", "API v1");

        // Optional: Serve Swagger UI at the application root (localhost:XXXX/)
        // options.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
