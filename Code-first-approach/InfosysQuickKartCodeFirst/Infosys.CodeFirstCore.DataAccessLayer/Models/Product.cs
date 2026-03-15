using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infosys.CodeFirstCore.DataAccessLayer.Models
{
    public class Product
    {
        public string ProductId { get; set; }

        [Required]
        [MinLength(0)]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Column(TypeName = "Numeric(8)")]
        public decimal Price { get; set; }

        [ForeignKey("CategoryId")]
        public byte CategoryId { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int QuantityAvailable { get; set; }
        public Category Category { get; set; }
    }
}
