using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Files
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public int Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedIp { get; set; }
        public string CreatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedIp { get; set; }
        public string UpdatedUser { get; set; }

    }
}