using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using System.Linq;

namespace YallaNakol.Data.Repository
{
    public class CategoryRepo : ICategory
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryRepo(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Category> AllCategories => _applicationDbContext.Categories;

        public Category GetCategoryById(int CategoryId) =>
            _applicationDbContext.Categories.FirstOrDefault(I => I.Id == CategoryId);
    }
}
