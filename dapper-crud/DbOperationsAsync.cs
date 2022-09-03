using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace dapper_crud
{
    public class DbOperationsAsync
    {
        //connection string for the local database (Northwind).
        static string ConnectionString = "Data Source=.;Initial Catalog=Northwind;Integrated Security=True;";

        public async Task<int> InsertNewProductAsync()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                Product product = new Product()
                {
                    ProductName = "iPhone 11",
                    SupplierID = 3,
                    CategoryID = 7,
                    QuantityPerUnit = "1 Piece",
                    UnitPrice = 499.99M,
                    UnitsInStock = 15,
                    UnitsOnOrder = 10,
                    ReorderLevel = 10,
                    Discontinued = true
                };

                string sqlQuery = "INSERT INTO Products (ProductName,SupplierID,CategoryID,QuantityPerUnit," +
                    "UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued) " +
                    "VALUES(@ProductName,@SupplierID,@CategoryID,@QuantityPerUnit," +
                    "@UnitPrice,@UnitsInStock,@UnitsOnOrder,@ReorderLevel,@Discontinued)";

                await con.OpenAsync();
                int rowsAffected = await con.ExecuteAsync(sqlQuery, product);

                return rowsAffected;
            }
        }

        public async Task<int> DeleteProductAsync()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sqlQuery = "DELETE Products WHERE ProductName = @ProductName";

                await con.OpenAsync();
                int rowsAffected = await con.ExecuteAsync(sqlQuery, new { ProductName = "iPhone 11" });

                return rowsAffected;
            }
        }

        public async Task<int> UpdateProductAsync()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                Product product = new Product()
                {
                    ProductID = 1,
                    ProductName = "Chai Tea",
                    SupplierID = 1,
                    CategoryID = 3,
                    QuantityPerUnit = "1 Package",
                    UnitPrice = 13.99M,
                    UnitsInStock = 2,
                    UnitsOnOrder = 3,
                    ReorderLevel = 4,
                    Discontinued = true
                };

                string sqlQuery = "UPDATE Products SET ProductName=@ProductName," +
                    "SupplierID=@SupplierID,CategoryID=@CategoryID,QuantityPerUnit=@QuantityPerUnit,UnitPrice=@UnitPrice," +
                    "UnitsInStock=@UnitsInStock,UnitsOnOrder=@UnitsOnOrder,ReorderLevel=@ReorderLevel," +
                    "Discontinued=@Discontinued WHERE ProductID = @ProductID";

                await con.OpenAsync();
                int rowsAffected = await con.ExecuteAsync(sqlQuery, product);

                return rowsAffected;
            }
        }

        public async Task<int> TotalProductCountAsync()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT COUNT(*) FROM Products";

                await con.OpenAsync();
                int count = await con.ExecuteScalarAsync<int>(sqlQuery);
                return count;
            }
        }

        public async Task<Product> GetLastProductAsync()
        {
            Product product = new Product();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT TOP 1 PERCENT * FROM Products ORDER BY ProductID DESC;";

                await con.OpenAsync();
                product = await con.QueryFirstAsync<Product>(sqlQuery);
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> products;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT * FROM Products;";

                await con.OpenAsync();
                products = await con.QueryAsync<Product>(sqlQuery);
            }

            return products;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductWithCategoryAsync()
        {
            IEnumerable<ProductDTO> productDTOs;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT P.*, C.* FROM Products P INNER JOIN Categories C ON P.CategoryID=C.CategoryID;";

                await con.OpenAsync();
                productDTOs = await con.QueryAsync<Product, Category, ProductDTO>(sqlQuery, map: (P, C) =>
                {
                    ProductDTO productDTO = new ProductDTO();
                    productDTO.product = P;
                    productDTO.category = C;
                    return productDTO;

                }, splitOn: "CategoryID");
            }

            return productDTOs;
        }
    }
}
