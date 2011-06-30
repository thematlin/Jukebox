using System;
using System.Collections.Generic;

namespace Jukebox.Business.Models
{
    public class JukeboxTrack
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public TimeSpan Length { get; set; }

        public string Album { get; set; }

        public IList<JukeboxArtist> Artists { get; set; }

        public int PlaylistPosition { get; set; }

        public string ArtistsAndName { get; set; }
    }
}