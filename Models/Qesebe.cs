using System;
using System.Collections.Generic;

namespace Proje1.Models
{
    public partial class Qesebe
    {
        public Qesebe()
        {
            Products = new HashSet<Product>();
        }

        public int QesebeId { get; set; }
        public string? QesebeName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
