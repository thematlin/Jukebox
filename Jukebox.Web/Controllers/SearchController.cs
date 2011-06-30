using System.Web.Mvc;
using Jukebox.Infrastructure.Services;

namespace Jukebox.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IJukeboxService _jukeboxService;

        public SearchController(IJukeboxService jukeboxService)
        {
            _jukeboxService = jukeboxService;
        }

        public ActionResult Track()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Track(string query)
        {
            var search = _jukeboxService.Search(query);

            TempData.Add("Search", search);

            return RedirectToAction("Index", "Jukebox");
        }
    }
}
