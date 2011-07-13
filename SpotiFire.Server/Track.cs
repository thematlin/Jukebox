using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using SpotiFire.SpotifyLib;

namespace SpotiFire.Server
{
    [DataContract, DebuggerDisplay("Track - {Name}")]
    public class Track
    {
        public Track(ITrack track)
        {
            Name = track.Name;
            Artists = track.Artists.Select(a => new Artist(a)).ToArray();
            Album = track.Album.Name;
            Length = track.Duration;
            IsAvailable = track.IsAvailable;
            Popularity = track.Popularity;
            IsStarred = track.IsStarred;
        }

        [DataMember]
        public string Id
        {
            get
            {
                var uniqueHash = ArtistAndName + Album;

                var hashish = uniqueHash.GetHashCode();
                hashish = Math.Abs(hashish) * 1;
                return hashish.ToString();
            }
            internal set { if (value == null) throw new ArgumentNullException("value"); }
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Artist[] Artists { get; set; }

        [DataMember]
        public string Album { get; set; }

        [DataMember]
        public TimeSpan Length { get; set; }

        [DataMember]
        public bool IsAvailable { get; set; }

        [DataMember]
        public int Popularity { get; set; }

        [DataMember]
        public bool IsStarred { get; set; }

        [DataMember]
        public string ArtistAndName 
        { 
            get
            {
                var artists = "";

                if (Artists.Count().Equals(1))
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
            internal set { if (value == null) throw new ArgumentNullException("value"); }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Track)) return false;
            return Equals((Track) obj);
        }

        public bool Equals(Track other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = (Name != null ? Name.GetHashCode() : 0);
                result = (result*397) ^ (Artists != null ? Artists.GetHashCode() : 0);
                result = (result*397) ^ (Album != null ? Album.GetHashCode() : 0);
                result = (result*397) ^ Length.GetHashCode();
                result = (result*397) ^ IsAvailable.GetHashCode();
                result = (result*397) ^ Popularity;
                result = (result*397) ^ IsStarred.GetHashCode();
                result = (result*397) ^ Id.GetHashCode();
                return result;
            }
        }
    }
}
