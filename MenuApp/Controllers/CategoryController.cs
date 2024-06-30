using MenuApp.Models;
using MenuApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MenuApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepositorycs _categoryRepositorycs;

        public CategoryController(ICategoryRepositorycs categoryRepository)
        {
            _categoryRepositorycs = categoryRepository;
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryDto category)
        {

            if (ModelState.IsValid)
            {


                _categoryRepositorycs.AddCategory(category);
                return RedirectToAction("CategoryList");
            }

            return View(category);
        }

        public IActionResult CategoryList()
        {
            var category = _categoryRepositorycs.GetAllCategories();
            return View(category);
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var category = _categoryRepositorycs.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult UpdateCategory(CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepositorycs.UpdateCategory(category);
                return RedirectToAction("CategoryList");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {

            var category = _categoryRepositorycs.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepositorycs.DeleteCategory(category);
            return RedirectToAction("CategoryList");
        }
    }
}

