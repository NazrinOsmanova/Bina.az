using System;
using System.Collections.Generic;

namespace Proje1.Models
{
    public partial class Product
    {
        public Product()
        {
            Photos = new HashSet<Photo>();
        }

        public int ProductId { get; set; }
        public int? ProductPrice { get; set; }
        public string? ProductLocation { get; set; }
        public int? ProductArea { get; set; }
        public string? ProductDate { get; set; }
        public int? ProductSeherId { get; set; }
        public int? ProductRayonId { get; set; }
        public int? ProductMenzilId { get; set; }
        public int? ProductOtaq { get; set; }
        public int? ProductAlisKirayeId { get; set; }
        public string? ProductDescription { get; set; }
        public int? ProductConfirm { get; set; }
        public int? ProductVip { get; set; }
        public bool ProductTemir { get; set; }
        public int? ProductPremium { get; set; }
        public bool ProductCixaris { get; set; }
        public bool ProductIpoteka { get; set; }
        public int? ProductQesebeId { get; set; }
        public int? ProductFloor { get; set; }
        public int? ProductFloorCount { get; set; }
        public int? ProductLandArea { get; set; }
        public int? ProductUserId { get; set; }

        public virtual AlisKiraye? ProductAlisKiraye { get; set; }
        public virtual Menzil? ProductMenzil { get; set; }
        public virtual Qesebe? ProductQesebe { get; set; }
        public virtual Rayon? ProductRayon { get; set; }
        public virtual Seher? ProductSeher { get; set; }
        public virtual User? ProductUser { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
