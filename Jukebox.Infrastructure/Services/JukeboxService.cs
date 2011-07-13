using System.Collections.Generic;
using Jukebox.Business.Models;
using Jukebox.Infrastructure.Repositories;
using Jukebox.Infrastructure.Services.SpotiFireService;

namespace Jukebox.Infrastructure.Services
{
    public class JukeboxService : IJukeboxService
    {
        private readonly IJukeboxTrackRepository _trackRepository;
        private readonly ISpotiFireService _spotiFireService;

        public JukeboxService(IJukeboxTrackRepository trackRepository, ISpotiFireService spotiFireService)
        {
            _trackRepository = trackRepository;
            _spotiFireService = spotiFireService;
        }

        public IEnumerable<JukeboxTrack> GetAllTracks()
        {
            return _trackRepository.Get();
        }

        public JukeboxSearch Search(string query)
        {
            return _spotiFireService.Search(query);
        }

        public void PlayTrack(string query, string trackId)
        {
            var track = _trackRepository.GetTrackById(trackId);
            
            if (track == null)
            {
                var searchedTrack = _spotiFireService.PlaySearchedTrack(query, trackId);
                _trackRepository.Add(searchedTrack);
            }
            else
            {
                //_spotiFireService.EnqueueTrack(track.;
            }
        }
    }
}
