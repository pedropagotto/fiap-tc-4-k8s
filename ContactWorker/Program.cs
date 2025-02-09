using System.Reflection;
using Application.Services;
using ContactWorker;
using ContactWorker.Config;
using ContactWorker.Events;
using Domain.Abstractions;
using Infra;
using Infra.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var config = new ConfigurationBuilder()
    .SetBasePath(Environment.CurrentDirectory)
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
    .AddEnvironmentVariables()
    .Build();

var configuration = config.GetSection("RabbitMQConnection").Get<ConsumerConfiguration>();

var builder = Host.CreateApplicationBuilder(args);

builder.AddNpgsqlDbContext<AppDbContext>("techchallenge01");

builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IConfiguration>(config);
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactRepository, ConctactRepository>();
builder.Services.AddSingleton<IElasticClient, ElasticClient>();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    
    x.AddConsumers(typeof(Program).Assembly);

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(configuration.Host), h =>
        {
            h.Username(configuration.Username);
            h.Password(configuration.Password);
        });
        
        cfg.ReceiveEndpoint("create-contact", e =>
        {
            e.ConfigureConsumer<CreateContactConsumer>(context);
        });
        
        cfg.ReceiveEndpoint("update-contact", e =>
        {
            e.ConfigureConsumer<UpdateContactConsumer>(context);
        });
        
        cfg.ReceiveEndpoint("remove-contact", e =>
        {
            e.ConfigureConsumer<DeleteContactConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
        
        cfg.UseCircuitBreaker(cb =>
        {
            cb.ActiveThreshold = 5;
            cb.TrackingPeriod = TimeSpan.FromSeconds(10);
            cb.ResetInterval = TimeSpan.FromMinutes(1);
        });
    });
});

var host = builder.Build();
host.Run();
