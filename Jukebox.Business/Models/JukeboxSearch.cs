using System.Collections.Generic;
using System.Linq;

namespace Jukebox.Business.Models
{
    public class JukeboxSearch
    {
        public IList<JukeboxAlbum> Albums { get; set; }

        public IList<JukeboxArtist> Artists { get; set; }

        public string DidYouMean { get; set; }

        public string Query { get; set; }

        public int TotalAlbums { get; set; }

        public int TotalArtists { get; set; }

        public int TotalTracks { get; set; }

        public IList<JukeboxTrack> Tracks { get; set; }

        public IList<IList<JukeboxTrack>> TracksSeparatedAlphabetically
        {
            get
            {
                var lists = new List<IList<JukeboxTrack>>();

                var letters = new HashSet<string>(Tracks.Select(x => x.ArtistsAndName.Substring(0, 1)));

                foreach (var letter in letters)
                {
                    var tracksForCurrentLetter = Tracks.Where(x => x.ArtistsAndName.StartsWith(letter));

                    lists.Add(tracksForCurrentLetter.ToList());
                }

                return lists;
            }
        }
    }
}