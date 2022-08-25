using dapper_crud;

DbOperations db = new DbOperations();

Console.WriteLine("{0} Record added.", db.InsertNewProduct());

Console.WriteLine("{0} Record deleted.", db.DeleteProduct());

Console.WriteLine("{0} Record updated.", db.UpdateProduct());