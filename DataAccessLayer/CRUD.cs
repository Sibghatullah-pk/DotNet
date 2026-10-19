using System;
using System.Collections.Generic;
using Npgsql;
using Model;

namespace DataAccessLayer
{
    public class CRUD
    {
        public static List<ProductModel> GetAllData()
        {
            List<ProductModel> products = new List<ProductModel>();
            string query = "SELECT \"PId\", \"ProductName\", \"Price\" FROM \"Products\"";

            using (NpgsqlConnection connection = DBHelper.GetConnection())
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductModel model = new ProductModel
                        {
                            PId = Convert.ToInt32(reader["PId"]),
                            ProductName = reader["ProductName"]?.ToString(),
                            Price = Convert.ToInt32(reader["Price"])
                        };
                        products.Add(model);
                    }
                }
            }

            return products;
        }
    }
}