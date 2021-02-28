using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNakol.Data.Models;

namespace YallaNakol.UI.ViewModels
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }
        public SelectList Addresses { get; set; }
    }
}
