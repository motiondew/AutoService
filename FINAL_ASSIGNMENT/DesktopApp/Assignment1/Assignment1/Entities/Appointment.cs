
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Models
{
   
    public class Appointment 
    {
    
        public string BsonID { get; set; }
        public DateTime date { get; set; }
        public String clientName { get; set; }
        public String telephoneNo { get; set; }
        public String carBrand { get; set; }
        public string description { get; set; }
        public int status { get; set; }

    }
}
