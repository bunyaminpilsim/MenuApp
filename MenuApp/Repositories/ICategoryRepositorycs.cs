using MenuApp.Models;

namespace MenuApp.Repositories
{
    public interface ICategoryRepositorycs
    {
        void AddCategory(CategoryDto categoryDto);
        void UpdateCategory(CategoryDto categoryDto);
        void DeleteCategory(CategoryDto categoryDto);
        List<CategoryDto> GetAllCategories();
        CategoryDto GetCategoryById(int id);
    }
}
