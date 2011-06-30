using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.Services.SpotiFireService
{
    public class PlaylistHolder : IPlaylistHolder
    {
        public Playlist ApplicationPlaylist { get; set; }
    }
}