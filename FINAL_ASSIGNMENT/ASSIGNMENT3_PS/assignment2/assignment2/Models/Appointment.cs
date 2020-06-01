using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Models
{
    [BsonCollection("appointments")]
    public class Appointment : IMongoObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BsonID { get; set; }
        [BsonElement("registration_date")]
        [Display(Name = "Registration Date")]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }
        [BsonElement("client_name")]
        [Display(Name = "Client Name")]
        public String clientName { get; set; }
        [BsonElement("telephone_number")]
        [Display(Name = "Telephone Number")]
        public String telephoneNo { get; set; }
        [BsonElement("car")]
        [Display(Name = "Car")]
        public String carBrand { get; set; }
        [BsonElement("description")]
        [Display(Name = "Description")]
        public string description { get; set; }
        [BsonElement("status")]
        [Display(Name = "Status")]
        public int status { get; set; }

    }
}
