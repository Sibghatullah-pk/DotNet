using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using Model;

namespace DataAccessLayer
{
    public class CRUD
    {
        private readonly NpgsqlDataSource _dataSource;

        public CRUD(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        // 1. READ (Get All)
        public async Task<List<ProductModel>> GetAllDataAsync()
        {
            var products = new List<ProductModel>();
            const string query = "SELECT \"PId\", \"ProductName\", \"Price\" FROM public.\"Products\" ORDER BY \"PId\" ASC";

            try
            {
                await using var cmd = _dataSource.CreateCommand(query);
                cmd.CommandTimeout = 15;
                await using var reader = await cmd.ExecuteReaderAsync();

                int pIdOrdinal = reader.GetOrdinal("PId");
                int productNameOrdinal = reader.GetOrdinal("ProductName");
                int priceOrdinal = reader.GetOrdinal("Price");

                while (await reader.ReadAsync())
                {
                    products.Add(new ProductModel
                    {
                        PId = reader.GetInt32(pIdOrdinal),
                        ProductName = reader.IsDBNull(productNameOrdinal) ? null! : reader.GetString(productNameOrdinal),
                        Price = reader.GetInt32(priceOrdinal)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[GetAllData Error]: {ex}");
                throw;
            }

            return products;
        }

        // 2. CREATE
        public async Task<bool> InsertProductAsync(ProductModel product)
        {
            const string query = "INSERT INTO public.\"Products\" (\"PId\", \"ProductName\", \"Price\") VALUES (@PId, @ProductName, @Price)";

            try
            {
                await using var cmd = _dataSource.CreateCommand(query);
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("@PId", product.PId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName is null ? DBNull.Value : product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                Console.Error.WriteLine($"[Insert Conflict]: Primary Key PId {product.PId} already exists.");
                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Insert Error]: {ex}");
                throw;
            }
        }

        // 3. UPDATE
        public async Task<bool> UpdateProductAsync(ProductModel product)
        {
            const string query = "UPDATE public.\"Products\" SET \"ProductName\" = @ProductName, \"Price\" = @Price WHERE \"PId\" = @PId";

            try
            {
                await using var cmd = _dataSource.CreateCommand(query);
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("@PId", product.PId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName is null ? DBNull.Value : product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Update Error]: {ex}");
                throw;
            }
        }

        // 4. DELETE
        public async Task<bool> DeleteProductAsync(int pId)
        {
            const string query = "DELETE FROM public.\"Products\" WHERE \"PId\" = @PId";

            try
            {
                await using var cmd = _dataSource.CreateCommand(query);
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("@PId", pId);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Delete Error]: {ex}");
                throw;
            }
        }
    }
}
