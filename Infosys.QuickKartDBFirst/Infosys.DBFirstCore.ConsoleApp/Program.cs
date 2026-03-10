using System;
using System.Collections.Generic;
using Infosys.DBFirstCore.DataAccessLayer;
using Infosys.DBFirstCore.DataAccessLayer.Models;

namespace Infosys.DBFirstCore.ConsoleApp
{
    public class Program
    {
        static QuickKartDbContext context;
        static QuickKartRepository repository;

        static Program()
        {
            context = new QuickKartDbContext();
            repository = new QuickKartRepository(context);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\n=========== CATEGORY LIST ===========\n");

            var categories = repository.GetAllCategories();

            Console.WriteLine("{0,-15}{1,-25}", "CategoryId", "CategoryName");
            Console.WriteLine(new string('-', 40));

            foreach (var category in categories)
            {
                Console.WriteLine("{0,-15}{1,-25}", category.CategoryId, category.CategoryName);
            }


            byte categoryId = 1;
            Console.WriteLine("\n=========== PRODUCTS IN CATEGORY " + categoryId + " ===========\n");

            List<Product> lstProducts = repository.GetProductsOnCategoryId(categoryId);

            if (lstProducts.Count == 0)
            {
                Console.WriteLine("No products available under category = " + categoryId);
            }
            else
            {
                Console.WriteLine("{0,-12}{1,-30}{2,-12}{3,-12}{4,-10}",
                    "ProductId", "ProductName", "CategoryId", "Price", "Quantity");

                Console.WriteLine(new string('-', 80));

                foreach (var product in lstProducts)
                {
                    Console.WriteLine("{0,-12}{1,-30}{2,-12}{3,-12:C}{4,-10}",
                        product.ProductId,
                        product.ProductName,
                        product.CategoryId,
                        product.Price,
                        product.QuantityAvailable);
                }
            }


            Console.WriteLine("\n=========== FILTERED PRODUCT ===========\n");

            Product product1 = repository.FilterProducts(categoryId);

            if (product1 == null)
            {
                Console.WriteLine("No product details available");
            }
            else
            {
                Console.WriteLine("{0,-12}{1,-30}{2,-12}{3,-12}{4,-10}",
                    "ProductId", "ProductName", "CategoryId", "Price", "Quantity");

                Console.WriteLine(new string('-', 80));

                Console.WriteLine("{0,-12}{1,-30}{2,-12}{3,-12:C}{4,-10}",
                    product1.ProductId,
                    product1.ProductName,
                    product1.CategoryId,
                    product1.Price,
                    product1.QuantityAvailable);
            }


            string pattern = "BMW%";
            Console.WriteLine("\n=========== PRODUCTS LIKE '" + pattern + "' ===========\n");

            List<Product> lstProducts1 = repository.FilterProductsUsingLike(pattern);

            if (lstProducts1.Count == 0)
            {
                Console.WriteLine("No products available with pattern = " + pattern);
            }
            else
            {
                Console.WriteLine("{0,-12}{1,-30}{2,-12}{3,-12}{4,-10}",
                    "ProductId", "ProductName", "CategoryId", "Price", "Quantity");

                Console.WriteLine(new string('-', 80));

                foreach (var product in lstProducts1)
                {
                    Console.WriteLine("{0,-12}{1,-30}{2,-12}{3,-12:C}{4,-10}",
                        product.ProductId,
                        product.ProductName,
                        product.CategoryId,
                        product.Price,
                        product.QuantityAvailable);
                }
            }


            Console.WriteLine("\n=========== USER LIST ===========\n");

            List<User> allUsers = repository.GetAllUsers();

            if (allUsers.Count == 0)
            {
                Console.WriteLine("No Users available");
            }
            else
            {
                Console.WriteLine("{0,-30}{1,-20}{2,-10}{3,-10}{4,-15}{5,-25}",
                    "EmailId", "Password", "RoleId", "Gender", "DOB", "Address");

                Console.WriteLine(new string('-', 110));

                foreach (var user in allUsers)
                {
                    Console.WriteLine("{0,-30}{1,-20}{2,-10}{3,-10}{4,-15:dd-MM-yyyy}{5,-25}",
                        user.EmailId,
                        user.UserPassword,
                        user.RoleId,
                        user.Gender,
                        user.DateOfBirth,
                        user.Address);
                }
            }

            Console.WriteLine("\n========================================\n");
            Console.ReadKey();
        }
    }
}