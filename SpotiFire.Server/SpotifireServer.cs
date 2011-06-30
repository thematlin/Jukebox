using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Threading;
using SpotiFire.SpotifyLib;
using c = System.ServiceModel.Channels;
namespace SpotiFire.Server
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    class SpotifireServer : ISpotifireServer, IErrorHandler
    {
        public static SpotifireServer Instance { get; private set; }
        private BASSPlayer player = null;
        static SpotifireServer()
        {
            Instance = null;
        }

        private string password;
        private ISession spotify;
        private LiveQueue<ITrack> playQueue;
        private AutoResetEvent loginComplete = new AutoResetEvent(false);
        private readonly object canLoginLock = new object();
        private Playlist playlist = null;
        private ManualResetEvent waitHandle = new ManualResetEvent(false);

        private DateTime trackStartTime = DateTime.Now;
        private DateTime trackPauseTime = DateTime.MinValue;

        private bool playing = false;

        public SpotifireServer(string password = null)
        {
            if (Instance != null)
                throw new InvalidOperationException("Can't have 2 servers!");
            Instance = this;

            this.password = password;
            this.spotify = Spotify.CreateSession(Program.applicationKey, Program.cacheLocation, Program.settingsLocation, Program.userAgent);
            this.spotify.SetPrefferedBitrate(sp_bitrate.BITRATE_320k);

            this.spotify.LoginComplete += new SessionEventHandler(spotify_LoginComplete);
            this.spotify.LogoutComplete += new SessionEventHandler(spotify_LogoutComplete);

            this.spotify.EndOfTrack += new SessionEventHandler(spotify_EndOfTrack);
            this.spotify.MusicDeliver += new MusicDeliveryEventHandler(spotify_MusicDeliver);

            this.playQueue = new LiveQueue<ITrack>();
            playQueue.Repeat = true;

            player = new BASSPlayer();
        }

        public void Run()
        {
            waitHandle.WaitOne();
        }

        void spotify_MusicDeliver(ISession sender, MusicDeliveryEventArgs e)
        {
            if (e.Samples.Length > 0)
            {
                e.ConsumedFrames = player.EnqueueSamples(e.Channels, e.Rate, e.Samples, e.Frames);
            }
            else
            {
                e.ConsumedFrames = 0;
            }
        }

        void spotify_EndOfTrack(ISession sender, SessionEventArgs e)
        {
            Console.WriteLine("End Of a track");
            if (playQueue.Count > 0)
            {
                var track = playQueue.Dequeue();
                spotify.PlayerLoad(track);
                spotify.PlayerPlay();
            }
            else
            {
                playQueue.Dequeue(true);
            }

            playing = true;
        }

        void spotify_LogoutComplete(ISession sender, SessionEventArgs e)
        {
            // Message about logout.
        }

        void spotify_LoginComplete(ISession sender, SessionEventArgs e)
        {
            loginComplete.Set();
        }

        ~SpotifireServer()
        {
            GC.KeepAlive(spotify);
            spotify.Dispose();
        }

        public AuthenticationStatus Authenticate(string password)
        {
            if (this.password != null && password != this.password)
            {
                return AuthenticationStatus.Bad;
            }


            if (spotify.ConnectionState == sp_connectionstate.LOGGED_OUT)
                return AuthenticationStatus.RequireLogin;

            return AuthenticationStatus.Ok;
        }

        public bool Login(string username, string password)
        {
            lock (canLoginLock)
            {
                if (spotify.ConnectionState == sp_connectionstate.LOGGED_OUT)
                {
                    spotify.Login(username, password);
                    loginComplete.WaitOne();
                    return spotify.ConnectionState == sp_connectionstate.LOGGED_IN;
                }
                
                return true;
            }
        }


        public IEnumerable<Playlist> GetPlaylists()
        {
            yield return Playlist.Get(spotify.Starred);
            foreach (var p in spotify.PlaylistContainer.Playlists)
                yield return Playlist.Get(p);
        }

        public IEnumerable<Track> GetPlaylistTracks(Guid id)
        {
            Console.WriteLine("GetPlaylistTracks");
            Playlist playlist = Playlist.GetById(id);
            return playlist.playlist.Tracks.Select(t => new Track(t)).ToArray();
        }

        public void PlayPlaylistTrack(Guid playlistId, int position)
        {
            
            var track = GetTrack(playlistId, position);

            if (position != -1)
                spotify.PlayerLoad(track);
            playQueue.Feed = playlist.playlist.Tracks.Cast<ITrack>();
            if (position == -1)
                spotify.PlayerLoad(playQueue.Dequeue());
            if (position != -1)
                playQueue.Index = position;
            if (position != -1)
                playQueue.Current = playlist.playlist.Tracks[position];
            spotify.PlayerPlay();
            playing = true;
            Console.WriteLine("PlayPlaylistTrack: " + track.Name);
        }

        private IPlaylistTrack GetTrack(Guid playlistId, int position)
        {
            Playlist playlist = Playlist.GetById(playlistId);
            this.playlist = playlist;

            var tracks = playlist.playlist.Tracks;
            var track = tracks[position];

            return track;
        }

        public ITrack GetTrack(Guid playlistId, string id)
        {
            var playlist = Playlist.GetById(playlistId);

            return playlist.playlist.Tracks.FirstOrDefault(itrack => new Track(itrack).Id == id);
        }

        public void SetShuffle(bool shuffle)
        {
            playQueue.Shuffle = shuffle;
        }

        public void SetRepeat(bool repeat)
        {
            playQueue.Repeat = repeat;
        }

        public void Exit()
        {
            waitHandle.Set();
        }

        public void SetVolume(int volume)
        {
            volume = Math.Max(Math.Max(volume, 0), 100);
            player.Volume = (volume / 100);
        }

        public int GetVolume()
        {
            return (int)(player.Volume * 100);
        }

        public void PlayPause(Guid playlistId)
        {
            Console.WriteLine("PlayPause");
            if (playing)
            {
                spotify.PlayerPause();
                playing = false;
            }
            else
            {
                if (playQueue.Current == null || playQueue.Count < 1)
                {
                    var rand = new Random();
                    var position = rand.Next(Playlist.GetById(playlistId).playlist.Tracks.Count);

                    var randomTrack = GetTrack(playlistId, position);

                    playQueue.Feed = playlist.playlist.Tracks.Cast<ITrack>();
                    spotify.PlayerLoad(randomTrack);
                    playQueue.Index = position;
                    playQueue.Current = randomTrack;
                }

                spotify.PlayerPlay();

                playing = true;
            }
                
        }

        public void Load(ITrack track)
        {
            
            if (track != null)
                spotify.PlayerLoad(track);
        }

        public bool HandleError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void ProvideFault(Exception error, c.MessageVersion version, ref c.Message fault)
        {
            if (error is FaultException)
                return;
        }

        public SpotifyStatus GetStatus()
        {
            return new SpotifyStatus
            {
                IsPlaying = playing,
                CanGoBack = false,
                CanGoNext = false,
                CanStartPlayback = playQueue.Current != null,
                CurrentTrack = playQueue.Current != null ? new Track(playQueue.Current) : null,
                LengthPlayed = playQueue.Current != null ? (playing ? DateTime.Now.Subtract(trackStartTime) : trackPauseTime.Subtract(trackStartTime)) : TimeSpan.Zero,
                Repeat = playQueue.Repeat,
                Shuffle = playQueue.Shuffle,
                Volume = GetVolume()
            };
        }

        public Track GetCurrentTrack()
        {
            return playQueue.Current == null ? null : new Track(playQueue.Current);
        }

        public void EnqueueTrack(Guid playlistId, int position)
        {
            Console.WriteLine("Enqueue");

            var track = GetTrack(playlistId, position);

            if (playQueue.Current == null)
            {
                playQueue.Feed = playlist.playlist.Tracks.Cast<ITrack>();
                spotify.PlayerLoad(track);
                playQueue.Index = position;
                playQueue.Current = track;
                
                spotify.PlayerPlay();
                playing = true;
            }
            else
            {
                var lastAdded = playQueue.Last();
                var wrappedtrack = new Track(lastAdded);
                Console.WriteLine("Enqueue info:");
                Console.WriteLine("last track in queue: " + wrappedtrack.Name);
                Console.WriteLine("adding track: " + track.Name);
                playQueue.Enqueue(track);
            }
        }

        public void EnqueueTrack(Guid playlistId, string trackId)
        {
            Console.WriteLine("Enqueue by trackId");

            var track = GetTrack(playlistId, trackId);

            if (playQueue.Current == null)
            {
                playQueue.Feed = playlist.playlist.Tracks.Cast<ITrack>();
                spotify.PlayerLoad(track);
                playQueue.Index = track.Index;
                playQueue.Current = track;

                spotify.PlayerPlay();
                playing = true;
            }
            else
            {
                var lastAdded = playQueue.Last();
                var wrappedtrack = new Track(lastAdded);
                Console.WriteLine("Enqueue info:");
                Console.WriteLine("last track in queue: " + wrappedtrack.Name);
                Console.WriteLine("adding track: " + track.Name);
                playQueue.Enqueue(track);
            }
        }

        public void PlayNext(Guid playlistId)
        {
            Console.WriteLine("PlayNExt");
            if (playQueue.Current == null || playQueue.Count < 1)
            {
                var rand = new Random();
                var position = rand.Next(Playlist.GetById(playlistId).playlist.Tracks.Count);

                var randomTrack = GetTrack(playlistId, position);

                playQueue.Feed = playlist.playlist.Tracks.Cast<ITrack>();
                spotify.PlayerLoad(randomTrack);
                playQueue.Index = position;
                playQueue.Current = randomTrack;

                spotify.PlayerPlay();
            }
            else
            {
                var track = playQueue.Dequeue();
                spotify.PlayerLoad(track);
                playQueue.Current = track;

                spotify.PlayerPlay();
            }

            playing = true;
        }

        public IEnumerable<Track> GetQueue()
        {
            Console.WriteLine("GetQueue");

            return playQueue.Queue.Select(t => new Track(t)).ToArray();
        }

        public IEnumerable<Track> GetCustomQueue()
        {
            Console.WriteLine("GetCustomQueue");
            return playQueue.CustomQueue.Select(t => new Track(t)).ToArray();
        }

        public Search Search(string query)
        {
            Console.WriteLine("Search");
            var search = spotify.Search(query, 0, 100, 0, 100, 0, 100);

            while (search.Error == sp_error.IS_LOADING)
            {
                Thread.Sleep(TimeSpan.FromSeconds(.1));
            }
            
            return new Search(search);
        }

        public void AddTrackFromSearchToPlaylist(Guid playlistId, string query, string trackId)
        {
            Console.WriteLine("AddTrackFromSearch");
            SearchAndAddTrack(query, trackId, playlistId);
        }

        private Track SearchAndAddTrack(string query, string trackId, Guid playlistId)
        {
            var search = spotify.Search(query, 0, 100, 0, 100, 0, 100);

            while (search.Error == sp_error.IS_LOADING)
            {
                Thread.Sleep(TimeSpan.FromSeconds(.1));
            }

            var playlistTracks = search.Tracks;
            var trackToAdd = playlistTracks.Single(x => new Track(x).Id == trackId);

            var thePlaylist = Playlist.GetById(playlistId);
            
            thePlaylist.playlist.Tracks.Add(trackToAdd, trackToAdd.Index);

            var track = new Track(trackToAdd);

            EnqueueTrack(playlistId, track.Id);

            return track;
        }

        public Track PlaySearchedTrack(Guid playlistId, string query, string trackId)
        {
            return SearchAndAddTrack(query, trackId, playlistId);
        }
    }
}
