using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using SmartMusic.Core.Annotations;
using SmartMusic.Core.DataModel.Interfaces;
using SmartMusic.Core.Helpers;

namespace SmartMusic.Core.DataModel.MusicCollection
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Album))]
    [KnownType(typeof(Artist))]
    [KnownType(typeof(Folder))]
    public class Track : IFileSystemItem, INotifyPropertyChanged
    {
        #region IFileSystemItem

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ulong Size { get; set; }

        [DataMember]
        public DateTimeOffset DateModified { get; set; }

        #endregion

        [DataMember]
        public char GrouperKey { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public Album Album { get; set; }

        [DataMember]
        public Artist Artist { get; set; }

        [DataMember]
        public Folder Folder { get; set; }

        [DataMember]
        public TimeSpan Duration { get; set; }

        [DataMember]
        public uint Bitrate { get; set; }

        private bool _isPlayed;
        public bool IsPlayed
        {
            get { return _isPlayed; }
            set
            {
                _isPlayed = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
