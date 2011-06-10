using System;
using System.Collections.Generic;
using Jukebox.Business;
using Jukebox.Business.Models;
using Jukebox.Business.Models.Contracts;
using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.ObjectMapper
{
    public interface IPlaylistMapper
    {
        IList<IJukeboxPlaylist> MapSpotiFirePlaylistsToJukeboxPlaylists(Playlist[] playlists);
    }
}