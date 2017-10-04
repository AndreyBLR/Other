using Windows.Media.Playback;
using Windows.UI.Core;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Services;

namespace SmartMusic.UILogic.ViewModels.Pages.PlayerPage.Views
{
    public class PlayerViewModel:ViewModel
    {
        private CoreWindow _coreWindow;

        #region Presentation Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string ImageSource
        {
            get { return "pack://application:,,,/Resources/Images/NoCover.png "; }
        }

        private Track _nowPlaying;
        public Track NowPlaying
        {
            get { return _nowPlaying; }
            set { SetProperty(ref _nowPlaying, value); }
        }

        #endregion
        
        public PlayerViewModel()
        {
            _coreWindow = CoreWindow.GetForCurrentThread();
            NowPlaying = AudioPlayer.Instance.Playlist.Current;
            AudioPlayer.Instance.Playlist.CurrentTrackChanged += CurrentTrackChangedEventHandler;
        }

        void CurrentTrackChangedEventHandler(object sender, Track e)
        {
            if (_coreWindow != null)
            {
                _coreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => NowPlaying = e);
            }
        }
    }
}
