using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string LoginLog { get; set; }
        public string RegisLog { get; set; }
        public string ServiceLog { get; set; }
        public string ListLog { get; set; }
        public string DetailLog { get; set; }
        public string EditLog { get; set; }
        public string AdminLog { get; set; }
        public string AdminEditLog { get; set; }
    }
}