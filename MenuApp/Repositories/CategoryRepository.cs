using MenuApp.Data;
using MenuApp.Models;

namespace MenuApp.Repositories
{
    public class CategoryRepository : ICategoryRepositorycs
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public void AddCategory(CategoryDto categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name,
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(CategoryDto categoryDto)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.Id == categoryDto.Id);
            if (existingCategory != null)
            {
                _context.Categories.Remove(existingCategory);
                _context.SaveChanges();
            }
        }

        public List<CategoryDto> GetAllCategories()
        {
            List<Category> categories = _context.Categories.ToList();

            var categoryDTOs = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoryDTOs.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                });
            }

            return categoryDTOs;
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public void UpdateCategory(CategoryDto categoryDto)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.Id == categoryDto.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = categoryDto.Name;
                _context.SaveChanges();
            }
        }
    }
}
