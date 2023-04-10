using BackgroundWorkerService;
using BackgroundWorkerService.Interfaces;
using BackgroundWorkerService.Utils;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddSingleton<IDBServices, DBServices>();
        services.AddSingleton<IRabbitServices, RabbitServices>();
    })
    .Build();

await host.RunAsync();
