using System;
using System.Threading.Tasks;
using Demo.Contracts;
using MassTransit;
using Serilog;

namespace Demo.Workers.User
{
    public class UserCreatedConsumer : IConsumer<IUserCreated>
    {
        private readonly ILogger _logger;

        public UserCreatedConsumer(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IUserCreated> context)
        {
            await Task.Delay(TimeSpan.FromSeconds(10));

            _logger.Information("FirstName: {0}", context.Message.FirstName);
        }
    }
}