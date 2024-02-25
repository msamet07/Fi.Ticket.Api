using Autofac;
using Fi.Ticket.Api.Persistence;
using Fi.Persistence.Relational.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fi.Mediator.Interfaces;
using Fi.Infra.Context.Interfaces;
using System;
using Microsoft.AspNetCore.Hosting;
using Fi.Infra.Abstraction;
using Fi.Infra.Options;
using Fi.Mediator.Impl;
using Fi.Infra.Response;
using Fi.Infra.Context;

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
            //builder.RegisterType<FiTicketDbContext>().As<ILogger>().InstancePerLifetimeScope();
            //builder.RegisterType<FiMediator>().As<IFiMediator>().InstancePerLifetimeScope();
            //builder.RegisterType<ExecutionTrace>().As<IExecutionTrace>().InstancePerLifetimeScope();
            //builder.RegisterType<ServiceProvider>().As<IServiceProvider>().InstancePerLifetimeScope();
            //builder.RegisterType<SessionContext>().As<ISessionContext>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageContext>().As<IMessageContext>().InstancePerLifetimeScope();
            ////builder.RegisterType<WebHost>().As<IWebHostEnvironment>().InstancePerLifetimeScope();
            //builder.RegisterType<>().As<IJsonStringLocalizer>().InstancePerLifetimeScope();
            //builder.RegisterType<FiOptions>().As<IFiOptions>().InstancePerLifetimeScope();
            //builder.RegisterType<FiApplicationContext>().As<IFiApplicationContext>().InstancePerLifetimeScope();
        }
    }
}