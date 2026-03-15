using System;
using System.Collections.Generic;
using System.Text;

namespace Infosys.CodeFirstCore.DataAccessLayer.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public byte CategoryId { get; set; }
        public int QuantityAvailable { get; set; }
        public Category Category { get; set; }
    }
}
