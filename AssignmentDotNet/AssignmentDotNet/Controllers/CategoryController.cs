using AssignmentDotNet.Models;
using AssignmentDotNet.Service;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDotNet.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public IActionResult GetCategory(int page = 1, int pageSize = 10)
        {
            int totalCategories;
            var categories = categoryService.GetCategories(page, pageSize, out totalCategories);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCategories / pageSize);

            return View(categories);
        }

        [HttpGet]
        public IActionResult PostCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostCategory(Category category)
        {
            try
            {
                categoryService.AddCategory(category);
                return RedirectToAction("GetCategory");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(category);
            }
        }

        public IActionResult EditCategory(int id)
        {
            var category = categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            try
            {
                categoryService.UpdateCategory(category);
                return RedirectToAction("GetCategory");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(category);
            }
        }

        public IActionResult DeleteCategory(int id)
        {
            categoryService.DeleteCategory(id);
            return RedirectToAction("GetCategory");
        }
    }
}
