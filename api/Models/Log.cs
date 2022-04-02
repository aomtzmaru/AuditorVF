using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ActionDetail { get; set; }
        public string PageAction { get; set; }
        public DateTime Created { get; set; }
    }
}