using System.Linq;
using Windows.UI.Xaml.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SmartMusic.Core.DataModel;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Repositories;
using SmartMusic.Core.Services;

namespace SmartMusic.UILogic.ViewModels.Pages.MusicCollection.Views
{
    public class TracksViewModel:ViewModel
    {
        private INavigationService _navigationService;

        #region PresentationProperties

        private CollectionViewSource _collection;
        public CollectionViewSource Tracks
        {
            get
            {
                if (_collection == null)
                {
                    _collection = new CollectionViewSource();
                    _collection.Source = MusicRepository.Instance.MusicCollection.GroupedTracks;
                    _collection.IsSourceGrouped = true;
                }
                return _collection;
            }
        } 

        #endregion

        #region Commands

        private DelegateCommand<Track> _tapToTrackCommand;
        public DelegateCommand<Track> TapToTrackCommand
        {
            get { return _tapToTrackCommand ?? new DelegateCommand<Track>(TapToTrackExecute, (param) => true); }
        }

        #endregion

        #region Commands Handlers

        private void TapToTrackExecute(Track param)
        {
            var source = Tracks.View.Cast<Track>().ToList();
            var currentPosition = source.FindIndex(item=> item == param);

            AudioPlayer.Instance.SetPlaylist(source);
            AudioPlayer.Instance.PlayTrack(currentPosition);
        }

        #endregion

        public TracksViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

    }
}
