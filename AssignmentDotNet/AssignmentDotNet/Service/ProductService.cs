using AssignmentDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDotNet.Service
{
    public class ProductService : IProductService
    {
        private readonly ProductDb conn;
        public ProductService(ProductDb context)
        {
            conn = context;
        }

        public IEnumerable<Product> GetProducts(int page, int pageSize)
        {
            return conn.products
                .Include(p => p.CategoryName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Product GetProductById(int productId)
        {
            var product = conn.products
                .Include(p => p.CategoryName)
                .FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            return product;
        }

        public void AddProduct(Product product)
        {
            if (conn.products.Any(p => p.ProductName == product.ProductName))
            {
                throw new Exception("Product with the same name already exists.");
            }

            conn.products.Add(product);
            conn.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            if (conn.products.Any(p => p.ProductName == product.ProductName && p.ProductId != product.ProductId))
            {
                throw new Exception("Product with the same name already exists.");
            }

            conn.products.Update(product);
            conn.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var product = conn.products.Find(productId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            conn.products.Remove(product);
            conn.SaveChanges();
        }
    }
}
