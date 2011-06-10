using System;

namespace Jukebox.Business.Models.Contracts
{
    public interface IJukeboxPlaylist
    {
        string Description { get; set; }

        Guid Id { get; set; }

        string Name { get; set; }
    }
}