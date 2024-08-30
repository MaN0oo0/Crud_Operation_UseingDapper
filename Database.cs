using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_UseingDapper
{
    public class Database
    {
        private string _connectionString;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void AddProduct(Product product)
        {
            using (var connection = CreateConnection())
            {
                string sql = "INSERT INTO Products (Name, Price, Stock) VALUES (@Name, @Price, @Stock)";
                connection.Execute(sql, new { product.Name, product.Price, product.Stock });
            }
        }

        public List<Product> GetAllProducts()
        {
            using (var connection = CreateConnection())
            {
                string sql = "SELECT * FROM Products";
                var products = connection.Query<Product>(sql).ToList();
                return products;
            }
        }
        public Product GetProductById(int id)
        {
            using (var connection = CreateConnection())
            {
                string sql = "SELECT * FROM Products WHERE Id = @Id";
                return connection.QuerySingleOrDefault<Product>(sql, new { Id = id });
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var connection = CreateConnection())
            {
                string sql = "UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock WHERE Id = @Id";
                connection.Execute(sql, new { product.Name, product.Price, product.Stock, product.Id });
            }
        }

        public void DeleteProduct(int id)
        {
            using (var connection = CreateConnection())
            {
                string sql = "DELETE FROM Products WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }

    }

}
