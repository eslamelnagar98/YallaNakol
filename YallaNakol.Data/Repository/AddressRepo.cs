using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YallaNakol.Data.Repository
{
    /*public class AddressRepo : IAddress
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AddressRepo(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public void AddAddress(Address address) =>
        
            _applicationDbContext.Addresses.Add(address);
   
        public bool AddressExists(int id)
        
           => _applicationDbContext.Addresses.Any(e => e.ID == id);

        public int SaveChanges() => _applicationDbContext.SaveChanges();

    }*/
}
