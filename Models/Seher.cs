using System;
using System.Collections.Generic;

namespace Proje1.Models
{
    public partial class Seher
    {
        public Seher()
        {
            Products = new HashSet<Product>();
        }

        public int SeherId { get; set; }
        public string? SeherName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
