using Microsoft.EntityFrameworkCore;
using SuperMarketBackend.Data;

namespace SuperMarketBackend.Services
{
    public class ProductServices
    {
        private readonly SuperMrktDbContext _context;
        public ProductServices()
        {
            _context = new SuperMrktDbContext();
        }

        public Product AddProduct(Product product)
        {
            if (_context.Products.FirstOrDefault(p => p.ProductName == product.ProductName) != null)
                return new Product() { };
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product? GetProduct(int productId = 0 , string productName = "")
        {
            if(productId != 0)
                return _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (productName != "")
                return _context.Products.FirstOrDefault(p => p.ProductName == productName);
            return null;
        }

        public List<Product>? GetAllProducts(int categoryId = 0, string search = "" , bool orderBydesc = false)
        {
            var products = _context.Products.Where( p => !p.IsDeleted).ToList();
            if (categoryId != 0)
                products = products.Where(p => p.ProductCategoryId == categoryId).ToList();
            if (search != "")
                products = products.Where(p => EF.Functions.Like(p.ProductName, $"%{search}%")).ToList();
            if (orderBydesc)
                products = products.OrderByDescending(p => p.ProductId).ToList();
            else
                products = products.OrderBy(p => p.ProductId).ToList();
            return products;
        }

        public Product? UpdateProduct(Product product)
        {
            var p = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId && !p.IsDeleted);
            if (p == null)
                return new Product() { };
            p.ProductName = product.ProductName;            
            p.StockQuantity = product.StockQuantity;
            p.Price = product.Price;
            p.Vendor = product.Vendor;
            p.Descriptions = product.Descriptions;
            p.Discount = product.Discount;
            p.Tax = product.Tax;
            p.ProductCategoryId = product.ProductCategoryId;            
            _context.Products.Update(p);
            _context.SaveChanges();
            return p;
        }

        public bool DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if(product == null) 
                return false;
            product.IsDeleted = true;
            _context.Products.Update(product);
            _context.SaveChanges();
            return true;
        }

        public ProductCategory? AddProductCategory(ProductCategory category)
        {
            if (_context.ProductCategories.FirstOrDefault(pc => pc.CategoryName == category.CategoryName) != null)
                return null;
            _context.ProductCategories.Add(category);
            _context.SaveChanges();
            return _context.ProductCategories.OrderBy(pc => pc.ProductCategoryId).Last();
        }

        public bool DeleteProductCategory (int productCategoryId)
        {
            var category = _context.ProductCategories.FirstOrDefault(pc => pc.ProductCategoryId == productCategoryId && !pc.IsDeleted);
            if (category == null) return false;
            category.IsDeleted = true;
            _context.ProductCategories.Update(category);
            _context.SaveChanges();
            return true;
        }

        public List<ProductCategory>? GetAllProductCategories()
        {
            return _context.ProductCategories.Where(pc => !pc.IsDeleted).ToList();
        }
    }
}
