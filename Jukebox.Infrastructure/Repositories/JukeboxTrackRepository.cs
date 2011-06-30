using System.Collections.Generic;
using System.Linq;
using Jukebox.Infrastructure.Repositories.RavenBaseRepo;
using Raven.Client;
using Jukebox.Business.Models;

namespace Jukebox.Infrastructure.Repositories
{
    public class JukeboxTrackRepository : RavendbRepository<JukeboxTrack>, IJukeboxTrackRepository
    {
        public JukeboxTrackRepository(IDocumentSession session) : base(session)
        {
        }

        public JukeboxTrack GetTrackById(string id)
        {
            return SingleOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable<JukeboxTrack> Get()
        {
            return All<JukeboxTrack>();
        }

        public void Add(JukeboxTrack track)
        {
            base.Add(track);
        }

        public void AddRange(IEnumerable<JukeboxTrack> tracks)
        {
            base.AddRange(tracks.ToList());
        }
    }
}
