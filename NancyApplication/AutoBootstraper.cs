using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;

namespace NancyApplication
{
    internal sealed class AutoBootstraper : AutofacNancyBootstrapper
    {
        private readonly IServiceCollection _services;

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.EnableBasicAuthentication(new BasicAuthenticationConfiguration(
                container.Resolve<IUserValidator>(),
                "MyRealm"));
        }

        public AutoBootstraper(IServiceCollection services)
        {
            _services = services;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            base.ConfigureApplicationContainer(container);

            container.Update(builder =>
            {
                builder.RegisterType<UserValidator>().As<IUserValidator>();
                builder.Populate(_services);
            });
        }
    }
}