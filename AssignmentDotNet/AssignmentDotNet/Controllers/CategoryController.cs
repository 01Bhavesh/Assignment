using AssignmentDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDotNet.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ProductDb conn;
        public CategoryController(ProductDb _product)
        {
            conn = _product;            
        }

        public IActionResult GetCategory()
        {
            List<Category> categories = conn.categories.ToList();
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
            conn.categories.Add(category);
            conn.SaveChanges();
            return RedirectToAction("GetCategory");
        }
        public IActionResult EditCategory(int id)
        {
            Category cat = conn.categories.Find(id);
            return View(cat);
        }
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            conn.categories.Update(category);
            conn.SaveChanges();
            return RedirectToAction("GetCategory");
        }
        public IActionResult DeleteCategory(int id)
        {
            Category cat = conn.categories.Find(id);
            conn.categories.Remove(cat);
            conn.SaveChanges();
            return RedirectToAction("GetCategory");
        }
    }
}
