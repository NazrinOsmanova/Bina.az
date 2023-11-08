using System;
using System.Collections.Generic;

namespace Proje1.Models
{
    public partial class User
    {
        public User()
        {
            Products = new HashSet<Product>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public int? UserStatusId { get; set; }
        public string? UserMail { get; set; }
        public string? UserPhone { get; set; }
        public string? UserPhoto { get; set; }

        public virtual Status? UserStatus { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
