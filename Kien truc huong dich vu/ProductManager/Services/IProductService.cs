using ProductManager.Models;

namespace ProductManager.Services
{
    public interface IProductService
    {
        List<Product> GetProduct();
        List<Category> GetCategories();
        Product? GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}