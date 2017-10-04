using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SmartMusic.Core.DataModel;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Repositories;
using SmartMusic.Core.Services;

namespace SmartMusic.UILogic.ViewModels.Pages.MusicCollection
{
    public class ArtistPageViewModel:ViewModel
    {
        #region Fields

        private Artist _artist;
        private INavigationService _navigationService;

        #endregion

        #region Presentation Properties

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public ObservableCollection<Album> Albums
        {
            get { return _artist.Albums; }
        }

        public ObservableCollection<Track> Tracks
        {
            get { return _artist.Tracks; }
               
        }

        #endregion

        #region Commands
        
        private DelegateCommand<Album> _tapToAlbumCommand;
        public DelegateCommand<Album> TapToAlbumCommand
        {

            get { return _tapToAlbumCommand ?? new DelegateCommand<Album>(TapToAlbumExecute, (param) => true); }
        }

        private DelegateCommand<Track> _tapToTrackCommand;
        public DelegateCommand<Track> TapToTrackCommand
        {
            get { return _tapToTrackCommand ?? new DelegateCommand<Track>(TapToTrackExecute, (param) => true); }
        }

        #endregion

        #region Commands Handlers

        private void TapToAlbumExecute(Album obj)
        {
            _navigationService.Navigate("Album", obj);
        }

        private async void TapToTrackExecute(Track obj)
        {
            var currentPosition = Tracks.ToList().FindIndex(item => item == obj);

            AudioPlayer.Instance.SetPlaylist(Tracks.ToList());
            AudioPlayer.Instance.PlayTrack(currentPosition);
        }

        #endregion

        public ArtistPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            var artist = navigationParameter as Artist;

            if(artist == null)
                return;

            _artist = artist;
            _name = artist.Name;
            

            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }
    }
}
