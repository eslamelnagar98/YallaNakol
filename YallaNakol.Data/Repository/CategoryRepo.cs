using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YallaNakol.Data.Repository
{
    public class CategoryRepo : ICategory
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryRepo(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Category> AllCategories => 
            _applicationDbContext.Categories
                                 .AsNoTracking()
                                 .ToList();

        private object AsNoTracking()
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int ? categoryId) =>
            _applicationDbContext.Categories.FirstOrDefault(I => I.Id == categoryId);
        public void AddCategory(Category category)
        {
            _applicationDbContext.Categories.Add(category);
            _applicationDbContext.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            _applicationDbContext.Update(category);
            _applicationDbContext.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            _applicationDbContext.Remove(category);
            _applicationDbContext.SaveChanges();
        }
        public bool CategoryExists(int id)
        {
            return _applicationDbContext.Categories.Any(e => e.Id == id);
        }

    }
}
