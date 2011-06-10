using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Jukebox.Business;
using Jukebox.Business.Models;
using Jukebox.Business.Models.Contracts;
using Jukebox.Infrastructure.SpotiFireServer;

namespace Jukebox.Infrastructure.ObjectMapper
{
    public class MapEngine : IMapEngine
    {
        public IList<IJukeboxTrack> MapSpotiFireTracksToJukeboxTracks(Track[] tracks)
        {
            var jukeboxTracks = new List<IJukeboxTrack>();

            var trackCount = 0;
            foreach (var track in tracks)
            {
                var jukeTrack = MapSpotiFireTrackToJukeboxTrack(track);
                jukeTrack.PlaylistPosition = trackCount;

                jukeboxTracks.Add(jukeTrack);

                trackCount++;
            }

            return jukeboxTracks;
        }

        public ReadOnlyCollection<IJukeboxTrack> MapSpotiFireQueueToJukeboxQueue(Track[] tracks)
        {
            var jukeboxTracks = new List<IJukeboxTrack>();

            var trackCount = 0;
            foreach (var track in tracks)
            {
                var jukeTrack = MapSpotiFireTrackToJukeboxTrack(track);
                jukeTrack.PlaylistPosition = trackCount;

                jukeboxTracks.Add(jukeTrack);

                trackCount++;
            }

            var readOnlyCollection = new ReadOnlyCollection<IJukeboxTrack>(jukeboxTracks);

            return readOnlyCollection;
        }

        public IJukeboxTrack MapSpotiFireTrackToJukeboxTrack(Track track)
        {
            if (track == null)
                return null;

            var jukeboxTrack = new JukeboxTrack();

            jukeboxTrack.Name = track.Name;
            jukeboxTrack.Length = track.Length;
            jukeboxTrack.Album = track.Album;
            jukeboxTrack.Artists = MapSpotiFireArtistsToJukeboxArtists(track.Artists);

            return jukeboxTrack;
        }

        public IJukeboxSearch MapSpotiFireSearchToJukeboxSearch(Search search)
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

        private IList<IJukeboxAlbum> MapSpotiFireAlbumsToJukeboxAlbums(Album[] albums)
        {
            var jukeAlbums = new List<IJukeboxAlbum>();

            foreach (var album in albums)
            {
                jukeAlbums.Add(MapSpotiFireAlbumToJukeboxAlbum(album));
            }

            return jukeAlbums;
        }

        private IJukeboxAlbum MapSpotiFireAlbumToJukeboxAlbum(Album album)
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

        private IList<IJukeboxArtist> MapSpotiFireArtistsToJukeboxArtists(Artist[] artists)
        {
            var jukeboxArtists = new List<IJukeboxArtist>();

            foreach (var artist in artists)
            {
                jukeboxArtists.Add(MapSpotiFireArtistToJukeboxArtist(artist));
            }

            return jukeboxArtists;
        }

        private IJukeboxArtist MapSpotiFireArtistToJukeboxArtist(Artist artist)
        {
            var jukeboxArtist = new JukeboxArtist();

            jukeboxArtist.Name = artist.Name;
            jukeboxArtist.Link = artist.Link;

            return jukeboxArtist;
        }
    }
}