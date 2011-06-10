using System.Collections.Generic;
using Jukebox.Business.Models.Contracts;

namespace Jukebox.Business.Models
{
    public class JukeboxSearch : IJukeboxSearch
    {
        public IList<IJukeboxAlbum> Albums { get; set; }

        public IList<IJukeboxArtist> Artists { get; set; }

        public string DidYouMean { get; set; }

        public string Query { get; set; }

        public int TotalAlbums { get; set; }

        public int TotalArtists { get; set; }

        public int TotalTracks { get; set; }

        public IList<IJukeboxTrack> Tracks { get; set; }
    }
}