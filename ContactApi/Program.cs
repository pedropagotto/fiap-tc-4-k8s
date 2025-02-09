using API.Config;
using API.Middlewares;
using Application.Mappings;
using AutoMapper;
using Common.Config;
using Infra;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Prometheus.SystemMetrics;
using System.Reflection;
using ContactApi.Extensions;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var config = new ConfigurationBuilder()
    .SetBasePath(Environment.CurrentDirectory)
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
    .AddEnvironmentVariables()
    .Build();

var configuration = config.GetSection("Values:TechChallenge").Get<TechChallengeFiapConfiguration>();

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<AppDbContext>("techchallenge01");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.JwtConfig(configuration);
builder.Services.AddSwaggerConfig();
builder.Services.AddCorsConfig();
builder.AddMassTransitConfig();

//AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AllowNullCollections = true;
    mc.AllowNullDestinationValues = true;
    mc.AddMaps(typeof(UserMapping).Assembly);
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddSingleton<ITechChallengeFiapConfiguration>(prop => configuration);
builder.Services.AddDependencyInjectionConfig();

builder.Services.AddSystemMetrics();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpMetrics();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapMetrics();
app.MapControllers();

app.Run();


public partial class Program { }
