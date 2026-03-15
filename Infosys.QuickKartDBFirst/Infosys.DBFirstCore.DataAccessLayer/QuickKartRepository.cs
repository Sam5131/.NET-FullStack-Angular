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
            List<Product> lstProducts = context.Products.Where(p => p.CategoryId == categoryId).ToList();
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
            catch (Exception ex)
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
        public bool UpdateCategory(byte categoryId, string newCategoryName)
        {
            bool status;
            try
            {
                //Category category = context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
                Category category = context.Categories.Find(categoryId);
                if (category != null)
                {
                    category.CategoryName = newCategoryName;
                    context.Categories.Update(category);
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public int UpdateProduct(string productId, decimal price)
        {
            int status;
            try
            {
                Product product = context.Products.Find(productId);
                if(product!= null)
                {
                    product.Price = price;
                    using var newContext = new QuickKartDbContext();
                    newContext.Products.Update(product);
                    newContext.SaveChanges();
                    status = 1;
                }
                else
                {
                    status = 0;
                }
            }
            catch (Exception)
            {
                status = -1;
            }
            return status;
        }
        public int UpdateProductsUsingUpdateRange(byte categoryId, int quantityProcured)
        {
            int status;
            try
            {
                List<Product> productList = context.Products.Where(p => p.CategoryId == categoryId).ToList();
                foreach (Product product in productList)
                {
                    product.QuantityAvailable += quantityProcured;
                }
                using var newContext = new QuickKartDbContext();
                newContext.Products.UpdateRange(productList);
                newContext.SaveChanges();
                status = 1;
            }
            catch (Exception)
            {
                status = -1;
            }
            return status;
        }

        public bool UpdateUserPassword(string emailId, string newPassword)
        {
            bool status;
            try
            {
                User user = context.Users.Find(emailId);
                if (user != null)
                {
                    user.UserPassword = newPassword;
                    using var newContext = new QuickKartDbContext();
                    newContext.Users.Update(user);
                    newContext.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region DELTE Operations
        public bool DeleteProduct(string productId)
        {
            bool status;
            try
            {
                Product product = context.Products.Find(productId);
                if(product != null)
                {
                    using var newContext = new QuickKartDbContext();
                    newContext.Products.Remove(product);
                    newContext.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public bool DeleteProductsUsingRemoveRange(string subString)
        {
            bool status;
            try
            {
                List<Product> lstProducts = context.Products.Where(p => p.ProductName.Contains(subString)).ToList();
                if(lstProducts.Count > 0)
                {
                    using var newContext = new QuickKartDbContext();
                    newContext.Products.RemoveRange(lstProducts);
                    newContext.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public bool DeleteUserDetails(string emailId)
        {
            bool status;
            try
            {
                User user = context.Users.Find(emailId);
                if(user != null)
                {
                    using var newContext = new QuickKartDbContext();
                    newContext.Users.Remove(user);
                    newContext.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        #endregion
    }
}
