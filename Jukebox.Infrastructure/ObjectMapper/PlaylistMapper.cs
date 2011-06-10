using System;
using System.Collections.Generic;
using System.Linq;
using Jukebox.Business;
using Jukebox.Business.Models;
using Jukebox.Business.Models.Contracts;
using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.ObjectMapper
{
    public class PlaylistMapper : IPlaylistMapper
    {
        public IList<IJukeboxPlaylist> MapSpotiFirePlaylistsToJukeboxPlaylists(Playlist[] playlists)
        {
            return playlists.Select(playlist => MapSpotiFirePlaylistToJukeboxPlaylist(playlist)).ToList();
        }

        private IJukeboxPlaylist MapSpotiFirePlaylistToJukeboxPlaylist(Playlist playlist)
        {
            var jukePlaylist = new JukeboxPlaylist();

            jukePlaylist.Description = playlist.Description;
            jukePlaylist.Id = playlist.Id;
            jukePlaylist.Name = playlist.Name;

            return jukePlaylist;
        }
    }
}