using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.BL
{   
    class UserService
    { 
        public bool login(String email, String password)
        {
            string uri = "https://localhost:44301/api/service/auth";
            string data = "?email="+Uri.EscapeDataString(email) + "&password=" + Uri.EscapeDataString(password);
            byte[] postBytes = Encoding.ASCII.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri+data);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
           

            using (var stream = request.GetRequestStream())
            {
                stream.Write(postBytes, 0, postBytes.Length);
            }

            var reader = new StreamReader(request.GetResponse().GetResponseStream());
            bool content = Convert.ToBoolean(reader.ReadToEnd());
          
            return content;
        }
    }
}
