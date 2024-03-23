using System;
using Fi.Infra.Const;
using Fi.Infra.Context;
using Fi.Infra.Exceptions;
using Fi.Infra.Impl;
using Fi.Infra.Options;
using Fi.Infra.Schema.Attributes;
using Fi.Persistence.Relational.Context;
using Fi.Persistence.Relational.Interfaces;
using Fi.Persistence.Relational.Context.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fi.Ticket.Api.Persistence
{
    public class FiTicketDbContext : FiDbContext, IFiModuleDbContext //yeni bir contex oluşturmak isteseydim FiDB contexten türetirdim.Entity frameworkün Db contexi bunun alyapısında old. içim.
    {
        public FiTicketDbContext(IFiDbContextFactory fiDbContextFactory) : base(fiDbContextFactory)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder, string dbName, string applicationName, int sqlCommandTimeout)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            string connectionString = string.Format(databaseSettings.DatabaseCluster.ConnectionString,
                    databaseSettings.DatabaseCluster.HostName,
                    dbName,
                    databaseSettings.DatabaseCluster.UserName,
                    databaseSettings.DatabaseCluster.Password,
                    applicationName);//burdaki konfigürasyonu değiştirerek bağlandığım yeri değiştiebilirdim.

            optionsBuilder.UseSqlServer(connectionString,//bu Usesql dediği için Mssql e bağlanır.
                                        sqlServerOptions => sqlServerOptions.CommandTimeout(sqlCommandTimeout));
        }
    }
}
