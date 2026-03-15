using Infosys.DBFirstCore.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infosys.CodeFirstCore.DataAccessLayer.Models
{
    public class PurchaseDetail
    {
        public DateTime DateOfPurchase { get; set; }
        public User Email { get; set; }
        public string EmailId { get; set; }
        public Product Product { get; set; }
        public string ProductId { get; set; }
        public long PurchaseId { get; set; }
        public short QuantityPurchased { get; set; }
    }
}
