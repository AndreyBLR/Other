using Windows.UI.Xaml.Data;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SmartMusic.Core.Repositories;

namespace SmartMusic.UILogic.ViewModels.Pages.MusicCollection.Views
{
    public class ArtistsViewModel:ViewModel
    {
        private INavigationService _navigationService;

        #region PresentationProperties

        private CollectionViewSource _collection;
        public CollectionViewSource Artists
        {
            get
            {
                if (_collection == null)
                {
                    _collection = new CollectionViewSource();
                    _collection.Source = MusicRepository.Instance.MusicCollection.GroupedArtists;
                    _collection.IsSourceGrouped = true;
                }
                return _collection;
            }
        } 

        #endregion

        #region Commands
        #endregion

        #region Commands Handlers
        #endregion

        public ArtistsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
