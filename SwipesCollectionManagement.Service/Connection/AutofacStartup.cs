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
        // creating container with Autofac service
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //registering SwipesDbContext options
            var dbContextOptionsBuilder =
                new DbContextOptionsBuilder<SwipesDbContext>().UseSqlServer(ConfigurationManager.ConnectionStrings["SwipesCollectionDb"].ConnectionString);
            //registering SwipesDbContext 
            builder.RegisterType<SwipesDbContext>().WithParameter("options", dbContextOptionsBuilder.Options).InstancePerLifetimeScope();

            //register swipe repository
            builder.RegisterType<SwipeRepository>().WithParameter("connectionString", ConfigurationManager.ConnectionStrings["SwipesCollectionDb"].ConnectionString).SingleInstance();

            //add WCF service to container
            builder.RegisterType<SwipesCollectingService>().SingleInstance();
            //.Named<object>("my-service").
            //build
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}