using assignment2.Data;
using assignment2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Services
{
    public class JsonExporter : IExporter
    {

        public Document export(List<Appointment> lstData)
        {
            Document jsonDocument = new Document();

            jsonDocument.FileContent = new System.Text.UTF8Encoding().GetBytes(JsonConvert.SerializeObject(lstData, Formatting.Indented));
            jsonDocument.FileType = "text/json";
            jsonDocument.DownloadName = "appointments.json";

            return jsonDocument;
            
        }
    }
}
