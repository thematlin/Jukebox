using System;
using System.Collections.Generic;

namespace Jukebox.Business.Models.Contracts
{
    public interface IJukeboxTrack : IPersistable
    {
        string Name { get; set; }

        TimeSpan Length { get; set; }

        string Album { get; set; }

        IList<IJukeboxArtist> Artists { get; set; }
        int PlaylistPosition { get; set; }

        string ArtistsAndName { get; }
    }
}