using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.Publisher
{
    internal class DemoHostedService : BackgroundService
    {
        private readonly ILogger<DemoHostedService> _logger;
        private readonly IPublishEndpoint _endpoint;

        public DemoHostedService(ILogger<DemoHostedService> logger, IPublishEndpoint endpoint)
        {
            _logger = logger;
            _endpoint = endpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var id = Guid.NewGuid();

                await _endpoint.Publish<IUserCreated>(new UserCreated
                {
                    CorrelationId = id,
                    FirstName = $"Nam {id}",
                    LastName = "Le"
                }, stoppingToken);

                //await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}