using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using assignment2.Models;
using Newtonsoft.Json;

namespace Assignment1.BL
{
    class AppointmentService : IAppointmentService
    {
        public void Edit(Appointment appointment)
        {
            string uri = "https://localhost:44301/api/service/update";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.ContentType = "text/json";
            request.Method = "PUT";
            var body = JsonConvert.SerializeObject(appointment);

            //write the serialized product to the request
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(body);
            }
            request.GetResponse();
        }

        public List<Appointment> Get()
        {
            string uri = "https://localhost:44301/api/service";

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "GET";
            var response = request.GetResponse();
            Stream stream = response.GetResponseStream();

            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(List<Appointment>));

            List<Appointment> objResponse = (List<Appointment>)dataContractJsonSerializer.ReadObject(stream);
            if (objResponse != null)
            {
                return objResponse;
            }
            else
            {
                return new List<Appointment>();
            }
        }
    }
}
