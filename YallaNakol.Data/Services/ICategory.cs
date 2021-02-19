using System.Collections.Generic;
using YallaNakol.Data.Models;

namespace YallaNakol.Data.Services
{
    public interface ICategory
    {
        IEnumerable<Category> AllCategories { get;}
        Category GetCategoryById(int CategoryId);
    }
}