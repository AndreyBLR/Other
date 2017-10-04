using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using SmartMusic.Core.DataModel.Interfaces;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Helpers;
using SmartMusic.Core.Services;

namespace SmartMusic.Core.Repositories
{
    public class MusicRepository
    {
        #region Fileds

        private static MusicRepository _instance;

        private bool _isStarted;
        private string _alphabet = "abcdefghijklmnopqrstuvwxyz#";

        #endregion

        public event EventHandler<object> MusicCollectionLoaded; 

        #region Properties

        public static MusicRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MusicRepository();
                }
                return _instance;
            }
        }

        public FileSystem FileSystem { get; private set; }
        public MusicCollection MusicCollection { get; private set; }

        #endregion

        private MusicRepository()
        {
            FileSystem = new FileSystem();
            MusicCollection = new MusicCollection();
        }

        #region Public Methods

        public async Task LoadMusicCollectionAsync()
        {
            if (!_isStarted)
            {
                _isStarted = true;

                var progressBar = StatusBar.GetForCurrentView().ProgressIndicator;
                progressBar.Text = "Loading...";
                progressBar.ShowAsync();

                //Loading deresialized data
                await DeserializeFolders();
                UpdatingMusicCollection(FileSystem.Root);

                //Try to update deserialized folders
                var storageFolder = await StorageFolder.GetFolderFromPathAsync(@"C:\Data\Users\Public\Music");
                await UpdatingFolder(storageFolder);

                await SerializeFolders();

                _isStarted = false;
                progressBar.HideAsync();

                if (MusicCollectionLoaded != null)
                    MusicCollectionLoaded(this, null);
            }
        }
        
        #endregion

        #region Private Methods

        private async Task SerializeFolders()
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("music_folders", CreationCollisionOption.ReplaceExisting);

            var serializer = new DataContractSerializer(typeof(Folder), new DataContractSerializerSettings()
            {
                PreserveObjectReferences = true,
                MaxItemsInObjectGraph = 0x7FFF
            });

            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                serializer.WriteObject(fileStream, FileSystem.Root);
            }
        }

        private async Task DeserializeFolders()
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("music_folders", CreationCollisionOption.OpenIfExists);

            var serializer = new DataContractSerializer(typeof(Folder), new DataContractSerializerSettings()
            {
                PreserveObjectReferences = true,
                MaxItemsInObjectGraph = 0x7FFF
            });
           
            Folder result;
            using (var fileStream = await file.OpenStreamForReadAsync())
            {
                try
                {
                    result = (Folder)serializer.ReadObject(fileStream);

                    if (result != null)
                    {
                        FileSystem.AddRoot(result);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        private void UpdatingMusicCollection(Folder folder)
        {
            ReadTracksFromFolder(folder);

            var folders = folder.Children.Where(item => item is Folder).Cast<Folder>();

            foreach (var item in folders)
            {
                UpdatingMusicCollection(item);
            }
        }

        private async Task UpdatingFolder(StorageFolder storageFolder)
        {
            var storageFolders = await storageFolder.GetFoldersAsync();

            var storageFolderPath = storageFolder.Path;

            var folder = FileSystem.GetFolder(storageFolderPath);

            if (folder != null)
            {
                var properties = await storageFolder.GetBasicPropertiesAsync();

                if (folder.DateModified != properties.DateModified)
                {
                    folder.RemoveTracks();
                    folder.DateModified = properties.DateModified;

                    await ReadTracksFromFolder(storageFolder);
                }
            }
            else
            {
                await ReadTracksFromFolder(storageFolder);
            }

            foreach (var item in storageFolders)
            {
                await UpdatingFolder(item);
            }
        }

        private void ReadTracksFromFolder(Folder folder)
        {
            if (folder.Children != null)
            {
                foreach (var child in folder.Children)
                {
                    var track = child as Track;
                    if (track != null)
                    {
                        MusicCollection.AddTrack(track);
                        MusicCollection.AddAlbum(track.Album);
                        MusicCollection.AddArtist(track.Artist);
                    }
                }
            }
        }

        private async Task<Folder> ReadTracksFromFolder(StorageFolder storageFolder)
        {
            var folder = FileSystem.GetFolder(storageFolder.Path) ?? await FileSystem.AddFolder(storageFolder);
            if (folder == null)
                return null;

            var storageFiles = await storageFolder.GetFilesAsync();

            foreach (var storageFile in storageFiles.Where(storageFile => storageFile.ContentType.StartsWith("audio")))
            {
                var newTrack = await MusicCollection.CreateNewTrack(storageFile);
                folder.AddItem(newTrack);
            }

            return folder;
        }
       
        #endregion
    }
}
