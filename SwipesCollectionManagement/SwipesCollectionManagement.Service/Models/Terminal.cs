using SwipesCollectionManagement.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwipesCollectionManagement.Service.Models
{
    public class Terminal
    {
        public Terminal(Random random)
        {
            IPAddress = $"{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";
            Status = Status.Waiting;
            Swipes = new List<Swipe>();
        }

        public void ParseSwipes(string ip, string swipesString)
        {
            foreach (string swipeString in swipesString.Split('\n')) Swipes.Add(new Swipe(ip, swipeString.Split(',')));
        }

        public string IPAddress { get; set; }
        public Status Status { get; set; }
        public List<Swipe> Swipes { get; set; }
     
    }
}