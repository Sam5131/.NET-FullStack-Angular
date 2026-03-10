using System;
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
            var categories = repository.GetAllCategories();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("CategoryId\tCategoryName");
            Console.WriteLine("----------------------------------");
            foreach (var category in categories)
            {
                Console.WriteLine("{0}\t\t{1}", category.CategoryId, category.CategoryName);
            }

            byte categoryId = 1;
            List<Product> lstProducts = repository.GetProductsOnCategoryId(categoryId);
            if (lstProducts.Count == 0)
            {
                Console.WriteLine("No products available under the category = " + categoryId);
            }
            else
            {
                Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", "ProductId", "ProductName", "CategoryId", "Price", "QuantityAvailable");
                Console.WriteLine("---------------------------------------------------------------------------------------");
                foreach (var product in lstProducts)
                {
                    Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", product.ProductId, product.ProductName, product.CategoryId, product.Price, product.QuantityAvailable);
                }
            }

            Product product1 = repository.FilterProducts(categoryId);
            if (product1 == null)
            {
                Console.WriteLine("No product details available");
            }
            else
            {
                Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", "ProductId", "ProductName", "CategoryId", "Price", "QuantityAvailable");
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", product1.ProductId, product1.ProductName, product1.CategoryId, product1.Price, product1.QuantityAvailable);
            }
            Console.WriteLine();

            string pattern = "BMW%";
            List<Product> lstProducts1 = repository.FilterProductsUsingLike(pattern);
            if (lstProducts1.Count == 0)
            {
                Console.WriteLine("No products available with the = " + pattern);
            }
            else
            {
                Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", "ProductId", "ProductName", "CategoryId", "Price", "QuantityAvailable");
                Console.WriteLine("---------------------------------------------------------------------------------------");
                foreach (var product in lstProducts1)
                {
                    Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", product.ProductId, product.ProductName, product.CategoryId, product.Price, product.QuantityAvailable);
                }
            }
            Console.WriteLine();
        }
    }
}
