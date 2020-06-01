using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Models
{
    public class Document
    {

        public byte[] FileContent { get; set; }
        public string FileType { get; set; }
        public string DownloadName { get; set; }
    }
}
