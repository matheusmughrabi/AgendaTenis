using AgendaTenis.BuildingBlocks.EventBus.Configuracao;
using AgendaTenis.BuildingBlocks.EventBus.Servicos;
using AgendaTenis.Core.Jogadores.AcessoDados;
using AgendaTenis.Core.Jogadores.Eventos.Consumidores;
using AgendaTenis.Workers.EventBus.Consumidores;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.AddHostedService<PlacarConfirmadoWorker>();
        services.AddSingleton<PlacarConfirmadoConsumidor>();
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.Load("AgendaTenis.Core.Jogadores")));
        services.Configure<RabbitMQConfiguracao>(configuration.GetSection("RabbitMQ"));
        services.AddSingleton<IMessageBus, RabbitMessageBus>();
        services.AddDbContext<JogadoresDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Jogadores")));
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "agendaTenis";
        });
    })
    .Build();

await host.RunAsync();
