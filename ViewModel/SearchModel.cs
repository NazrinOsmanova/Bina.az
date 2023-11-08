using Proje1.Models;

namespace Proje1.ViewModel
{
    public class SearchModel
    {
        public List<Product> Mehsullar { get; set; }
        public int? Kiraye { get; set; }
        public int? Nov { get; set; }
        public int? OtaqSayi { get; set; }
        public int? Seher { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
    }
}
