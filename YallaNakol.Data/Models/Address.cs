using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YallaNakol.Data.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string Zone { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string DetailedInfo { get; set; }
        [Required]
        public string Phone { get; set; }
        public string AddressString => $"{City}, {Area}, {Zone}, {DetailedInfo}, {Phone}";
        public string ApplicationUserID { get; set; }
    }
}
