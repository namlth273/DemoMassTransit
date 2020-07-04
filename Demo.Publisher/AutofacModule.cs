using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Publisher
{
    internal class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddMassTransit(massBuilder =>
            {
                massBuilder.AddBus(context =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(rabbitConfig =>
                    {
                        rabbitConfig.Host("localhost", "/", h =>
                        {
                            h.Username("admin");
                            h.Password("admin");
                        });

                        rabbitConfig.ConfigureEndpoints(context);

                        // rabbitConfig.Message<IUserCreated>(topologyConfig =>
                        // {
                        //     topologyConfig.SetEntityName("DemoExchange");
                        // });

                        // rabbitConfig.Publish<IUserCreated>(topologyConfig =>
                        // {
                        //     topologyConfig.ExchangeType = ExchangeType.Topic;
                        //     // topologyConfig.BindQueue("DemoExchange", "DemoQueue",
                        //     //     queueConfig => { queueConfig.ExchangeType = ExchangeType.Topic; });
                        // });

                        // rabbitConfig.ReceiveEndpoint("DemoQueue", config =>
                        // {
                        //     // config.ExchangeType = ExchangeType.Topic;
                        //     config.PrefetchCount = 1;
                        //     config.Bind("DemoExchange",
                        //         exchangeConfig => { exchangeConfig.ExchangeType = ExchangeType.Topic; });
                        //
                        //     config.Consumer<UserCreatedConsumer>();
                        // });
                    });
                });
            });

            var services = new ServiceCollection();

            services.AddHostedService<DemoHostedService>();

            builder.Populate(services);
        }
    }
}