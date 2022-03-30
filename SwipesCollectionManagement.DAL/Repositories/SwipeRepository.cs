using SwipesCollectionManagement.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipesCollectionManagement.DAL.Repositories
{
    public class SwipeRepository : ISwipeRepository
    {
        private readonly string _connectionString;

        // for autofac
        public SwipeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        //adding list of swipes to db
        public void CreateRange(List<Swipe> list)
        {
            using (var context = new SwipesDbContext(_connectionString))
            {
                //insert multiple swipes
                context.Swipes.AddRange(list);
      
                //save to db
                context.SaveChanges();
            }
        }

        //getting all swipes from db
        public List<Swipe> GetAll()
        {
            using (var context = new SwipesDbContext(_connectionString))
            {
                //retrieve all swipes from db
                return context.Swipes.ToList();
            }
        }

        // clearing db (for testing purposes)
        public void DeleteAll()
        {
            using (var context = new SwipesDbContext(_connectionString))
            {
                //delete all swipes from db
                var all = context.Swipes.ToList();
                if(context.Swipes.Count() > 0)
                {
                    context.Swipes.RemoveRange(all);
                    context.SaveChanges();
                }
              
             
            }
        }
    }
}
