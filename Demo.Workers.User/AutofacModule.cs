using Autofac;
using Autofac.Extensions.DependencyInjection;
using Demo.Contracts.Configs;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.MassTransit;

namespace Demo.Workers.User
{
    internal class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(r =>
            {
                var rabbitConfig = new RabbitMqConfig();
                var configuration = r.Resolve<IConfiguration>();
                configuration.GetSection("RabbitMq").Bind(rabbitConfig);

                return rabbitConfig;
            }).As<IRabbitMqConfig>();

            builder.AddMassTransit(massBuilder =>
            {
                massBuilder.UsingRabbitMq((context, config) =>
                {
                    var rabbitConfig = context.GetService<IRabbitMqConfig>();

                    config.Host(rabbitConfig.Host, rabbitConfig.VirtualHost, h =>
                    {
                        h.Username(rabbitConfig.Username);
                        h.Password(rabbitConfig.Password);
                    });

                    config.ConfigureEndpoints(context);
                });

                massBuilder.AddConsumerWithDefaultEndpoint<UserCreatedConsumer>();

                // massBuilder.AddBus(context =>
                // {
                //     return Bus.Factory.CreateUsingRabbitMq(rabbitConfig =>
                //     {
                //         rabbitConfig.Host("localhost", "/", h =>
                //         {
                //             h.Username("admin");
                //             h.Password("admin");
                //         });
                //
                //         rabbitConfig.ConfigureEndpoints(context);
                //     });
                // });
            });

            var services = new ServiceCollection();

            services.AddMassTransitHostedService();

            builder.Populate(services);
        }
    }
}