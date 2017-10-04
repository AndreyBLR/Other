using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.MediaProperties;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using SmartMusic.Core.DataModel;
using SmartMusic.Core.Helpers;

namespace SmartMusic.BackgroundTasks
{
    public sealed class SmartMusicAudioTask:IBackgroundTask
    {
        #region Fields

        private MediaPlayer _mediaPlayer;
        private BackgroundTaskDeferral _deferral;
        private SystemMediaTransportControls _smtc;
       

        private Playlist<string> _playlist;
        
        #endregion

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _playlist = new Playlist<string>();

            _mediaPlayer = BackgroundMediaPlayer.Current;


            _mediaPlayer.MediaPlayerRateChanged += MediaPlayerRateChanged;
            _mediaPlayer.MediaEnded +=MediaEnded;

            _smtc = SystemMediaTransportControls.GetForCurrentView();
            _smtc.ButtonPressed += SmtcButtonPressed;
            _smtc.PropertyChanged += SmtcPropertyChanged;

            _smtc.IsEnabled = true;
            _smtc.IsNextEnabled = true;
            _smtc.IsPauseEnabled = true;
            _smtc.IsPlayEnabled = true;
            _smtc.IsPreviousEnabled = true;
            _smtc.IsFastForwardEnabled = true;
            _smtc.IsRewindEnabled = true;

            taskInstance.Task.Completed += TaskCompleted;
            taskInstance.Canceled += TaskInstanceCanceled;

            BackgroundMediaPlayer.MessageReceivedFromForeground += MessageReceivedFromForeground;
            _deferral = taskInstance.GetDeferral();
        }

        void MessageReceivedFromForeground(object sender, MediaPlayerDataReceivedEventArgs e)
        {
            var commands = e.Data.Keys;

            foreach (var key in commands)
            {
                switch (key)
                {
                    case BackgroundMediaMessages.Play:
                        Play();
                        continue;
                    case BackgroundMediaMessages.Pause:
                        _mediaPlayer.Pause();
                        continue;
                    case BackgroundMediaMessages.ResetPlaylist:
                        _playlist.ResetPlaylist();
                        continue;
                    case BackgroundMediaMessages.ResetPosition:
                        _playlist.ResetPosition();
                        continue;
                    case BackgroundMediaMessages.MoveToPosition:
                        var position = e.Data[BackgroundMediaMessages.MoveToPosition];
                        if(position != null && position is int)
                            _playlist.MoveToPosition((int) position);
                        continue;
                    case BackgroundMediaMessages.MoveNext:
                        _playlist.MoveNext();
                        continue;
                    case BackgroundMediaMessages.MovePrevious:
                        _playlist.MovePrevious();
                        continue;
                    case BackgroundMediaMessages.AddTrackToPlaylist:
                        var track = e.Data[BackgroundMediaMessages.AddTrackToPlaylist];
                        if(track != null && track is string)
                            _playlist.Tracks.Add((string)track);
                        continue;
                    case BackgroundMediaMessages.RequestCurrentPosition:
                        SendCurrentPositionToForeground();
                        continue;
                }
            }
        }

        private void SendCurrentPositionToForeground()
        {
            var message = new ValueSet {{BackgroundMediaMessages.MoveToPosition, _playlist.Position}};
            BackgroundMediaPlayer.SendMessageToForeground(message);
        }

        private async void Play()
        {
            var file = await StorageFile.GetFileFromPathAsync(_playlist.Current);
            _mediaPlayer.SetFileSource(file);
            _mediaPlayer.Play();
            UpdateSystemMediaTransportControls(file);
        }

        private async void UpdateSystemMediaTransportControls(StorageFile file)
        {
            var musicProperties = await file.Properties.GetMusicPropertiesAsync();

            var thumbnail = await file.GetThumbnailAsync(ThumbnailMode.MusicView);
            _smtc.DisplayUpdater.Type = MediaPlaybackType.Music;
            _smtc.DisplayUpdater.MusicProperties.Title = musicProperties.Title;
            _smtc.DisplayUpdater.MusicProperties.Artist = musicProperties.Artist;
            _smtc.DisplayUpdater.Thumbnail = RandomAccessStreamReference.CreateFromStream(thumbnail);
            var a = new MediaStreamSource(new AudioStreamDescriptor(new AudioEncodingProperties()));
            
            _smtc.DisplayUpdater.Update();
        }

        #region MediaPlayer Events Handlers

        void MediaEnded(MediaPlayer sender, object args)
        {
            if (_playlist.MoveNext())
            {
                Play();
            }
        }

        void MediaPlayerRateChanged(MediaPlayer sender, MediaPlayerRateChangedEventArgs args)
        {
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
                case SystemMediaTransportControlsButton.Play:
                    _mediaPlayer.Play();
                    break;
                case SystemMediaTransportControlsButton.Pause:
                    _mediaPlayer.Pause();
                    break;
                case SystemMediaTransportControlsButton.Next:
                    if(_playlist.MoveNext())
                        Play();
                    break;
                case SystemMediaTransportControlsButton.Previous:
                    if(_playlist.MovePrevious());
                        Play();
                    break;
                case SystemMediaTransportControlsButton.Rewind:
                    _mediaPlayer.PlaybackRate = 150.0;
                    break;
                case SystemMediaTransportControlsButton.FastForward:
                    _mediaPlayer.PlaybackRate = 500.0;
                    break;
                case SystemMediaTransportControlsButton.ChannelUp:
                    break;
                case SystemMediaTransportControlsButton.ChannelDown:
                    break;
            }
        }

        #endregion

        #region Task Events Handlers

        void TaskCompleted(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            _deferral.Complete();
        }

        void TaskInstanceCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            _smtc.ButtonPressed -= SmtcButtonPressed;
            _smtc.PropertyChanged -= SmtcPropertyChanged;
            BackgroundMediaPlayer.MessageReceivedFromForeground -= MessageReceivedFromForeground;
            BackgroundMediaPlayer.Shutdown();
        }

        #endregion
        
    }
}
