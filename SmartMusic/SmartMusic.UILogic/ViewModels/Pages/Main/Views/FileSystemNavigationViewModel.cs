using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SmartMusic.Core.Repositories;

namespace SmartMusic.UILogic.ViewModels.Pages.Main.Views
{
    public class FileSystemNavigationViewModel:ViewModel
    {
        private string _rootFolderFullPath = @"C:\Data\Users\Public\Music";
        private INavigationService _navigationService;

        

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private DelegateCommand<string> _tapToRootFolderCommand;

        public DelegateCommand<string> TapToRootFolderCommand
        {
            get
            {
                if (_tapToRootFolderCommand == null)
                {
                    _tapToRootFolderCommand = new DelegateCommand<string>(TapToRootFolderExecute, (param)=> true);
                }
                return _tapToRootFolderCommand;
            }
        }

        private void TapToRootFolderExecute(string obj)
        {
            _navigationService.Navigate("Folder", MusicRepository.Instance.FileSystem.GetFolder(_rootFolderFullPath));
        }

        public ObservableCollection<string> RootFolders { get; set; } 

        public FileSystemNavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Name = "File System";
            RootFolders = new ObservableCollection<string>() { KnownFolders.MusicLibrary.Name };
        }
    }
}
