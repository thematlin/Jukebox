using System.Collections.Generic;
using System.Collections.ObjectModel;
using Jukebox.Business.Models;
using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.ObjectMapper
{
    public class MapEngine : IMapEngine
    {
        public IList<JukeboxTrack> MapSpotiFireTracksToJukeboxTracks(Track[] tracks)
        {
            var jukeboxTracks = new List<JukeboxTrack>();

            foreach (var track in tracks)
            {
                var jukeTrack = MapSpotiFireTrackToJukeboxTrack(track);

                jukeboxTracks.Add(jukeTrack);
            }

            return jukeboxTracks;
        }

        public ReadOnlyCollection<JukeboxTrack> MapSpotiFireQueueToJukeboxQueue(Track[] tracks)
        {
            var jukeboxTracks = new List<JukeboxTrack>();

            foreach (var track in tracks)
            {
                var jukeTrack = MapSpotiFireTrackToJukeboxTrack(track);

                jukeboxTracks.Add(jukeTrack);
            }

            var readOnlyCollection = new ReadOnlyCollection<JukeboxTrack>(jukeboxTracks);

            return readOnlyCollection;
        }

        public JukeboxTrack MapSpotiFireTrackToJukeboxTrack(Track track)
        {
            if (track == null)
                return null;

            var jukeboxTrack = new JukeboxTrack();

            jukeboxTrack.Id = track.Id;
            jukeboxTrack.ArtistsAndName = track.ArtistAndName;
            jukeboxTrack.Name = track.Name;
            jukeboxTrack.Length = track.Length;
            jukeboxTrack.Album = track.Album;
            jukeboxTrack.Artists = MapSpotiFireArtistsToJukeboxArtists(track.Artists);

            return jukeboxTrack;
        }

        public JukeboxSearch MapSpotiFireSearchToJukeboxSearch(Search search)
        {
            var jukeSearch = new JukeboxSearch();

            jukeSearch.Albums = MapSpotiFireAlbumsToJukeboxAlbums(search.Albums);
            jukeSearch.Artists = MapSpotiFireArtistsToJukeboxArtists(search.Artists);
            jukeSearch.DidYouMean = search.DidYouMean;
            jukeSearch.Query = search.Query;
            jukeSearch.TotalAlbums = search.TotalAlbums;
            jukeSearch.TotalArtists = search.TotalArtists;
            jukeSearch.TotalTracks = search.TotalTracks;
            jukeSearch.Tracks = MapSpotiFireTracksToJukeboxTracks(search.Tracks);

            return jukeSearch;
        }

        private IList<JukeboxAlbum> MapSpotiFireAlbumsToJukeboxAlbums(Album[] albums)
        {
            var jukeAlbums = new List<JukeboxAlbum>();

            foreach (var album in albums)
            {
                jukeAlbums.Add(MapSpotiFireAlbumToJukeboxAlbum(album));
            }

            return jukeAlbums;
        }

        private JukeboxAlbum MapSpotiFireAlbumToJukeboxAlbum(Album album)
        {
            var jukeAlbum = new JukeboxAlbum();

            jukeAlbum.Artist = MapSpotiFireArtistToJukeboxArtist(album.Artist);
            jukeAlbum.CoverId = album.CoverId;
            jukeAlbum.Name = album.Name;
            jukeAlbum.Type = MapSpotiFireAlbumTypeToJukeboxAlbumType(album.Type);
            jukeAlbum.Year = album.Year;

            return jukeAlbum;
        }

        private AlbumType MapSpotiFireAlbumTypeToJukeboxAlbumType(sp_albumtype type)
        {
            if (type == sp_albumtype.SP_ALBUMTYPE_ALBUM)
                return AlbumType.Album;
            if (type == sp_albumtype.SP_ALBUMTYPE_COMPILATION)
                return AlbumType.Compilation;
            if (type == sp_albumtype.SP_ALBUMTYPE_SINGLE)
                return AlbumType.Single;
         
            return AlbumType.Unknown;
        }

        private IList<JukeboxArtist> MapSpotiFireArtistsToJukeboxArtists(Artist[] artists)
        {
            var jukeboxArtists = new List<JukeboxArtist>();

            foreach (var artist in artists)
            {
                jukeboxArtists.Add(MapSpotiFireArtistToJukeboxArtist(artist));
            }

            return jukeboxArtists;
        }

        private JukeboxArtist MapSpotiFireArtistToJukeboxArtist(Artist artist)
        {
            var jukeboxArtist = new JukeboxArtist();

            jukeboxArtist.Name = artist.Name;
            jukeboxArtist.Link = artist.Link;

            return jukeboxArtist;
        }
    }
}