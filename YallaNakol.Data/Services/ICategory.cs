using System.Collections.Generic;
using YallaNakol.Data.Models;

namespace YallaNakol.Data.Services
{
    public interface ICategory
    {
        IEnumerable<Category> AllCategories { get;}
        Category GetCategoryById(int? categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        bool CategoryExists(int id);
    }
}