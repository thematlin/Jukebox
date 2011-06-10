using System.Web.Mvc;
using Jukebox.Infrastructure.Services;

namespace Jukebox.Web.Controllers
{
    public class SearchController : Controller
    {
        private ISpotiFireService _spotiFireService;

        public SearchController(ISpotiFireService spotiFireService)
        {
            _spotiFireService = spotiFireService;
        }

        public ActionResult Track()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Track(string query)
        {
            var search = _spotiFireService.Search(query);

            ViewBag.Search = search;

            return View();
        }
    }
}
