using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SpotiFire.SpotifyLib;

namespace SpotiFire.Server
{
    [DataContract]
    public class Search
    {
        public Search(ISearch search)
        {
            Albums = search.Albums.Select(x => new Album(x)).ToArray();
            Artists = search.Artists.Select(x => new Artist(x)).ToArray();
            DidYouMean = search.DidYouMean;
            Query = search.Query;
            TotalAlbums = search.TotalAlbums;
            TotalArtists = search.TotalArtists;
            TotalTracks = search.TotalTracks;
            Tracks = search.Tracks.Select(x => new Track(x)).ToArray();
        }

        [DataMember]
        public Track[] Tracks { get; set; }

        [DataMember]
        public int TotalTracks { get; set; }

        [DataMember]
        public int TotalArtists { get; set; }

        [DataMember]
        public int TotalAlbums { get; set; }

        [DataMember]
        public string Query { get; set; }

        [DataMember]
        public string DidYouMean { get; set; }

        [DataMember]
        public Artist[] Artists { get; set; }

        [DataMember]
        public Album[] Albums { get; set; }
    }
}
