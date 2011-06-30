using System;

namespace Jukebox.Business.Models
{
    public class JukeboxPlaylist
    {
        public string Description { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}