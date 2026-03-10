using Infosys.DBFirstCore.DataAccessLayer.Models;
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
    }
}
