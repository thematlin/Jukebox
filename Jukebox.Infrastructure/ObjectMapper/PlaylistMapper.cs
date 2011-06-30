using System.Collections.Generic;
using System.Linq;
using Jukebox.Business.Models;
using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.ObjectMapper
{
    public class PlaylistMapper : IPlaylistMapper
    {
        public IList<JukeboxPlaylist> MapSpotiFirePlaylistsToJukeboxPlaylists(Playlist[] playlists)
        {
            return playlists.Select(playlist => MapSpotiFirePlaylistToJukeboxPlaylist(playlist)).ToList();
        }

        private JukeboxPlaylist MapSpotiFirePlaylistToJukeboxPlaylist(Playlist playlist)
        {
            var jukePlaylist = new JukeboxPlaylist();

            jukePlaylist.Description = playlist.Description;
            jukePlaylist.Id = playlist.Id;
            jukePlaylist.Name = playlist.Name;

            return jukePlaylist;
        }
    }
}