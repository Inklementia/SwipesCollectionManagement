using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SwipesCollectionManagement.DAL.DBO
{
    public class Swipe
    {
        [Key]
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string IPAddress { get; set; }
        public DateTime SwipeDateTime { get; set; }
        public string SwipeDirection { get; set; }
    }
}
