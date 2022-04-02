using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Files
    {
        public int Id { get; set; }
        public string IdCardDuplicate { get; set; }
        public string AuditorCardDuplicate { get; set; }
        public string RenameDuplicate { get; set; }
        public string PhotoFiles { get; set; }
        public string LicenceDuplicate { get; set; }
        public string OtherDuplicate { get; set; }
        public string MissingDocument { get; set; }
        public string SlipPayment { get; set; }

    }
}