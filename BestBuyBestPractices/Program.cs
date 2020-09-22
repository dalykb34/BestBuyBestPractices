using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using BestBuyBestPractices;

namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion
            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            
            
            Console.WriteLine("Hello user, here are the current departments.");
            Console.WriteLine("Please press enter");
            Console.ReadLine();
            var depos = repo.GetAllDepartments();
            Print(depos);
            
            Console.WriteLine("Do you want to add a department?");
            string userResponse = Console.ReadLine();

            if(userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new department?");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }
            Console.WriteLine("Have a great day!");

            //IDbConnection conn1 = new MySqlConnection(connString);
            DapperProductsRepository repo1 = new DapperProductsRepository(conn);
            Console.WriteLine("Hello user, here are the current products.");
            Console.WriteLine("Please press enter");
            Console.ReadLine();
            var produc = repo1.GetAllProducts();
            Print(produc);

            Console.WriteLine("Do you want to add a product?");
            string userResponse1 = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new product?");
                var proName = Console.ReadLine();

                Console.WriteLine($"What is the new product's price?");
                var price = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"What is the new product's category id?");
                var categoryID = Convert.ToInt32(Console.ReadLine());

                repo1.CreateProduct(proName, price, categoryID);
                Print(repo1.GetAllProducts());
            }
            Console.WriteLine("Have a great day!");
        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach(var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentId} Name: {depo.Name}");
            }
        }

        private static void Print(IEnumerable<Products> produc)
        {
            foreach (var produ in produc)
            {
                Console.WriteLine($"Id: {produ.ProductID} Name: {produ.Name} Price: {produ.Price}");
             }
        }
    }
    

        
        
    
}
