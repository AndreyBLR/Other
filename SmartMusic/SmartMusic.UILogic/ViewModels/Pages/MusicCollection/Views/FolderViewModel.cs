using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Repositories;
using SmartMusic.Core.Services;

namespace SmartMusic.UILogic.ViewModels.Pages.MusicCollection.Views
{
    public class FolderViewModel:ViewModel
    {
        #region Fields
        
        private INavigationService _navigationService;

        #endregion

        #region Presentation Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _name, value);
                }
            }
        }

        private Folder _currentFolder;
        public Folder CurrentFolder
        {
            get { return _currentFolder; }
            set { SetProperty(ref _currentFolder, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand<Track> _tapToTrackCommand;

        public DelegateCommand<Track> TapToTrackCommand
        {
            get { return _tapToTrackCommand ?? (_tapToTrackCommand = new DelegateCommand<Track>(TapToTrackExecute, (param) => true)); }
        }

        private DelegateCommand<Folder> _tapToFolderCommand;

        public DelegateCommand<Folder> TapToFolderCommand
        {
            get { return _tapToFolderCommand ?? (_tapToFolderCommand = new DelegateCommand<Folder>(TapToFolderExecute, (param) => true)); }
        }

        #endregion

        #region Command Handlers

        private void TapToTrackExecute(Track track)
        {
            var source = CurrentFolder.Children.Where(item => item is Track).Cast<Track>().ToList();
            var currentPosition = source.FindIndex(item => item == track);

            AudioPlayer.Instance.SetPlaylist(source);
            AudioPlayer.Instance.PlayTrack(currentPosition);

            _navigationService.Navigate("Player", track);
        }

        private void TapToFolderExecute(Folder param)
        {
            Name = param.Name;
            CurrentFolder = param;
        }

        #endregion

        public FolderViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Name = MusicRepository.Instance.FileSystem.Root.Name;
            CurrentFolder = MusicRepository.Instance.FileSystem.Root;
        }
        
        public override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            var folder = navigationParameter as Folder;
            if (folder != null)
            {
                Name = folder.Name;
                CurrentFolder = folder;
            }
        }
    }
}
