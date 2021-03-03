using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YallaNakol.Data.Models;

namespace YallaNakol.Data.Services
{
    public interface IAddress
    {
        void AddAddress(Address category);
        bool AddressExists(int id);
        int SaveChanges();
    }
}
