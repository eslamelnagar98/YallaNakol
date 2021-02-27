using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YallaNakol.Data.Models
{
    public class Address
    {
        public string ID { get; set; }
        public string Zone { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string DetailedInfo { get; set; }
        public string Phone { get; set; }

        public int ApplicationUserID { get; set; }
    }
}
