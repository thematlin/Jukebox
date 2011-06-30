using System.Collections.Generic;
using Jukebox.Business.Models;
using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.ObjectMapper
{
    public interface IPlaylistMapper
    {
        IList<JukeboxPlaylist> MapSpotiFirePlaylistsToJukeboxPlaylists(Playlist[] playlists);
    }
}