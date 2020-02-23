using Autofac;

using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infrastructure.Models;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ExternalIdNumberValidationService>().As<IIdNumberValidationService>();

            builder.Register(c =>
            {
                var options = new DbContextOptionsBuilder<CleanArchitectureDbContext>()
                      .UseInMemoryDatabase("UserDb")
                      .Options;

                return new CleanArchitectureDbContext(options);
            }).InstancePerLifetimeScope();
        }
    }
}
