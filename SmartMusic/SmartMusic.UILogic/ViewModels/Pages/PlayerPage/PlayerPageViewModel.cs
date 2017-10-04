using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Services;
using SmartMusic.UILogic.ViewModels.Pages.PlayerPage.Views;

namespace SmartMusic.UILogic.ViewModels.Pages.PlayerPage
{
    public class PlayerPageViewModel:ViewModel
    {
        private CoreWindow _coreWindow;
        private DispatcherTimer _timer;
        private INavigationService _navigationService;

        #region Presentation Properties

        private PlayerViewModel _playerViewModel;
        public PlayerViewModel PlayerViewModel
        {
            get { return _playerViewModel; }
            set { SetProperty(ref _playerViewModel, value); }
        }

        private PlaylistViewModel _playlistViewModel;
        public PlaylistViewModel PlaylistViewModel
        {
            get { return _playlistViewModel; }
            set { SetProperty(ref _playlistViewModel, value); }
        }

        public bool IsNoTrack
        {
            get { return AudioPlayer.Instance.Playlist.Current == null; }
        }

        public TimeSpan Position
        {
            get { return AudioPlayer.Instance.Position; }
        }

        public TimeSpan Duration
        {
            get
            {
                if (AudioPlayer.Instance.Playlist.Current != null)
                    return AudioPlayer.Instance.Playlist.Current.Duration;
                else
                    return TimeSpan.Zero;
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _tapToCentralCommand;
        public DelegateCommand TapToCentralCommand
        {
            get { return _tapToCentralCommand ?? new DelegateCommand(TapToCentralExecute, () => true); }
        }

        private DelegateCommand _tapToLeftCommand;
        public DelegateCommand TapToLeftCommand
        {
            get { return _tapToLeftCommand ?? new DelegateCommand(TapToLeftExecute, () => true); }
        }

        private DelegateCommand _tapToRightCommand;
        public DelegateCommand TapToRightCommand
        {
            get { return _tapToRightCommand ?? new DelegateCommand(TapToRightExecute, () => true); }
        }

        private DelegateCommand _openCollectionCommand;
        public DelegateCommand OpenCollectionCommand
        {
            get { return _openCollectionCommand ?? new DelegateCommand(OpenCollectionExecute, () => true); }
        }
        
        #endregion

        #region Commands Handlers

        private async void TapToCentralExecute()
        {
            switch (AudioPlayer.Instance.State)
            {
                case MediaPlayerState.Playing:
                    AudioPlayer.Instance.PauseTrack();
                    break;
                case MediaPlayerState.Paused:
                    AudioPlayer.Instance.ResumeTrack();
                    break;
            }
        }

        private async void TapToLeftExecute()
        {
            AudioPlayer.Instance.PlayPreviousTrack();
        }

        private async void TapToRightExecute()
        {
            AudioPlayer.Instance.PlayNextTrack();
        }

        private async void OpenCollectionExecute()
        {
            _navigationService.Navigate("MusicCollection", null);
        }


        #endregion

        public PlayerPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            PlayerViewModel = new PlayerViewModel();
            PlaylistViewModel = new PlaylistViewModel();
            
            _timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(0.2)};
            _timer.Tick += Tick;
            _timer.Start();

            _coreWindow = CoreWindow.GetForCurrentThread();

            AudioPlayer.Instance.PlayStarted += PlayStarted;
            AudioPlayer.Instance.PlayEnded += PlayEnded;

            AudioPlayer.Instance.Playlist.CurrentTrackChanged += CurrentTrackChangedEventHandler;
        }

        #region Event Handlers

        void PlayStarted(object sender, Track e)
        {
            if (_coreWindow != null)
            {
                _coreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => _timer.Start());
            }
        }

        void PlayEnded(object sender, Track e)
        {
            if (_coreWindow != null)
            {
                _coreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => _timer.Stop());
            }
        }

        async void Tick(object sender, object e)
        {
            if (_coreWindow != null)
            {
                _coreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => OnPropertyChanged("Position"));
            }
        }

        void CurrentTrackChangedEventHandler(object sender, Track e)
        {
            if (_coreWindow != null)
            {
                _coreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    OnPropertyChanged("IsNoTrack");
                    OnPropertyChanged("Position");
                    OnPropertyChanged("Duration");
                });
            }

        }

        #endregion
    }
}
