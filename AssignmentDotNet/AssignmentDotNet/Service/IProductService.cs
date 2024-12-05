using AssignmentDotNet.Models;

namespace AssignmentDotNet.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(int page, int pageSize);
        Product GetProductById(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
    }
}
