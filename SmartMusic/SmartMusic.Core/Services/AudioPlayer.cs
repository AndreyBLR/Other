using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Editing;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Core;
using SmartMusic.Core.DataModel;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Helpers;
using SmartMusic.Core.Repositories;

namespace SmartMusic.Core.Services
{
    public class AudioPlayer
    {
        #region Fields

        private static AudioPlayer _instance;

        private MediaPlayer _mediaPlayer;
        private SystemMediaTransportControls _smtc;
        
        #endregion

        #region Properties

        public MediaPlayerState State
        {
            get { return _mediaPlayer.CurrentState; }
        }

        public TimeSpan Position
        {
            get { return _mediaPlayer.Position; }
        }

        public Playlist<Track> Playlist { get; private set; }

        public static AudioPlayer Instance
        {
            get { return _instance ?? (_instance = new AudioPlayer()); }
        }

        #endregion

        #region Events

        public event EventHandler<Track> PlayStarted;
        public event EventHandler<Track> PlayEnded; 

        #endregion

        private AudioPlayer()
        {
            Playlist = new Playlist<Track>();
        }

        #region Public Methods

        public void Initialize()
        {
            _mediaPlayer = BackgroundMediaPlayer.Current;
            _smtc = SystemMediaTransportControls.GetForCurrentView();

            _smtc.ButtonPressed += SmtcButtonPressed;
            _smtc.PropertyChanged += SmtcPropertyChanged;

            _mediaPlayer.MediaOpened += MediaOpened;
            _mediaPlayer.MediaEnded += MediaEnded;

            BackgroundMediaPlayer.MessageReceivedFromBackground += MessageReceivedFromBackground;
            MusicRepository.Instance.MusicCollectionLoaded += MusicCollectionLoaded;
        }

        public void Shutdown()
        {
            BackgroundMediaPlayer.MessageReceivedFromBackground -= MessageReceivedFromBackground;

            _mediaPlayer.MediaOpened -= MediaOpened;
            _mediaPlayer.MediaEnded -= MediaEnded;

            MusicRepository.Instance.MusicCollectionLoaded -= MusicCollectionLoaded;
        }

        public async void AddToCurrentPlaylist(Track track)
        {
            Playlist.Tracks.Add(track);

            var message = new ValueSet {{ BackgroundMediaMessages.AddTrackToPlaylist, track.Path }};
            BackgroundMediaPlayer.SendMessageToBackground(message);
        }

        public void SetPlaylist(List<Track> items)
        {
            if (Playlist.Current != null)
                Playlist.Current.IsPlayed = false;

            Playlist.ResetPlaylist();
            BackgroundMediaPlayer.SendMessageToBackground(new ValueSet { { BackgroundMediaMessages.ResetPlaylist, string.Empty } });

            foreach (var item in items)
            {
                Playlist.Tracks.Add(item);

                var message = new ValueSet {{BackgroundMediaMessages.AddTrackToPlaylist, item.Path}};
                BackgroundMediaPlayer.SendMessageToBackground(message);
            }

            SaveCurrentPlaylist();
        }

        public async void PlayTrack(int position)
        {
            if (Playlist.Current != null)
                Playlist.Current.IsPlayed = false;

            var moved = Playlist.MoveToPosition(position);

            if (moved)
            {
                var message = new ValueSet { { BackgroundMediaMessages.MoveToPosition, position } };
                BackgroundMediaPlayer.SendMessageToBackground(message);

                message = new ValueSet { { BackgroundMediaMessages.Play, string.Empty } };
                BackgroundMediaPlayer.SendMessageToBackground(message);

                Playlist.Current.IsPlayed = true;
            }
        }

        public async void PlayNextTrack()
        {
            if (Playlist.Current != null)
                Playlist.Current.IsPlayed = false;

            var moved = Playlist.MoveNext();

            if (moved)
            {
                var message = new ValueSet { { BackgroundMediaMessages.MoveNext, string.Empty } };
                BackgroundMediaPlayer.SendMessageToBackground(message);

                message = new ValueSet { { BackgroundMediaMessages.Play, string.Empty } };
                BackgroundMediaPlayer.SendMessageToBackground(message);

                Playlist.Current.IsPlayed = true;
            }
        }

        public async void PlayPreviousTrack()
        {
            if (Playlist.Current != null)
                Playlist.Current.IsPlayed = false;

            var moved = Playlist.MovePrevious();

            if (moved)
            {
                var message = new ValueSet { { BackgroundMediaMessages.MovePrevious, string.Empty } };
                BackgroundMediaPlayer.SendMessageToBackground(message);

                message = new ValueSet { { BackgroundMediaMessages.Play, string.Empty } };
                BackgroundMediaPlayer.SendMessageToBackground(message);

                Playlist.Current.IsPlayed = true;
            }
        }

        public async void PauseTrack()
        {
            if (_mediaPlayer.CurrentState == MediaPlayerState.Playing)
            {
                _mediaPlayer.Pause();
            }
        }

        public async void ResumeTrack()
        {
            if (_mediaPlayer.CurrentState == MediaPlayerState.Paused)
            {
                _mediaPlayer.Play();
            }
        }
        
        #endregion

        #region Private Methods

        private async void SaveCurrentPlaylist()
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("audio_player_state", CreationCollisionOption.ReplaceExisting);

            var serializer = new DataContractSerializer(typeof(List<string>), new DataContractSerializerSettings()
            {
                PreserveObjectReferences = false,
                MaxItemsInObjectGraph = 0x7FFF
            });

            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                serializer.WriteObject(fileStream, Playlist.Tracks.Select(item=>item.Path).ToList());
            }
        }

        private async void LoadCurrentPlaylist()
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("audio_player_state", CreationCollisionOption.OpenIfExists);

            var serializer = new DataContractSerializer(typeof(List<string>), new DataContractSerializerSettings()
            {
                PreserveObjectReferences = false,
                MaxItemsInObjectGraph = 0x7FFF
            });

            List<string> result;

            using (var fileStream = await file.OpenStreamForReadAsync())
            {
                try
                {
                    result = (List<string>) serializer.ReadObject(fileStream);

                    if (result != null)
                    {
                        foreach (var trackPath in result)
                        {
                            var track = MusicRepository.Instance.MusicCollection.GetTrackByPath(trackPath);
                            if (track != null)
                                Playlist.Tracks.Add(track);
                        }

                        SetPlaylist(Playlist.Tracks.ToList());
                    }
                    
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            var message = new ValueSet { { BackgroundMediaMessages.RequestCurrentPosition, null } };
            BackgroundMediaPlayer.SendMessageToBackground(message);
        }

        #endregion

        #region Music Player Event Handlers

        void MediaEnded(MediaPlayer sender, object args)
        {
            PlayEnded(this, null);
        }

        void MediaOpened(MediaPlayer sender, object args)
        {
            PlayStarted(this, null);
        }

        #endregion

        #region BackgroundMedia Events Handlers

        void MessageReceivedFromBackground(object sender, MediaPlayerDataReceivedEventArgs e)
        {
            var commands = e.Data.Keys;

            foreach (var key in commands)
            {
                switch (key)
                {
                    case BackgroundMediaMessages.Play:
                        continue;
                    case BackgroundMediaMessages.Pause:
                        continue;
                    case BackgroundMediaMessages.ResetPlaylist:
                        continue;
                    case BackgroundMediaMessages.ResetPosition:
                        continue;
                    case BackgroundMediaMessages.MoveToPosition:
                        var position = e.Data[BackgroundMediaMessages.MoveToPosition];
                        if (position != null && position is int)
                            Playlist.MoveToPosition((int)position);
                        continue;
                    case BackgroundMediaMessages.MoveNext:
                        continue;
                    case BackgroundMediaMessages.MovePrevious:
                        continue;
                    case BackgroundMediaMessages.AddTrackToPlaylist:
                        var path = e.Data[BackgroundMediaMessages.AddTrackToPlaylist];
                        if (path != null && path is string)
                        {
                            Playlist.Tracks.Add(new Track());
                        }
                        continue;
                }
            }
        }

        #endregion

        #region System Media Transport Controls Events Handlers

        void SmtcPropertyChanged(SystemMediaTransportControls sender, SystemMediaTransportControlsPropertyChangedEventArgs args)
        {
        }

        void SmtcButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Next:
                    Playlist.MoveNext();
                    break;
                case SystemMediaTransportControlsButton.Previous:
                    Playlist.MovePrevious();
                    break;
            }
        }

        #endregion

        void MusicCollectionLoaded(object sender, object e)
        {
            LoadCurrentPlaylist();
        }
    }
}
