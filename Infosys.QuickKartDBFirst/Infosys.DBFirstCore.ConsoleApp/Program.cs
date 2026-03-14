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
            //CategoryList();
            //ProductInCategoryList();
            //FilteredProduct();
            //FilteredProductUsingLike();
            //AddCategory();
            //AddProductUsingRange();
            //UserList();
            //AddNewUser();
            //UpdateCategory();
            //UpdateProduct();
            //UpdateProductsUsingRange();
            UpdateUserPassword();
        }
        static void CategoryList()
        {
            Console.WriteLine("\n=========== CATEGORY LIST ===========\n");

            var categories = repository.GetAllCategories();

            Console.WriteLine("{0,-15}{1,-25}", "CategoryId", "CategoryName");
            Console.WriteLine(new string('-', 40));

            foreach (var category in categories)
            {
                Console.WriteLine("{0,-15}{1,-25}", category.CategoryId, category.CategoryName);
            }
        }
        static void ProductInCategoryList()
        {
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
                    Console.WriteLine("{0,-12}{1,-30}{2,-12}{3,-12}{4,-10}",
                        product.ProductId,
                        product.ProductName,
                        product.CategoryId,
                        product.Price,
                        product.QuantityAvailable);
                }
            }
        }
        static void FilteredProduct()
        {
            Console.WriteLine("\n=========== FILTERED PRODUCT ===========\n");

            byte categoryId = 1;
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

        }
        static void FilteredProductUsingLike()
        {
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
        }
        static void AddCategory()
        {
            Console.WriteLine("\n=========== ADD NEW CATEGORY ===========\n");
            string categoryName = "Stationery";
            bool isAdded = repository.AddCategory(categoryName);
            if (isAdded)
            {
                Console.WriteLine("New category added successfully");
            }
            else
            {
                Console.WriteLine("Failed to add new category");
            }
        }
        static void AddProductUsingRange()
        {
            Product productOne = new()
            {
                ProductId = "P158",
                ProductName = "The Ship of Secrets - Geronimo Stilton",
                CategoryId = 8,
                Price = 450,
                QuantityAvailable = 10
            };
            Product productTwo = new Product
            {
                ProductId = "P159",
                ProductName = "101 Nursery Rhymes",
                CategoryId = 8,
                Price = 700,
                QuantityAvailable = 10
            };
            bool result = repository.AddProductsUsingAddRange(productOne, productTwo);
            if (result)
            {
                Console.WriteLine("Product details added successfully!");
            }
            else
            {
                Console.WriteLine("Some error occurred. Try again!!");
            }
        }
        static void UserList()
        {

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
        static void AddNewUser()
        {
            Console.WriteLine("\n=========== ADD NEW USER ===========\n");
            User newUser = new User
            {
                EmailId = "paul.syamantak@gmail.com",
                UserPassword = "paul123",
                Gender = "M",
                DateOfBirth = new DateOnly(1990, 5, 15),
                Address = "123 Main Street, Cityville"
            };
            bool isAdded = repository.RegisterUser(newUser.EmailId, newUser.UserPassword, newUser.Gender, newUser.DateOfBirth, newUser.Address);
            if (isAdded)
            {
                Console.WriteLine("New User added successfully");
            }
            else
            {
                Console.WriteLine("Failed to add new user");
            }
        }
        static void UpdateCategory()
        {
            bool result = repository.UpdateCategory(8, "Stationery");
            if (result)
            {
                Console.WriteLine("Category details updated successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong. Try again!");
            }
        }
        static void UpdateProduct()
        {
            int status = repository.UpdateProduct("P159", 1000);
            if (status == 1)
            {
                Console.WriteLine("Product price updated successfully!");
            }
            else
            {
                Console.WriteLine("Some error occurred. Try again!!");
            }
        }
        static void UpdateProductsUsingRange()
        {
            int status = repository.UpdateProductsUsingUpdateRange(8, 10);
            if (status == 1)
            {
                Console.WriteLine("Products updated successfully!");
            }
            else
            {
                Console.WriteLine("Some error occurred. Try again!!");
            }
        }
        static void UpdateUserPassword()
        {
            bool status = repository.UpdateUserPassword("paul@gmail.com", "paul1234");
            if (status)
            {
                Console.WriteLine("UserPassword updated successfully!");
            }
            else
            {
                Console.WriteLine("Some error occurred. Try again!!");
            }
        }
    }
}