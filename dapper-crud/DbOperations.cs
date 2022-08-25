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
                    UnitPrice = 499.99,
                    UnitsInStock = 15,
                    UnitsOnOrder = 10,
                    ReorderLevel = 10,
                    Discontinued = 0
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
                string sqlQuery = "DELETE Products WHERE ProductName = @ID";
                int rowsAffected = con.Execute(sqlQuery,new {Id = "iPhone 11" });

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
                    UnitPrice = 13.99,
                    UnitsInStock = 2,
                    UnitsOnOrder = 3,
                    ReorderLevel = 4,
                    Discontinued = 1
                };

                string sqlQuery = "UPDATE Products SET ProductName=@ProductName," +
                    "SupplierID=@SupplierID,CategoryID=@CategoryID,QuantityPerUnit=@QuantityPerUnit,UnitPrice=@UnitPrice," +
                    "UnitsInStock=@UnitsInStock,UnitsOnOrder=@UnitsOnOrder,ReorderLevel=@ReorderLevel," +
                    "Discontinued=@Discontinued WHERE ProductID = @ProductID";

                int rowsAffected = con.Execute(sqlQuery, product);

                return rowsAffected;
            }
        }
    }
}
