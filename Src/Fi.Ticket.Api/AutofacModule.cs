using Autofac;
using Fi.Ticket.Api.Persistence;
using Fi.Persistence.Relational.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Fi.Ticket.Api
{
    public class AutofacModule : Autofac.Module,
                                 IFiEfCoreModuleLoader
    {
        public void LoadDbContext(IServiceCollection serviceCollection)
        {
        }

        public void RegisterFiModule(ContainerBuilder containerBuilder)
        {
            Load(containerBuilder);
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMediatorTypeImplementations(System.Reflection.Assembly.GetExecutingAssembly());

            builder.RegisterType<FiTicketDbContext>().As<IFiModuleDbContext>().InstancePerLifetimeScope();
        }
    }
}