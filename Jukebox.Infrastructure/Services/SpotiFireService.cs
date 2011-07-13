using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using Jukebox.Business.Models.Contracts;
using Jukebox.Infrastructure.ObjectMapper;
using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.Services
{
    public class SpotiFireService : ISpotiFireService, IDisposable
    {
        private readonly SpotifyClient _spotiFire;
        private readonly IMapEngine _mapEngine;
        private readonly IPlaylistHolder _playlistHolder;

        public SpotiFireService(IMapEngine mapEngine, IPlaylistHolder playlistHolder)
        {
            _mapEngine = mapEngine;
            _spotiFire = new SpotifyClient();
            _playlistHolder = playlistHolder;
        }

        public IList<IJukeboxTrack> GetPlaylistTracks()
        {
            var tracks = _spotiFire.GetPlaylistTracks(_playlistHolder.ApplicationPlaylist.Id);

            return _mapEngine.MapSpotiFireTracksToJukeboxTracks(tracks);
        }

        public void EnqueueTrack(int trackId)
        {
            _spotiFire.EnqueueTrack(_playlistHolder.ApplicationPlaylist.Id, trackId);
        }

        public void PlayPlaylistTrack(int trackId)
        {
            _spotiFire.PlayPlaylistTrack(_playlistHolder.ApplicationPlaylist.Id, trackId);
        }

        private Playlist GetPreConfiguredPlaylist()
        {
            var playlists = _spotiFire.GetPlaylists();

            foreach (var playlist in playlists)
            {
                if (playlist.Name.ToLower().Equals(ConfigurationManager.AppSettings["ApplicationPlaylist"].ToLower()))
                    return playlist;
            }

            throw new Exception("Couldn't find this application's playlist as configured in web.config");
        }

        private void GuardLogIn()
        {
            var status = _spotiFire.Authenticate("");

            if (status == AuthenticationStatus.Ok)
                return;
            if (status == AuthenticationStatus.Bad)
                throw new Exception("Something is terribly wrong with the service");
            if (status == AuthenticationStatus.RequireLogin)
            {
                var loggedIn = _spotiFire.Login(ConfigurationManager.AppSettings["SpotifyUserName"],
                                                ConfigurationManager.AppSettings["SpotifyPassword"]);
            }
        }

        public void ConfigureSpotiFire()
        {
            GuardLogIn();

            if (_playlistHolder.ApplicationPlaylist == null)
                _playlistHolder.ApplicationPlaylist = GetPreConfiguredPlaylist();
        }

        public ReadOnlyCollection<IJukeboxTrack> GetLibraryQueue()
        {
            var spotiFireCustomQueue = _spotiFire.GetQueue();

            return _mapEngine.MapSpotiFireQueueToJukeboxQueue(spotiFireCustomQueue);
        }

        public ReadOnlyCollection<IJukeboxTrack> GetCustomQueue()
        {
            var spotiFireCustomQueue = _spotiFire.GetCustomQueue();

            return _mapEngine.MapSpotiFireQueueToJukeboxQueue(spotiFireCustomQueue);
        }

        public void PlayNext()
        {
            _spotiFire.PlayNext(_playlistHolder.ApplicationPlaylist.Id);
        }

        public void Play()
        {
            _spotiFire.PlayPause(_playlistHolder.ApplicationPlaylist.Id);
        }

        public void Pause()
        {
            _spotiFire.PlayPause(_playlistHolder.ApplicationPlaylist.Id);
        }

        public IJukeboxTrack GetCurrentTrack()
        {
            var track = _spotiFire.GetCurrentTrack();
            
            return _mapEngine.MapSpotiFireTrackToJukeboxTrack(track);
        }

        public IJukeboxSearch Search(string query)
        {
            var search = _spotiFire.Search(query);

            return _mapEngine.MapSpotiFireSearchToJukeboxSearch(search);
        }

        public void AddTrackFromSearch(string query, int trackId)
        {
            _spotiFire.AddTrackFromSearchToPlaylist(_playlistHolder.ApplicationPlaylist.Id, query, trackId);
        }

        public void Dispose()
        {
            _spotiFire.Close();
        }
    }
}