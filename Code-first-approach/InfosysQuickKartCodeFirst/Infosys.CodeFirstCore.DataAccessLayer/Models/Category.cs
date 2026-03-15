using Infosys.DBFirstCore.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [StringLength(20)]
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
