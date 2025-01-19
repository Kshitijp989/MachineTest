using MachineTest.Context;
using MachineTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MachineTest.Service
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts(int page, int pageSize)
        {
            return _context.Products
                           .Include(p => p.Category) 
                           .OrderBy(p => p.ProductId) 
                           .Skip((page - 1) * pageSize) 
                           .Take(pageSize) 
                           .ToList(); 
        }


        public int GetTotalProductCount()
        {
            return _context.Products.Count();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            Product product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
        public int GetProductCount()
        {
            return _context.Products.Count();
        }
    }

}
