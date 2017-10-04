using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using SmartMusic.Core.Annotations;
using SmartMusic.Core.DataModel.Interfaces;

namespace SmartMusic.Core.DataModel.MusicCollection
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Track))]
    [KnownType(typeof(Artist))]
    [KnownType(typeof(Album))]
    public class Folder:IFileSystemItem
    {
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public ulong Size { get; set; }
        [DataMember]
        public DateTimeOffset DateModified { get; set; }
        [DataMember]
        public ObservableCollection<IFileSystemItem> Children { get; set; }

        public Folder()
        {
            Children = new ObservableCollection<IFileSystemItem>();
        }

        #region Public Methods

        public void AddItem(IFileSystemItem track)
        {
            Children.Add(track);
        }

        public IFileSystemItem GetItem(string name)
        {
            if (Children.Count == 0)
                return null;

            return Children.FirstOrDefault(item => item.Name == name);
        }

        public Track GetTrack(string name)
        {
            if (Children.Count == 0)
                return null;

            var item = GetItem(name);

            if (item != null)
                return (Track) item;
            else
                return null;
        }

        public Folder GetFolder(string name)
        {
            if (Children.Count == 0)
                return null;

            var item = GetItem(name);

            if (item != null)
                return (Folder)item;
            else
                return null;
        }

        public void RemoveAll()
        {
            Children.Clear();
        }

        public void RemoveTracks()
        {
            for (var i = 0; i < Children.Count; i++)
            {
                if (Children[i] is Track)
                    Children.Remove(Children[i]);
            }
        }

        public void RemoveFolders()
        {
            for (var i = 0; i < Children.Count; i++)
            {
                if (Children[i] is Folder)
                    Children.Remove(Children[i]);
            }
        }

        #endregion
    }
}
