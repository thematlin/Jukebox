using System;
using Jukebox.Business.Models.Contracts;

namespace Jukebox.Business.Models
{
    public class JukeboxPlaylist : IJukeboxPlaylist
    {
        public string Description { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}