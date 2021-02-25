using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YallaNakol.Data.Models;

namespace YallaNakol.Data.Services
{
    public interface IOrder
    {
        void CreateOrder(Order order);
    }
}
