using Windows.UI.Xaml.Data;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SmartMusic.Core.Repositories;

namespace SmartMusic.UILogic.ViewModels.Pages.MusicCollection.Views
{
    public class AlbumsViewModel:ViewModel
    {
        private INavigationService _navigationService;


        private CollectionViewSource _collection;
        public CollectionViewSource Albums
        {
            get
            {
                if (_collection == null)
                {
                    _collection = new CollectionViewSource();
                    _collection.Source = MusicRepository.Instance.MusicCollection.GroupedAlbums;
                    _collection.IsSourceGrouped = true;
                }
                return _collection;
            }
        } 

        public AlbumsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
