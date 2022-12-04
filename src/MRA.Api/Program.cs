using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using MRA.Application;
using MRA.Application.Middleware;
using MRA.CrossCuttingConcerns.Identity;
using MRA.CrossCuttingConcerns.OS;
using MRA.Domain.Services.Configuration;
using MRA.Infrastructure;
using MRA.Infrastructure.Identity;
using MRA.Infrastructure.OS;
using MRA.Persistence;
using MRA.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", builder => builder
        .WithOrigins(appSettings.CORS.AllowedOrigins)
        .AllowAnyMethod()
        .AllowAnyHeader());

    options.AddPolicy("AllowAnyOrigin", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    options.AddPolicy("CustomPolicy", builder => builder
        .AllowAnyOrigin()
        .WithMethods("Get")
        .WithHeaders("Content-Type"));
});
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>(name: "Application DB Context", failureStatus: HealthStatus.Degraded)
    .AddUrlGroup(new Uri(appSettings.ApplicationDetail.ContactWebsite), name: "My personal website", failureStatus: HealthStatus.Degraded)
    .AddSqlServer(builder.Configuration.GetConnectionString("MRAConnectionString"));
builder.Services.AddHealthChecksUI(setupSettings: setup =>
{
    setup.AddHealthCheckEndpoint("Basic Health Check", $"/healthz");
}).AddInMemoryStorage();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MRA.Api v1",
    });
});
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});
builder.Services.AddFeatureManagement();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MRA.Api v1"));
app.UseHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    },
}).UseHealthChecksUI(setup =>
{
    setup.ApiPath = "/healthcheck";
    setup.UIPath = "/healthcheck-ui";
});
app.UseCustomExceptionHandler();
app.UseCors("Open");
app.UseCors(appSettings.CORS.AllowAnyOrigin ? "AllowAnyOrigin" : "AllowedOrigins");
app.UseEndpoints(endpoints =>
{
    var x = app.Environment.EnvironmentName;
    endpoints.MapControllers();
});

using (var scope = app.Services.CreateScope())
{
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    applicationDbContext.Database.Migrate();
}

app.Run();
