using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SmartMusic.Core.DataModel.MusicCollection;

namespace SmartMusic.Core.Services
{
    public class FileSystem
    {
        private string _basePath = @"C:\Data\Users\Public";

        public Folder Root { get; private set; }

        #region Public Methods

        public void AddRoot(Folder root)
        {
            Root = root;
        }

        public Folder GetFolder(string path)
        {
            path = ConvertToRelativePath(path);

            var directories = SplitByDirectories(path);

            if (!directories.Any())
                return null;

            if (Root == null || Root.Name != directories[0])
                return null;

            var current = Root;

            for (int i = 1; i < directories.Count(); i++)
            {
                current = current.GetFolder(directories[i]);

                if (current == null)
                    return null;
            }

            return current;
        }

        public async Task<Folder> AddFolder(StorageFolder storageFolder)
        {
            var basicProperties = await storageFolder.GetBasicPropertiesAsync();

            string processedPath = _basePath;
            string path = ConvertToRelativePath(storageFolder.Path);

            var directories = SplitByDirectories(path);

            if (!directories.Any())
                return null;

            if (Root == null && directories.Count() == 1)
            {
                var newFolder = new Folder()
                {
                    Name = storageFolder.Name,
                    Path = storageFolder.Path,
                    DateModified = basicProperties.DateModified
                };

                AddRoot(newFolder);

                return newFolder;
            }
            else
            {
                var current = Root;
                processedPath = Path.Combine(processedPath, directories[0]);

                for (int i = 1; i < directories.Count(); i++)
                {
                    var temp = current.GetFolder(directories[i]);
                    processedPath = Path.Combine(processedPath, directories[i]);

                    if (temp != null)
                    {
                        current = temp;
                    }
                    else
                    {
                        var realFolder = await StorageFolder.GetFolderFromPathAsync(processedPath);

                        var newFolder = new Folder()
                        {
                            Name = realFolder.Name,
                            Path = realFolder.Path,
                            DateModified = basicProperties.DateModified
                        };

                        current.AddItem(newFolder);

                        current = newFolder;
                    }
                }

                return current;
            }
            
        }

        #endregion

        #region Private Methods

        private string ConvertToRelativePath(string path)
        {
            if (String.IsNullOrEmpty(path))
                return null;

            return path.Remove(0, _basePath.Count());
        }

        private string ConvertToAbsolutePath(string path)
        {
            if (String.IsNullOrEmpty(path))
                return null;

            return Path.Combine(_basePath, path);
        }

        private string[] SplitByDirectories(string path)
        {
            return path.Split('\\').Where(item=>!string.IsNullOrEmpty(item)).Select(item=>item).ToArray();
        }

        #endregion
    }
}
