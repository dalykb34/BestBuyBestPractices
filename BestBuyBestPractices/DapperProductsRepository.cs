using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;

namespace BestBuyBestPractices
{
    class DapperProductsRepository : IProductsRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string newProductsName, double price, int categoryID)
        {
            _connection.Execute("Insert into product (Name, price, categoryID) Values (@productsName, @productsPrice, @productsCategoryID);",
            new { productsName = newProductsName });
        }

        public IEnumerable<Products> GetAllProducts()
        {
            var produc = _connection.Query<Products>("Select * from products");

            return produc;
        }

       

        public void UpdateProductName(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
                new { updatedName = updatedName, productID = productID });
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });

            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = productID });

            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = productID });
        }
    }
}
