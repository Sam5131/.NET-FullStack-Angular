using System;
using System.Collections.Generic;
using System.Text;

namespace Infosys.CodeFirstCore.DataAccessLayer.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public byte CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
