using BackgroundWorkerService.Interfaces;

namespace BackgroundWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitServices _rabbitServices;

        public Worker(ILogger<Worker> logger, IRabbitServices rabbitServices)
        {
            _logger = logger;
            _rabbitServices = rabbitServices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //main code
            _rabbitServices.Start();


            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}