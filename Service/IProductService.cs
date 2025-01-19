using MachineTest.Models;

namespace MachineTest.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(int page, int pageSize);
        int GetTotalProductCount();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        int GetProductCount();
    }

}
