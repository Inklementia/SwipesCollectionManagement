using SwipesCollectionManagement.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipesCollectionManagement.DAL.Repositories
{
    public interface ISwipeRepository
    {
        void CreateRange(List<Swipe> list);
        List<Swipe> GetAll();
        void DeleteAll();
    }
}
