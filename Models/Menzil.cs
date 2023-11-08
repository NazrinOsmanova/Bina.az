using System;
using System.Collections.Generic;

namespace Proje1.Models
{
    public partial class Menzil
    {
        public Menzil()
        {
            Products = new HashSet<Product>();
        }

        public int MenzilId { get; set; }
        public string? MenzilName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
