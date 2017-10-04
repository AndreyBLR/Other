using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SmartMusic.Core.DataModel;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Repositories;
using SmartMusic.Core.Services;
using SmartMusic.UILogic.ViewModels.Pages.MusicCollection.Views;

namespace SmartMusic.UILogic.ViewModels.Pages.MusicCollection
{
    public class MusicCollectionPageViewModel:ViewModel
    {
        #region Fileds

        private INavigationService _navigationService;

        #endregion

        #region Presentation Properties

        public FolderViewModel FolderViewModel { get; set; }
        public ArtistsViewModel ArtistsViewModel { get; set; }
        public AlbumsViewModel AlbumsViewModel { get; set; }
        public TracksViewModel TracksViewModel { get; set; }

        #endregion

        #region Commands

        private DelegateCommand<Artist> _tapToArtistCommand; 
        public DelegateCommand<Artist> TapToArtistCommand 
        { 
            get { return _tapToArtistCommand ?? new DelegateCommand<Artist>(TapToArtistExecute, (param) => true); }
        }

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

        private void TapToArtistExecute(Artist obj)
        {
            _navigationService.Navigate("Artist", obj);
        }

        private void TapToAlbumExecute(Album obj)
        {
            _navigationService.Navigate("Album", obj);
        }

        private async void TapToTrackExecute(Track obj)
        {
            var source = TracksViewModel.Tracks.View.Cast<Track>().ToList();
            var currentPosition = source.FindIndex(item=> item == obj);

            AudioPlayer.Instance.SetPlaylist(source);
            AudioPlayer.Instance.PlayTrack(currentPosition);
        }
        
        #endregion

        public MusicCollectionPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            FolderViewModel = new FolderViewModel(_navigationService);
            ArtistsViewModel = new ArtistsViewModel(_navigationService);
            AlbumsViewModel = new AlbumsViewModel(_navigationService);
            TracksViewModel = new TracksViewModel(_navigationService);
        }
    }
}
