using HomeWard.Application.Auth;
using HomeWard.Application.Repositories;
using HomeWard.Infrastructure.Auth;
using HomeWard.Infrastructure.Persistence;
using HomeWard.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<HomeWardDbContext>(
    options =>
        options.UseNpgsql(
            builder.Configuration
                .GetConnectionString("Default")));

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"### JWT Auth FAILED on '{context.Request.Path}'");
                Console.WriteLine($"### Exception: {context.Exception.GetType().Name} - {context.Exception.Message}");
                Console.WriteLine($"### Auth header raw: '{context.Request.Headers.Authorization}'");
                Console.WriteLine($"### Auth header count: {context.Request.Headers.Authorization.Count}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("### JWT Token VALIDATED successfully");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"### JWT Challenge: {context.Error} - {context.ErrorDescription}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // UI disponível em /scalar/v1
}


app.Use(async (context, next) =>
{
    Console.WriteLine($">>> BEFORE AUTH: {context.Request.Path}");
    await next();
});

app.UseAuthentication();

app.Use(async (context, next) =>
{
    var result = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
    Console.WriteLine($">>> Manual AuthenticateAsync - Succeeded: {result.Succeeded}");
    if (!result.Succeeded)
    {
        Console.WriteLine($">>> Failure: {result.Failure?.Message}");
    }
    await next();
});

app.Use(async (context, next) =>
{
    Console.WriteLine($">>> AFTER AUTH: {context.Request.Path}, IsAuth: {context.User.Identity?.IsAuthenticated}");
    await next();
});

app.Use(async (context, next) =>
{
    Console.WriteLine($"=== Request: {context.Request.Method} {context.Request.Path} ===");
    Console.WriteLine($"Auth header: {context.Request.Headers["Authorization"]}");
    Console.WriteLine($"User authenticated: {context.User.Identity?.IsAuthenticated}");
    Console.WriteLine($"User claims: {string.Join(", ", context.User.Claims.Select(c => $"{c.Type}={c.Value}"))}");
    await next();
    Console.WriteLine($"=== Response status: {context.Response.StatusCode} ===");
});
app.UseAuthorization();
app.MapControllers();

app.Run();

// Transformer que adiciona o esquema Bearer ao documento OpenAPI
internal sealed class BearerSecuritySchemeTransformer(
    IAuthenticationSchemeProvider authenticationSchemeProvider
) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();

        if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
        {
            var bearerScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            };

            document.Components ??= new OpenApiComponents();
            document.AddComponent("Bearer", bearerScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            };

            foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
            {
                operation.Value.Security ??= new List<OpenApiSecurityRequirement>();
                operation.Value.Security.Add(securityRequirement);
            }
        }
    }
}