using System.Collections.Generic;
using Jukebox.Business.Models.Contracts;
using Jukebox.Infrastructure.Repositories;
using Jukebox.Infrastructure.Validators;

namespace Jukebox.Infrastructure.Services.PersistenceServices
{
    public class LibraryPersistenceService : ILibraryPersistenceService
    {
        private readonly IRavenRepository _ravenRepository;
        private readonly ILibraryValidator _libraryValidator;

        public LibraryPersistenceService(ILibraryValidator libraryValidator, IRavenRepository ravenRepository)
        {
            _libraryValidator = libraryValidator;
            _ravenRepository = ravenRepository;
        }

        public void PersistTracks(IList<IJukeboxTrack> tracks)
        {
            //_ravenRepository.AddRange(tracks);

            _libraryValidator.Validate();
        }
    }
}
