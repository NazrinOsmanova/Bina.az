using System;
using System.Collections.Generic;

namespace Proje1.Models
{
    public partial class Rayon
    {
        public Rayon()
        {
            Products = new HashSet<Product>();
        }

        public int RayonId { get; set; }
        public string? RayonName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
