using assignment2.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Data
{
    public class ServiceContext
    {
        private readonly IMongoDatabase _database = null;

        public ServiceContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Appointment> Appointments
        {
            get
            {
                return _database.GetCollection<Appointment>("Appointment");
            }
        }
    }
}
