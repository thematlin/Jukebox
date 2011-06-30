using System.Collections.Generic;
using System.Collections.ObjectModel;
using Jukebox.Business.Models;
using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.ObjectMapper
{
    public interface IMapEngine
    {
        IList<JukeboxTrack> MapSpotiFireTracksToJukeboxTracks(Track[] tracks);
        ReadOnlyCollection<JukeboxTrack> MapSpotiFireQueueToJukeboxQueue(Track[] tracks);
        JukeboxTrack MapSpotiFireTrackToJukeboxTrack(Track track);
        JukeboxSearch MapSpotiFireSearchToJukeboxSearch(Search search);
    }
}