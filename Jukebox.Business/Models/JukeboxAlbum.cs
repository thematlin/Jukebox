using Jukebox.Business.Models.Contracts;

namespace Jukebox.Business.Models
{
    public class JukeboxAlbum : IJukeboxAlbum
    {
        public IJukeboxArtist Artist { get; set; }

        public string CoverId { get; set; }

        public string Name { get; set; }

        public AlbumType Type { get; set; }

        public int Year { get; set; }
    }

    public enum AlbumType
    {
        Album,
        Compilation,
        Single,
        Unknown
    }
}