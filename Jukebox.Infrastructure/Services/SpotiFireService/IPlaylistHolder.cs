using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.Services.SpotiFireService
{
    public interface IPlaylistHolder
    {
        Playlist ApplicationPlaylist { get; set; }
    }
}
