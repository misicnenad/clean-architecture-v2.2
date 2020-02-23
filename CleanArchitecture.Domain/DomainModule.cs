using Autofac;

using CleanArchitecture.Domain.Commands;

using MediatR;

using System.Reflection;

namespace CleanArchitecture.Domain
{
    public class DomainModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(CreateUserHandler).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
