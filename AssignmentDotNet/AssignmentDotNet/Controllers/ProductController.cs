using AssignmentDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDotNet.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDb conn;

        public ProductController(ProductDb _productdb)
        {
            conn = _productdb;
        }

        public IActionResult GetProduct()
        {
            var products = conn.products.Include(p => p.CategoryName).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult PostProduct()
        {
            ViewBag.CategoryList = new SelectList(conn.categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult PostProduct(Product p)
        {
                conn.products.Add(p);
                conn.SaveChanges();
                return RedirectToAction("GetProduct");
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = conn.products.Find(id);
            ViewBag.CategoryList = new SelectList(conn.categories, "CategoryId", "CategoryName");
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product p)
        {
          
                conn.products.Update(p);
                conn.SaveChanges();
                return RedirectToAction("GetProduct");
            
        }

        public IActionResult DeleteProduct(int id)
        {
            var product = conn.products.Find(id);
            conn.products.Remove(product);
            conn.SaveChanges();
            return RedirectToAction("GetProduct");
        }
    }
}
