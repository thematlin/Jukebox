using System;
using System.Collections.Generic;
using Jukebox.Business.Models.Contracts;

namespace Jukebox.Business.Models
{
    public class JukeboxTrack : IJukeboxTrack
    {
        public string Name { get; set; }

        public TimeSpan Length { get; set; }

        public string Album { get; set; }

        public IList<IJukeboxArtist> Artists { get; set; }

        public int PlaylistPosition { get; set; }

        public string ArtistsAndName
        {
            get
            {
                var artists = "";

                if (Artists.Count.Equals(1))
                    return Artists[0].Name + " - " + Name;

                foreach (var artist in Artists)
                {
                    if (Name.Contains(artist.Name))
                        continue;

                    artists += artist.Name + ", ";
                }

                artists = artists.TrimEnd(new[] { ',', ' ' });

                return artists + " - " + Name;
            }
        }
    }
}