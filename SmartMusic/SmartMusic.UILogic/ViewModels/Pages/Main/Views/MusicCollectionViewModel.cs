using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;

namespace SmartMusic.UILogic.ViewModels.Pages.Main.Views
{
    public class MusicCollectionViewModel:ViewModel
    {
        private INavigationService _navigationService;

        #region Presentation Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public ObservableCollection<string> Categories { get; set; } 

        #endregion

        #region Commands

        private DelegateCommand<string> _tapToCategoryCommand;
        public DelegateCommand<string> TapToCategoryCommand
        {
            get
            {
                if (_tapToCategoryCommand == null)
                {
                    _tapToCategoryCommand = new DelegateCommand<string>(TapToCategoryExecute, (param) => true);
                }
                return _tapToCategoryCommand;
            }
        }

        #endregion

        #region Commands Handlers

        private void TapToCategoryExecute(string category)
        {
            _navigationService.Navigate("MusicCollection", category);
        }

        #endregion

        public MusicCollectionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Name = "Collection";

            Categories = new ObservableCollection<string>(){"Artists", "Albums", "Tracks", "Genres"};
        }
    }
}
