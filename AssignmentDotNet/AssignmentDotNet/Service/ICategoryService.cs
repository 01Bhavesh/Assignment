using AssignmentDotNet.Models;

namespace AssignmentDotNet.Service
{
    public interface ICategoryService
    {
        List<Category> GetCategories(int page, int pageSize, out int totalCategories);
        void AddCategory(Category category);
        Category GetCategoryById(int id);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}
