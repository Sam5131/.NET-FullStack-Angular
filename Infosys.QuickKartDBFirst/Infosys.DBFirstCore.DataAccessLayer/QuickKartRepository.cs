using Infosys.DBFirstCore.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infosys.DBFirstCore.DataAccessLayer
{
    public class QuickKartRepository
    {
        private QuickKartDbContext context;
        public QuickKartRepository(QuickKartDbContext context)
        {
            this.context = context;
        }
        #region READ Operations
        public List<Category> GetAllCategories()
        {
            List<Category> categories = context.Categories.OrderBy(c => c.CategoryId).ToList();
            return categories;
        }
        public List<Product> GetProductsOnCategoryId(byte categoryId)
        {
            List<Product> lstProducts = context.Products.Where(p => p.CategoryId == categoryId).OrderBy(p => p.ProductId).ToList();
            return lstProducts;
        }
        public Product FilterProducts(byte categoryId)
        {
            Product product = new Product();
            try
            {
                product = context.Products.Where(p => p.CategoryId == categoryId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                product = null;
            }
            return product;
        }
        public List<Product> FilterProductsUsingLike(string pattern)
        {
            List<Product> lstProduct = new List<Product>();
            try
            {
                lstProduct = context.Products.Where(p => EF.Functions.Like(p.ProductName, pattern)).ToList();
            }
            catch (Exception ex)
            {
                lstProduct = null;
            }
            return lstProduct;
        }
        public List<User> GetAllUsers()
        {
            List<User> users = context.Users.OrderBy(u => u.EmailId).ToList();
            return users;
        }
        #endregion

        #region CREATE Operations
        public bool AddCategory(string categoryName)
        {
            bool status = false;
            try
            {
                Category category = new Category();
                category.CategoryName = categoryName;
                context.Categories.Add(category);
                context.SaveChanges();
                status = true;
            }
            catch(Exception ex)
            {
                status = false;
            }
            return status;
        }
        public bool AddProductsUsingAddRange(params Product[] products)
        {
            bool status = false;
            try
            {
                context.Products.AddRange(products);
                context.SaveChanges();
                status = true;

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        public bool RegisterUser(string emailId, string userPassword, string gender, DateOnly dateOfBirth, string address)
        {
            bool status;
            try
            {
                User user = new()
                {
                    UserPassword = userPassword,
                    EmailId = emailId,
                    Gender = gender,
                    DateOfBirth = dateOfBirth,
                    Address = address
                };
                context.Users.Add(user);
                context.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }
        #endregion
        #region UPDATE Operations

        #endregion

    }
}
