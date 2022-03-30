using SwipesCollectionManagement.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SwipesCollectionManagement.Service
{
    [ServiceContract]
    public interface ISwipesCollectingService
    {

        [OperationContract]
        void StartCollectingSwipes(); //called from client 

        [OperationContract]
        List<TerminalModel> GetStatus(); //called from client multiple times

        [OperationContract]
        List<SwipeModel> GetAllSwipes(); //return list of swipes from db

        [OperationContract]
        void DeleteAllSwipes(); //delete swipes from db
    }
}
