using System.Runtime.Serialization;
using SpotiFire.SpotifyLib;

namespace SpotiFire.Server
{
    [DataContract]
    public class Album
    {
        public Album(IAlbum album)
        {
            Artist = new Artist(album.Artist);
            CoverId = album.CoverId;
            Name = album.Name;
            Type = album.Type;
            Year = album.Year;
        }
        
        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public sp_albumtype Type { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string CoverId { get; set; }

        [DataMember]
        public Artist Artist { get; set; }
    }
}