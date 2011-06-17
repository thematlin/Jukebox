using System;
using System.Collections.Generic;
using System.Linq;
using Jukebox.Infrastructure.Services;
using Jukebox.Infrastructure.Repositories;
using Jukebox.Infrastructure.Validators;
using Jukebox.Business.Models.Contracts;
using Jukebox.Infrastructure.Services.PersistenceServices;
namespace Jukebox.Infrastructure.DAO
{
    public class LibraryDataAccess : ILibraryDataAccess
    {
        private readonly ISpotiFireService _spotiFireService;
        private readonly IRavenRepository _ravenRepository;
        private readonly ILibraryValidator _libraryValidator;
        private readonly ILibraryPersistenceService _libraryPersistenceService;

        public LibraryDataAccess(ISpotiFireService spotiFireService, IRavenRepository ravenRepository, ILibraryValidator libraryValidator, ILibraryPersistenceService libraryPersistenceService)
        {
            _spotiFireService = spotiFireService;
            _libraryPersistenceService = libraryPersistenceService;
            _libraryValidator = libraryValidator;
            _ravenRepository = ravenRepository;
        }

        public IList<IJukeboxTrack> GetLibraryTracks()
        {
            if (_libraryValidator.Valid)
            {
                return _ravenRepository.All<IJukeboxTrack>().ToList();
            }

            var tracks = _spotiFireService.GetPlaylistTracks();

            Action<IList<IJukeboxTrack>> async = _libraryPersistenceService.PersistTracks;
            async.BeginInvoke(tracks, null, null);

            return tracks;
        }
    }

    public interface ILibraryDataAccess
    {
        IList<IJukeboxTrack> GetLibraryTracks();
    }
}
