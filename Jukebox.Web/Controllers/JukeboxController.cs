using System.Web.Mvc;
using Jukebox.Infrastructure.Services;

namespace Jukebox.Web.Controllers
{
    public class JukeboxController : Controller
    {
        private readonly IJukeboxService _jukeboxService;

        public JukeboxController(IJukeboxService jukeboxService)
        {
            _jukeboxService = jukeboxService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public void AddSearchedTrackToQueue(string query, string trackId)
        {
            _jukeboxService.PlayTrack(query, trackId);
        }
    }
}
