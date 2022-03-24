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
        List<TerminalModel> GetStatus();

        [OperationContract]
        List<SwipeModel> GetAllSwipes();
        [OperationContract]
        void DeleteAllSwipes();
    }
}
