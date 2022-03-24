using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SwipesCollectionManagement.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SwipesCollectionManagement.Service.Connection
{
    public class SwipesDbContextFactory : IDesignTimeDbContextFactory<SwipesDbContext>
    {
        public SwipesDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SwipesDbContext>();
            optionsBuilder.UseSqlServer(ConfigurationManager.
                ConnectionStrings["SwipesCollectionDb"].ConnectionString);

            return new SwipesDbContext(optionsBuilder.Options);
        }
    }

}