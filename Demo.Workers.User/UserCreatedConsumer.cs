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

        public Task Consume(ConsumeContext<IUserCreated> context)
        {
            _logger.Information("FirstName: {0}", context.Message.FirstName);
            return Task.CompletedTask;
        }
    }
}