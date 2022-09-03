# sample-dapper-crud

Dapper examples with c#

Install Nuget Packages

```
PM> Install-Package Dapper
PM> Install-Package System.Data.SqlClient
```

I am using the **Northwind** database!

I will do ***insert, delete, update, select, inner join, count*** operations on the product table.

```
DbOperations.cs for synchronous transactions.
DbOperationsAsync.cs for asynchronous transactions.
```

#### In synchronous transactions, the process of opening the connection is automatically managed by dapper.

#### I open connection with OpenAsync in asynchronous operations.