using Autofac;
using Autofac.Integration.Wcf;
using Microsoft.EntityFrameworkCore;
using SwipesCollectionManagement.DAL;
using SwipesCollectionManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SwipesCollectionManagement.Service.Connection
{
    public class AutofacStartup
    {
        // creating container for dependecy injection with Autofac 
        // https://autofac.readthedocs.io/en/latest/integration/wcf.html
        public static void ConfigureContainer()
        {
            //create new builder
            var builder = new ContainerBuilder();

            //create new DbContext builder 
            var dbContextOptionsBuilder =
                new DbContextOptionsBuilder<SwipesDbContext>().UseSqlServer(ConfigurationManager.ConnectionStrings["SwipesCollectionDb"].ConnectionString);

            //register SwipesDbContext with options
            builder.RegisterType<SwipesDbContext>().WithParameter("options", dbContextOptionsBuilder.Options).InstancePerLifetimeScope();

            //register swipe repository with connection string as a single instance
            builder.RegisterType<SwipeRepository>().WithParameter("connectionString", ConfigurationManager.ConnectionStrings["SwipesCollectionDb"].ConnectionString).SingleInstance();

            //register WCF service as a single instance
            builder.RegisterType<SwipesCollectingService>().SingleInstance();

            //build an autofac container
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}