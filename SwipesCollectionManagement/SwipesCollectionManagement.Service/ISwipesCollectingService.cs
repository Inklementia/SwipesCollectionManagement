using SwipesCollectionManagement.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SwipesCollectionManagement.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISwipesCollectingService" in both code and config file together.
    [ServiceContract]
    public interface ISwipesCollectingService
    {
        [OperationContract]
        void StartCollectingSwipes();

        [OperationContract]
        Dictionary<string, string> GetStatus();

        [OperationContract]
        ICollection<Swipe> GetAllSwipes();
    }
}
