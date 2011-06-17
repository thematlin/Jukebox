using System.Collections.Generic;
using Jukebox.Business.Models.Contracts;

namespace Jukebox.Infrastructure.Services.PersistenceServices
{
    public interface ILibraryPersistenceService
    {
        void PersistTracks(IList<IJukeboxTrack> tracks);
    }
}