using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace dapper_crud
{
    public class DbOperations
    {
        //connection string for the local database (Northwind).
        static string ConnectionString = "Data Source=.;Initial Catalog=Northwind;Integrated Security=True;";

        public int InsertNewProduct()
        {
            using (IDbConnection con = new SqlConnection(ConnectionString))
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

                int rowsAffected = con.Execute(sqlQuery, product);

                return rowsAffected;
            }
        }

        public int DeleteProduct()
        {
            using (IDbConnection con = new SqlConnection(ConnectionString))
            {
                string sqlQuery = "DELETE Products WHERE ProductName = @ProductName";
                int rowsAffected = con.Execute(sqlQuery,new { ProductName = "iPhone 11" });

                return rowsAffected;
            }
        }

        public int UpdateProduct()
        {
            using (IDbConnection con = new SqlConnection(ConnectionString))
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

                int rowsAffected = con.Execute(sqlQuery, product);

                return rowsAffected;
            }
        }

        public int TotalProductCount()
        {
            using (IDbConnection con = new SqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT COUNT(*) FROM Products";
                int count =con.ExecuteScalar<int>(sqlQuery);
                return count;
            }
        }

        public Product GetLastProduct()
        {
            Product product = new Product();

            using (IDbConnection con = new SqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT TOP 1 PERCENT * FROM Products ORDER BY ProductID DESC;";

                product = con.QueryFirst<Product>(sqlQuery);
            }

            return product;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (IDbConnection con = new SqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT * FROM Products;";

                products = con.Query<Product>(sqlQuery).ToList();
            }

            return products;
        }

        public List<ProductDTO> GetProductWithCategory()
        {
            List<ProductDTO> productDTOs = new List<ProductDTO>();

            using (IDbConnection con = new SqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT P.*, C.* FROM Products P INNER JOIN Categories C ON P.CategoryID=C.CategoryID;";

                productDTOs = con.Query<Product, Category, ProductDTO>(sqlQuery,map:(P,C) =>
                {
                    ProductDTO productDTO = new ProductDTO();
                    productDTO.product = P;
                    productDTO.category = C;
                    return productDTO;

                },splitOn: "CategoryID").ToList();
            }

            return productDTOs;
        }

    }
}
