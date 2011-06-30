using System.Collections.Generic;
using System.Collections.ObjectModel;
using Jukebox.Business.Models;

namespace Jukebox.Infrastructure.Services.SpotiFireService
{
    public interface ISpotiFireService
    {
        IList<JukeboxTrack> GetPlaylistTracks();
        void EnqueueTrack(int trackId);
        ReadOnlyCollection<JukeboxTrack> GetLibraryQueue();
        ReadOnlyCollection<JukeboxTrack> GetCustomQueue();
        void PlayNext();
        void Play();
        void Pause();
        JukeboxTrack GetCurrentTrack();
        JukeboxSearch Search(string query);
        void AddTrackFromSearch(string query, string trackId);
        void ConfigureSpotiFire();
        JukeboxTrack PlaySearchedTrack(string query, string trackId);
    }
}