using System;
using Jukebox.Infrastructure.SpotiFireServer;
namespace Jukebox.Infrastructure.Services
{
    public class PlaylistHolder : IPlaylistHolder
    {
        public Playlist ApplicationPlaylist { get; set; }
    }

    public interface IPlaylistHolder
    {
        Playlist ApplicationPlaylist { get; set; }
    }
}
