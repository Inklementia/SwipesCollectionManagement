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
    /*
        public TerminalModel(Random random)
        {
            IPAddress = $"{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";
            Status = TerminalStatus.Waiting;
            Swipes = new List<SwipeModel>();
        }

        public void ParseSwipes(string ip, string swipesString)
        {
            foreach (string swipeString in swipesString.Split('\n')) Swipes.Add(new SwipeModel(ip, swipeString.Split(',')));
        }

        public string IPAddress { get; set; }
        public TerminalStatus Status { get; set; }
        public List<SwipeModel> Swipes { get; set; }
     */
    
}