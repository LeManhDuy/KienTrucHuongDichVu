using Microsoft.AspNetCore.Mvc;
using ProductManager.Models;
using ProductManager.Services;

namespace ProductManager.Controllers
{
    public class ProductController : Controller
    {
        // private readonly DataContext _context;
        // public ProductController(DataContext context)
        // {
        //     _context = context;
        // }
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            // var products = _context.Products.ToList();
            var products = _productService.GetProduct();
            return View(products);
        }

        public IActionResult Create()
        {
            var categories = _productService.GetCategories();
            return View(categories);
        }
        public IActionResult Update(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return RedirectToAction("Create");
            var categories = _productService.GetCategories();
            ViewBag.Product = product;
            return View(categories);
        }
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
        public IActionResult Save(Product product)
        {
            if (product.Id == 0)
            {
                _productService.CreateProduct(product);
            }
            else
            {
                _productService.UpdateProduct(product);
            }
            return RedirectToAction("Index");
        }
    }
}