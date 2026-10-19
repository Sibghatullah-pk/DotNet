using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ProductService : IProductService
    {
        public List<ProductModel> GetAllData()
        {
            return CRUD.GetAllData();
        }

        
    }
}
