using assignment2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Services
{
    public class ExporterFactory
    {

        private static ExporterFactory _instance;

        public static ExporterFactory Instance()
        {
            // Note: this is not thread safe;
            if (_instance == null)
            {
                _instance = new ExporterFactory();
               
            }
            return _instance;
        }
        public IExporter CreateExporter(int type)
        {
            if(type == 0)
            {
                return new JsonExporter();
            }
            if(type == 1)
            {
                return new CsvExporter();
            }
            else
            {
                return null;
            }
        }
    }
}
