using System.Collections.Generic;
using Jukebox.Business.Models;

namespace Jukebox.Infrastructure.Services
{
    public interface IJukeboxService
    {
        IEnumerable<JukeboxTrack> GetAllTracks();
        JukeboxSearch Search(string query);
        void PlayTrack(string query, string trackId);
    }
}