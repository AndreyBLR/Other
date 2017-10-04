using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Services;

namespace SmartMusic.UILogic.ViewModels.Pages.PlayerPage.Views
{
    public class PlaylistViewModel:ViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public ObservableCollection<Track> Tracks
        {
            get { return AudioPlayer.Instance.Playlist.Tracks; }
        }

        private DelegateCommand<Track> _tapToTrackCommand;
        public DelegateCommand<Track> TapToTrackCommand
        {
            get { return _tapToTrackCommand ?? new DelegateCommand<Track>(TapToTrackExecute, (param) => true); }
        }

        private void TapToTrackExecute(Track obj)
        {
            var position = Tracks.ToList().FindIndex(item => item == obj);
            AudioPlayer.Instance.PlayTrack(position);
        }


        public PlaylistViewModel()
        {
            Name = "Now playing...";
        }
    }
}
