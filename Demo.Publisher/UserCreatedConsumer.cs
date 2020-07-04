using System.Threading.Tasks;
using Demo.Contracts;
using MassTransit;

namespace Demo.Publisher
{
    public class UserCreatedConsumer : IConsumer<IUserCreated>
    {
        private readonly Serilog.ILogger _logger;

        public UserCreatedConsumer(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IUserCreated> context)
        {
            _logger.Information("FirstName: {0}", context.Message.FirstName);
            return Task.CompletedTask;
        }
    }

    public class UserCreated2Consumer : IConsumer<IUserCreated>
    {
        private readonly Serilog.ILogger _logger;

        public UserCreated2Consumer(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IUserCreated> context)
        {
            _logger.Information("LastName: {0}", context.Message.LastName);
            return Task.CompletedTask;
        }
    }
}