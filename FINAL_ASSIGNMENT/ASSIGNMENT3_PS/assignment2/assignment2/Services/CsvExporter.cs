using assignment2.Data;
using assignment2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment2.Services
{
    public class CsvExporter : IExporter
    {
        public string listToString(List<Appointment> lstData)
        {
            var sb = new StringBuilder();

            sb.AppendLine("id,date,clientName,telephoneNo,carBrand,description,status");
            foreach (var data in lstData)
            {
                sb.AppendLine(data.BsonID.ToString() + "," + data.date + "," + data.clientName + "," + data.telephoneNo + "," + data.carBrand + "," + data.description + "," + data.status);
            }
            return sb.ToString();
        }
        public Document export(List<Appointment> lstData)
        {
            Document csvDocument = new Document();

            csvDocument.FileContent = new System.Text.UTF8Encoding().GetBytes(listToString(lstData));
            csvDocument.FileType = "text/csv";
            csvDocument.DownloadName = "appointments.csv";

            return csvDocument;

        }
    }
}
