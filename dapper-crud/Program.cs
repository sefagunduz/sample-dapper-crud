using dapper_crud;

DbOperations db = new DbOperations();

Console.WriteLine("{0} Record added.", db.InsertNewProduct());

Console.WriteLine("{0} Record deleted.", db.DeleteProduct());

Console.WriteLine("{0} Record updated.", db.UpdateProduct());

Console.WriteLine("Total Product Count {0}.", db.TotalProductCount());

Console.WriteLine("Last product name = {0}", db.GetLastProduct().ProductName);


List<Product> products = db.GetAllProducts();
foreach (Product item in products)
{
    Console.WriteLine("{0} - {1}",item.ProductID, item.ProductName);
}


List<ProductDTO> productDTOs = db.GetProductWithCategory();
foreach (ProductDTO item in productDTOs)
{
    Console.WriteLine("{0} - {1} - {2}",item.product.ProductID,item.product.ProductName,item.category.CategoryName);
}