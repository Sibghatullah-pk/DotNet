using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace BusinessLogic
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAllDataAsync();
        Task<bool> AddProductAsync(ProductModel product);
        Task<bool> UpdateProductAsync(ProductModel product);
        Task<bool> DeleteProductAsync(int pId);
    }
}