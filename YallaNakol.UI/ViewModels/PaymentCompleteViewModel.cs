using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNakol.Data.Models;

namespace YallaNakol.UI.ViewModels
{
    public class PaymentCompleteViewModel
    {
        public decimal TotalOrderCost { get; set; }
        public string TrackingId { get; set; }
        public PaymentType PaymentType{ get; set; }
    }
}
