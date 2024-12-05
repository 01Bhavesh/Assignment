using AssignmentDotNet.Models;
using AssignmentDotNet.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDotNet.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ProductDb conn;
        public ProductController(IProductService _productService , ProductDb context)
        {
            this.productService = _productService;
            conn = context;
        }


        public IActionResult GetProduct(int page = 1, int pageSize = 10)
        {
            var products = productService.GetProducts(page, pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)productService.GetProducts(1, int.MaxValue).Count() / pageSize);

            return View(products);
        }

        [HttpGet]
        public IActionResult PostProduct()
        {
            ViewBag.CategoryList = new SelectList(conn.categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            try
            {
                productService.AddProduct(product);
                return RedirectToAction("GetProduct");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(product);
            }
        }

        public IActionResult EditProduct(int id)
        {
            try
            {
                var product = productService.GetProductById(id);
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("GetProduct");
            }
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            try
            {
                productService.UpdateProduct(product);
                return RedirectToAction("GetProduct");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(product);
            }
        }

        public IActionResult DeleteProduct(int id)
        {
            try
            {
                productService.DeleteProduct(id);
                return RedirectToAction("GetProduct");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("GetProduct");
            }
        }
    }
}
