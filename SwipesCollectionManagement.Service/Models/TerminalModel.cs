using SwipesCollectionManagement.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SwipesCollectionManagement.Service.Models
{
    [DataContract]
    public class TerminalModel
    {
        public TerminalModel(string ip)
        {
            IPAddress = ip;
            Status = TerminalStatus.Waiting;
            Swipes = new List<SwipeModel>();
        }
        [DataMember]
        public string IPAddress { get; set; }
        [DataMember]
        public TerminalStatus Status { get; set; }

        [DataMember]
        public List<SwipeModel> Swipes { get; set; }
    }
    
}