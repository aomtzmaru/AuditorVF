using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Services
    {
        public int Id { get; set; }
        public string RenewServices { get; set; }
        public string NewCardServices { get; set; }
        public string ReplaceServices { get; set; }
        public string PrintServices { get; set; }

    }
}