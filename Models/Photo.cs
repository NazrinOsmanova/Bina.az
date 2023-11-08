using System;
using System.Collections.Generic;

namespace Proje1.Models
{
    public partial class Photo
    {
        public int PhotoId { get; set; }
        public string? PhotoUrl { get; set; }
        public int? PhotoProductId { get; set; }

        public virtual Product? PhotoProduct { get; set; }
    }
}
