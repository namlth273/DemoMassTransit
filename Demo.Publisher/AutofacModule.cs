using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shared.Console.Common;
using Shared.MassTransit;

namespace Demo.Publisher
{
    internal class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterRabbitMqConfig();

            builder.AddMassTransit(massBuilder => { massBuilder.UseRabbitMq(); });

            var services = new ServiceCollection();

            services.AddHostedService<DemoHostedService>();

            builder.Populate(services);
        }
    }
}