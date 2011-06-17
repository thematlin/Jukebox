using System.Collections.Generic;
using System.Collections.ObjectModel;
using Jukebox.Business.Models.Contracts;

namespace Jukebox.Infrastructure.Services
{
    public interface ISpotiFireService
    {
        IList<IJukeboxTrack> GetPlaylistTracks();
        void EnqueueTrack(int trackId);
        ReadOnlyCollection<IJukeboxTrack> GetLibraryQueue();
        ReadOnlyCollection<IJukeboxTrack> GetCustomQueue();
        void PlayNext();
        void Play();
        void Pause();
        IJukeboxTrack GetCurrentTrack();
        IJukeboxSearch Search(string query);
        void AddTrackFromSearch(string query, int trackId);
        void ConfigureSpotiFire();
    }
}