using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SwipesCollectionManagement.Service.Models
{
    [DataContract]
    public class Swipe
    {
        public Swipe(string ip, string[] data)
        {
            IPAddress = ip;
            ID = int.Parse(data[0]);
            SwipeDateTime = DateTime.Parse(data[1]);
            SwipeDirection = data[2];
        }

        [DataMember]
        public string IPAddress { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public DateTime SwipeDateTime { get; set; }
        [DataMember]
        public string SwipeDirection { get; set; }
    }
}