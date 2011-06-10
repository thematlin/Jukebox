namespace Jukebox.Business.Models.Contracts
{
    public interface IJukeboxAlbum
    {
        IJukeboxArtist Artist { get; set; }

        string CoverId { get; set; }

        string Name { get; set; }

        AlbumType Type { get; set; }

        int Year { get; set; }
    }
}