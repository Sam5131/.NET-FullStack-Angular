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

    }
}
