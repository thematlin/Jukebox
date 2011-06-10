using Jukebox.Business.Models.Contracts;

namespace Jukebox.Business.Models
{
    public class JukeboxArtist : IJukeboxArtist
    {
        public string Name { get; set; }

        public string Link { get; set; }
    }
}