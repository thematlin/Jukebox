using System.Collections.Generic;
using Jukebox.Business.Models;

namespace Jukebox.Infrastructure.Repositories
{
    public interface IJukeboxTrackRepository
    {
        void AddRange(IEnumerable<JukeboxTrack> tracks);
        void Add(JukeboxTrack track);
        IEnumerable<JukeboxTrack> Get();
        JukeboxTrack GetTrackById(string id);
    }
}