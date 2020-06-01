using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Models
{
    public class DatabaseSettings : IDatabaseSettings
    { 
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }

    public interface IDatabaseSettings
{
        string ConnectionString { get; set; }
        string Database { get; set; }
    }
}
