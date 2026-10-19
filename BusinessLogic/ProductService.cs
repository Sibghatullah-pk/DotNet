using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Npgsql;

namespace BusinessLogic
{
    public class ProductService : IProductService
    {
        private readonly CRUD _crud;

        public ProductService(NpgsqlDataSource dataSource)
        {
            _crud = new CRUD(dataSource);
        }

        public Task<List<ProductModel>> GetAllDataAsync()
        {
            return _crud.GetAllDataAsync();
        }

        public Task<bool> AddProductAsync(ProductModel product)
        {
            return _crud.InsertProductAsync(product);
        }

        public Task<bool> UpdateProductAsync(ProductModel product)
        {
            return _crud.UpdateProductAsync(product);
        }

        public Task<bool> DeleteProductAsync(int pId)
        {
            return _crud.DeleteProductAsync(pId);
        }
    }
}
