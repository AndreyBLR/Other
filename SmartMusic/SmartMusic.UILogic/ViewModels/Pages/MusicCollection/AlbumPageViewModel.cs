using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using SmartMusic.Core.DataModel;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Repositories;
using SmartMusic.Core.Services;

namespace SmartMusic.UILogic.ViewModels.Pages.MusicCollection
{
    public class AlbumPageViewModel:ViewModel
    {
        private Album _album;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public ObservableCollection<Track> Tracks
        {
            get { return _album.Tracks; }
        }

        private DelegateCommand<Track> _tapToTrackCommand;
        public DelegateCommand<Track> TapToTrackCommand
        {
            get { return _tapToTrackCommand ?? new DelegateCommand<Track>(TapToTrackExecute, (param) => true); }
        }

        private async void TapToTrackExecute(Track obj)
        {
            var position = Tracks.ToList().FindIndex(item => item == obj);

            AudioPlayer.Instance.SetPlaylist(Tracks.ToList());
            AudioPlayer.Instance.PlayTrack(position);

        }

        public override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            var album = navigationParameter as Album;

            if (album == null)
                return;

            _album = album;

            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }
    }
}
