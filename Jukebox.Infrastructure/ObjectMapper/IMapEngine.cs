using System.Collections.Generic;
using System.Collections.ObjectModel;
using Jukebox.Business;
using Jukebox.Business.Models;
using Jukebox.Business.Models.Contracts;
using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.ObjectMapper
{
    public interface IMapEngine
    {
        IList<IJukeboxTrack> MapSpotiFireTracksToJukeboxTracks(Track[] tracks);
        ReadOnlyCollection<IJukeboxTrack> MapSpotiFireQueueToJukeboxQueue(Track[] tracks);
        IJukeboxTrack MapSpotiFireTrackToJukeboxTrack(Track track);
        IJukeboxSearch MapSpotiFireSearchToJukeboxSearch(Search search);
    }
}