using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proje1.Models;
using Proje1.ViewModel;

namespace Proje1.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly ProjeBirContext _sql;

        public SearchViewComponent(ProjeBirContext sql)
        {
            _sql = sql;
        }
        public IViewComponentResult Invoke ()
        {
            ViewBag.AlisKiraye = new SelectList(_sql.AlisKirayes.ToList(), "AlisKirayeId", "AlisKirayeName");
            ViewBag.Menzil = new SelectList(_sql.Menzils.ToList(), "MenzilId", "MenzilName");
            ViewBag.Seher = new SelectList(_sql.Sehers.ToList(), "SeherId", "SeherName");
            SearchModel s = new SearchModel();
            return View(s);
        }
    }
}
