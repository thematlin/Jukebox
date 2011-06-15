using System.Linq;
using System.Web.Mvc;
using Jukebox.Infrastructure.Services;
using Jukebox.Infrastructure.Repositories;

namespace Jukebox.Web.Controllers
{
    public class LibraryController : Controller
    {
        private ISpotiFireService _spotiFireService;

        public LibraryController(ISpotiFireService spotiFireService)
        {
            _spotiFireService = spotiFireService;
        }

        public ActionResult Index()
        {
            var tracks = _spotiFireService.GetPlaylistTracks().ToList();

            tracks.Sort((x, y) => string.Compare(x.ToString(), y.ToString()));

            ViewBag.Tracks = tracks;

            return View();
        }

        public void AddToLibrary(string query, int trackId)
        {
            _spotiFireService.AddTrackFromSearch(query, trackId);

            _spotiFireService.EnqueueTrack(trackId);
        }
    }
}
