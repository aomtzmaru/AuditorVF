using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Services
    {
        public int Id { get; set; }
        public string ServiceType { get; set; }
        public string PrefixName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RegNumber { get; set; }
        public string RecieveDoc { get; set; }
        public string RecieveBranch { get; set; }
        public string AddressContact { get; set; }
        public string MooContact { get; set; }
        public string SoiContact { get; set; }
        public string RoadContact { get; set; }
        public string DistrictContact { get; set; }
        public string AmphurContact { get; set; }
        public string ProvinceContact { get; set; }
        public string ZipCodeContact { get; set; }
        public string Status { get; set; }
        public int Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedIp { get; set; }
        public string CreatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedIp { get; set; }
        public string UpdatedUser { get; set; }
        public IEnumerable<Files> Files { get; set; }

    }
}