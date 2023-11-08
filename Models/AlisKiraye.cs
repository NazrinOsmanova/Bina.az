using System;
using System.Collections.Generic;

namespace Proje1.Models
{
    public partial class AlisKiraye
    {
        public AlisKiraye()
        {
            Products = new HashSet<Product>();
        }

        public int AlisKirayeId { get; set; }
        public string? AlisKirayeName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
