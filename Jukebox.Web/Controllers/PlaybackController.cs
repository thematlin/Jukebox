using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jukebox.Infrastructure;
using Jukebox.Infrastructure.Services;

namespace Jukebox.Web.Controllers
{
    public class PlaybackController : Controller
    {
        private ISpotiFireService _spotiFireService;

        public PlaybackController(ISpotiFireService spotiFireService)
        {
            _spotiFireService = spotiFireService;
        }

        public JsonResult PlayPlaylistTrack(int trackId)
        {
            _spotiFireService.EnqueueTrack(trackId);

            return Json(new { Message = "Song added to queue." }, JsonRequestBehavior.AllowGet);
        } 

        public JsonResult GetCustomQueue()
        {
            var customQueue = _spotiFireService.GetCustomQueue();

            return Json(customQueue, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLibraryQueue()
        {
            var libraryQueue = _spotiFireService.GetLibraryQueue();

            return Json(libraryQueue, JsonRequestBehavior.AllowGet);
        }

        public void PlayNext()
        {
            _spotiFireService.PlayNext();
        }

        public void Play()
        {
            _spotiFireService.Play();
        }

        public void Pause()
        {
            _spotiFireService.Pause();
        }

        public JsonResult GetCurrentTrack()
        {
            return Json(_spotiFireService.GetCurrentTrack(), JsonRequestBehavior.AllowGet);
        }
    }
}
