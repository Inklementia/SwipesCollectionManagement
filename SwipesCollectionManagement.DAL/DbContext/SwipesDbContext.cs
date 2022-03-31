using Microsoft.EntityFrameworkCore;
using SwipesCollectionManagement.DAL;
using SwipesCollectionManagement.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SwipesCollectionManagement.DAL
{
    public class SwipesDbContext : DbContext
    {
        public SwipesDbContext(string connectionString) : base(
            (new DbContextOptionsBuilder<SwipesDbContext>().UseSqlServer(connectionString)).Options)
        {
        }

        public SwipesDbContext(DbContextOptions<SwipesDbContext> options) : base(options)
        {
        }

        public DbSet<Swipe> Swipes { get; set; }

    }
}


