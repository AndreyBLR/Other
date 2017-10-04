using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SmartMusic.Core.Repositories;
using SmartMusic.UILogic.ViewModels.Pages.Main.Views;

namespace SmartMusic.UILogic.ViewModels.Pages.Main
{
    public class MainPageViewModel:ViewModel
    {
        private INavigationService _navigationService;

        #region Presentation Properties

        private string _applicationName;
        public string ApplicationName
        {
            get { return _applicationName; }
            set { SetProperty(ref _applicationName, value); }
        }

        private FileSystemNavigationViewModel _fileSystemNavigationViewModel;
        public FileSystemNavigationViewModel FileSystemNavigationViewModel
        {
            get { return _fileSystemNavigationViewModel; }
            set { SetProperty(ref _fileSystemNavigationViewModel, value); }
        }

        private MusicCollectionViewModel _musicCollectionViewModel;
        public MusicCollectionViewModel MusicCollectionViewModel
        {
            get { return _musicCollectionViewModel; }
            set { SetProperty(ref _musicCollectionViewModel, value); }
        }

        #endregion

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ApplicationName = "Smart Music";

            FileSystemNavigationViewModel = new FileSystemNavigationViewModel(_navigationService);
            MusicCollectionViewModel = new MusicCollectionViewModel(_navigationService);
        }
    }
}
