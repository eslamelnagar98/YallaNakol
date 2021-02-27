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
       // public int ResturantId { get; set; }
        public DeliveryAreas deliveryAreas { get; set; }
        public SelectList ResturantDeliveryAreas 
        {
            get
            {
                return new SelectList
                    (
                         GetFlags(deliveryAreas),
                         "Key",
                         "Value"
                    );

            }
        
        }

        Dictionary<int,string> GetFlags(DeliveryAreas areas)
        {
            var dict = new Dictionary<int, string>();
            foreach (var area in Enum.GetValues<DeliveryAreas>())
            {
                if(areas.HasFlag(area))
                {
                    dict.Add(
                        (int) Enum.Parse(typeof(DeliveryAreas),area.ToString())
                        ,area.ToString());
                }
            }

            return dict;
        } 
       


    }
}
