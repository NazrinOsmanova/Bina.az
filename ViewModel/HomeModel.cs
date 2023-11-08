using Proje1.Models;

namespace Proje1.ViewModel
{
    public class HomeModel
    {
        public List<Product> VipElanlar { get; set; }
        public List<Product> SonElanlar { get; set; }
        public List<Product> PremiumElanlar { get; set; }
    }
}
