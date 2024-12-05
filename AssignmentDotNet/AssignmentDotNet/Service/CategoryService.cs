using AssignmentDotNet.Models;

namespace AssignmentDotNet.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ProductDb conn;

        public CategoryService(ProductDb context)
        {
            conn = context;
        }

        public List<Category> GetCategories(int page, int pageSize, out int totalCategories)
        {
            totalCategories = conn.categories.Count();

            return conn.categories
                           .Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();
        }

        public void AddCategory(Category category)
        {
            if (conn.categories.Any(c => c.CategoryName == category.CategoryName))
            {
                throw new Exception("Category name already exists.");
            }
            conn.categories.Add(category);
            conn.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            return conn.categories.Find(id);
        }

        public void UpdateCategory(Category category)
        {
            if (conn.categories.Any(c => c.CategoryName == category.CategoryName && c.CategoryId != category.CategoryId))
            {
                throw new Exception("Category name already exists.");
            }
            conn.categories.Update(category);
            conn.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = conn.categories.Find(id);
            if (category != null)
            {
                conn.categories.Remove(category);
                conn.SaveChanges();
            }
        }
    }
}
