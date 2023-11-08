using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje1.Models;
using Proje1.ViewModel;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Proje1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjeBirContext _sql;

        public HomeController(ProjeBirContext sql)
        {
            _sql = sql;
        }

        public IActionResult Index(int page = 0)
        {
            ViewBag.PageCount2 = Math.Ceiling((decimal)_sql.Products.Where(x => x.ProductConfirm == 1 && x.ProductVip == 1).Count() / 4);
            ViewBag.PageCount3 = Math.Ceiling((decimal)_sql.Products.Where(x => x.ProductConfirm == 1).Count() / 4);
            ViewBag.Seher = new SelectList(_sql.Sehers.ToList(), "SeherId", "SeherName");
            ViewBag.Rayon = new SelectList(_sql.Rayons.ToList(), "RayonId", "RayonName");
            ViewBag.Menzil = new SelectList(_sql.Menzils.ToList(), "MenzilId", "MenzilName");
            ViewBag.AlisKiraye = new SelectList(_sql.AlisKirayes.ToList(), "AlisKirayesId", "AlisKirayesName");
            HomeModel model = new HomeModel();
            model.SonElanlar = _sql.Products.Include(x => x.Photos).Where(x => x.ProductConfirm == 1).OrderByDescending(x => x.ProductId).Skip(page * 4).Take(4).ToList();
            model.VipElanlar = _sql.Products.Include(x => x.Photos).Where(x => x.ProductConfirm == 1 && x.ProductVip == 1).OrderByDescending(x => x.ProductId).Skip(page * 4).Take(4).ToList();
            model.PremiumElanlar = _sql.Products.Include(x => x.Photos).Where(x => x.ProductConfirm == 1 && x.ProductPremium == 1).OrderByDescending(x => x.ProductId).Skip(page * 4).Take(4).ToList();
            return View(model);
        }
        public IActionResult AlqiSatqi()
        {
            return View();
        }
        public IActionResult Kiraye(int page = 0)
        {
            return View(_sql.Products.Include(x => x.Photos).Where(x => x.ProductConfirm == 1 && x.ProductAlisKirayeId == 2).OrderByDescending(x => x.ProductId).Skip(page * 4).Take(4).ToList());
        }
        public IActionResult Agentlik()
        {
            return View();
        }
        public IActionResult Kompleks()
        {
            return View();
        }
        public IActionResult Like()
        {
            return View();
        }
        [Authorize]
        public IActionResult ElanYerlesdir()
        {
            ViewBag.Seher = new SelectList(_sql.Sehers.ToList(), "SeherId", "SeherName");
            ViewBag.Rayon = new SelectList(_sql.Rayons.ToList(), "RayonId", "RayonName");
            ViewBag.Menzil = new SelectList(_sql.Menzils.ToList(), "MenzilId", "MenzilName");
            ViewBag.Qesebe = new SelectList(_sql.Qesebes.ToList(), "QesebeId", "QesebeName");
            ViewBag.AlisKiraye = new SelectList(_sql.AlisKirayes.ToList(), "AlisKirayeId", "AlisKirayeName");
            return View();
        }
        [HttpPost]
        public IActionResult ElanYerlesdir(Product product, IFormFile[] Photo, string UserToken)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Seher = new SelectList(_sql.Sehers.ToList(), "SeherId", "SeherName");
                ViewBag.Rayon = new SelectList(_sql.Rayons.ToList(), "RayonId", "RayonName");
                ViewBag.Menzil = new SelectList(_sql.Menzils.ToList(), "MenzilId", "MenzilName");
                ViewBag.Qesebe = new SelectList(_sql.Qesebes.ToList(), "QesebeId", "QesebeName");
                ViewBag.AlisKiraye = new SelectList(_sql.AlisKirayes.ToList(), "AlisKirayeId", "AlisKirayeName");
                return View();
            }

            _sql.Products.Add(product);
            product.ProductConfirm = 0;
            product.ProductVip = 0;
            product.ProductUserId = int.Parse(User.FindFirst("Id").Value);
            _sql.SaveChanges();

            foreach (IFormFile photo in Photo)
            {
                string sekil = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(photo.FileName);
                using (Stream stream = new FileStream("wwwroot/img/" + sekil, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }

                using (IMagickImage magickImage = new MagickImage("wwwroot/img/" + sekil))
                {
                    magickImage.Resize(50, 50);
                    magickImage.Quality = 20;
                    magickImage.Write("wwwroot/img/small/" + sekil);
                }

                Photo newPhoto = new Photo();
                newPhoto.PhotoUrl = sekil;
                newPhoto.PhotoProductId = product.ProductId;
                _sql.Photos.Add(newPhoto);
            }
            _sql.SaveChanges();
            return RedirectToAction("ElanYerlesdir");
        }


        [HttpPost]
        public IActionResult Hesab(int id, User user, IFormFile userPhoto)
        {
            User oldUser = _sql.Users.SingleOrDefault(x => x.UserId == id);


            string sekil = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(userPhoto.FileName);
            using (Stream stream = new FileStream("wwwroot/img/UserPhoto/" + sekil, FileMode.Create))
            {
                userPhoto.CopyTo(stream);
            }

            oldUser.UserName = user.UserName;
            oldUser.UserMail = user.UserMail;
            oldUser.UserPhone = user.UserPhone;
            //oldUser.UserPhoto = user.UserPhoto;
            //user.UserPhoto = sekil;
            oldUser.UserPhoto = sekil;
            _sql.SaveChanges();
            return RedirectToAction("Hesab");
        }



        public IActionResult ReadMore(int id)
        {
            return View(_sql.Products.Include(x => x.ProductUser).Include(x => x.ProductRayon).Include(x => x.ProductQesebe).Include(x => x.Photos).Include(x => x.ProductSeher).SingleOrDefault(x => x.ProductId == id));
        }

        public IActionResult ReadMoreVip(int page = 0)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_sql.Products.Where(x => x.ProductConfirm == 1).Count() / 4);
            IndexModel model = new IndexModel();
            model.Mehsul = _sql.Products.Include(x => x.Photos).Where(x => x.ProductConfirm == 1).Skip(page * 4).Take(4).ToList();
            model.Sekil = _sql.Photos.ToList();
            return View(model);
        }

        public IActionResult Hesab(int id)
        {
            return View(_sql.Users.Include(x => x.Products).ThenInclude(x => x.Photos).SingleOrDefault(x => x.UserId == int.Parse(User.FindFirst("Id").Value)));
        }

        public IActionResult AllProduct(SearchModel searchModel, int page = 0)
        {
            var q = _sql.Products.Include(x => x.Photos).Where(x => x.ProductConfirm == 1).OrderByDescending(x => x.ProductId).Skip(page * 4);
            if (searchModel.Seher != null)
            {
                q = q.Where(x => x.ProductSeherId == searchModel.Seher);
            }
            if (searchModel.Kiraye != null)
            {
                q = q.Where(x => x.ProductAlisKirayeId == searchModel.Kiraye);
            }
            if (searchModel.Nov != null)
            {
                q = q.Where(x => x.ProductMenzilId == searchModel.Nov);
            }
            if (searchModel.OtaqSayi != null)
            {
                if (searchModel.OtaqSayi == 5)
                {
                    q = q.Where(x=>x.ProductOtaq >= 5);
                }
                else
                {
                    q = q.Where(x=>x.ProductOtaq == searchModel.OtaqSayi);
                }
            }
            if (searchModel.Min != null)
            {
                q = q.Where(x => x.ProductPrice >= searchModel.Min);
            }
            if (searchModel.Max != null)
            {
                q = q.Where(x => x.ProductPrice <= searchModel.Max);
            }

            searchModel.Mehsullar =q.Take(4).ToList();
            return View(searchModel);
        }
    }
}
