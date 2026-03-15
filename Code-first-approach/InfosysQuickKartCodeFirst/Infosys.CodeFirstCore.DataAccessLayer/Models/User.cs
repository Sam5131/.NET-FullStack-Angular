using Infosys.DBFirstCore.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infosys.CodeFirstCore.DataAccessLayer.Models
{
    public class User
    {
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public Role Role { get; set; }
        public byte? RoleId { get; set; }
        public string UserPassword { get; set; }
        public ICollection<PurchaseDetail> PurchaseDetail { get; set; }
        public User()
        {

        }
    }
}
