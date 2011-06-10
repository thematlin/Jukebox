using System.Collections.Generic;

namespace Jukebox.Business.Models.Contracts
{
    public interface IJukeboxSearch
    {
        IList<IJukeboxAlbum> Albums { get; set; }

        IList<IJukeboxArtist> Artists { get; set; }

        string DidYouMean { get; set; }

        string Query { get; set; }

        int TotalAlbums { get; set; }

        int TotalArtists { get; set; }

        int TotalTracks { get; set; }

        IList<IJukeboxTrack> Tracks { get; set; }
    }
}