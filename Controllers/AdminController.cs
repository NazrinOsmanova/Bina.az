using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Proje1.Models;
using Proje1.ViewModel;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Proje1.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProjeBirContext _sql;

        public AdminController(ProjeBirContext sql)
        {
            _sql = sql;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int id)
        {
            ViewBag.Seher = new SelectList(_sql.Sehers.ToList(), "SeherId", "SeherName");
            ViewBag.Rayon = new SelectList(_sql.Rayons.ToList(), "RayonId", "RayonName");
            ViewBag.Menzil = new SelectList(_sql.Menzils.ToList(), "MenzilId", "MenzilName");
            ViewBag.AlisKiraye = new SelectList(_sql.AlisKirayes.ToList(), "AlisKirayeId", "AlisKirayeName");
            return View(_sql.Products.Include(x => x.Photos).SingleOrDefault(x => x.ProductId == id));
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product, IFormFile Photo)
        {
            Product oldProduct = _sql.Products.SingleOrDefault(x=>x.ProductId == id);
            if (Photo != null)
            {
                using (Stream stream = new FileStream("wwwroot/img/" + oldProduct.Photos, FileMode.Create))
                {
                    Photo.CopyTo(stream);
                }
            }
            oldProduct.ProductMenzilId = product.ProductMenzilId;
            oldProduct.ProductAlisKirayeId = product.ProductAlisKirayeId;
            oldProduct.ProductSeherId = product.ProductSeherId;
            oldProduct.ProductRayonId = product.ProductRayonId;
            oldProduct.ProductPrice = product.ProductPrice;
            oldProduct.ProductDescription = product.ProductDescription;
            oldProduct.ProductArea = product.ProductArea;
            _sql.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult Delete(int id)
        {
            _sql.Products.Remove(_sql.Products.SingleOrDefault(x => x.ProductId == id));
            _sql.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Giris(User user)
        {
            StringBuilder sb = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                byte[] inputByte = Encoding.ASCII.GetBytes("ABC" + user.UserPassword + "ABC");
                byte[] outputByte = md5.ComputeHash(inputByte);

                for (int i = 0; i < outputByte.Length; i++)
                {
                    sb.Append(outputByte[i].ToString("X2"));
                }
            }
            user.UserPassword = sb.ToString();

            var adam = _sql.Users.Include(x=>x.UserStatus).SingleOrDefault(x=>x.UserMail==user.UserMail && x.UserPassword==user.UserPassword);
            if(adam!=null)
			{
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, adam.UserName),
                    new Claim("Id", adam.UserId.ToString()),
                    new Claim(ClaimTypes.Role, adam.UserStatus.StatusName)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties).Wait();
                return RedirectToAction("Index", "Home");
			}
			else
			{
                ViewBag.Error = "Login ve ya parol yanlisdir";
                return View();
            }
        }

        public IActionResult YeniSifre()
        {
            return View();
        }
     
        public IActionResult Qeydiyyat()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Qeydiyyat(User user, IFormFile? Photo)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            StringBuilder sb = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                byte [] inputByte = Encoding.ASCII.GetBytes("ABC" + user.UserPassword + "ABC");
                byte [] outputByte = md5.ComputeHash(inputByte);

                for (int i = 0; i < outputByte.Length; i++)
                {
                    sb.Append(outputByte[i].ToString("X2"));
                }
            }
            user.UserStatusId = 2;
            user.UserPassword = sb.ToString();
            user.UserPhoto = "Capture21.PNG";
            _sql.Users.Add(user);
            _sql.SaveChanges();
            return RedirectToAction("Giris");
        }

        public IActionResult Cixis()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Tesdiq(int page = 0)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_sql.Products.Count() / 4);
            IndexModel model = new IndexModel();
            model.Mehsul = _sql.Products.Include(x => x.Photos).Where(x => x.ProductConfirm == 0).Skip(page * 0).Take(4).ToList();
            model.Sekil = _sql.Photos.ToList();
            ViewBag.Seher = new SelectList(_sql.Sehers.ToList(), "SeherId", "SeherName");
            ViewBag.Rayon = new SelectList(_sql.Rayons.ToList(), "RayonId", "RayonName");
            ViewBag.Mnezil = new SelectList(_sql.Menzils.ToList(), "MenzilId", "MenzilName");
            ViewBag.AlisKiraye = new SelectList(_sql.AlisKirayes.ToList(), "AlisKirayesId", "AlisKirayesName");
            return View(model);
        }
        public IActionResult Confirm(int id)
        {
            _sql.Products.SingleOrDefault(x=>x.ProductId == id).ProductConfirm=1;
            _sql.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
