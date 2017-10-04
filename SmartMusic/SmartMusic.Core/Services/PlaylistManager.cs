using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SmartMusic.Core.DataModel;
using SmartMusic.Core.DataModel.MusicCollection;

namespace SmartMusic.Core.Services
{
    public static class PlaylistManager
    {
        public static Playlist<Track> CurrentPlaylist
        {
            get { return AudioPlayer.Instance.Playlist; }
        }

        public async static void SaveCurrentPlaylist()
        {
            var serializer = new DataContractSerializer(typeof(Playlist<Track>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(CurrentPlaylist.Name, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, CurrentPlaylist);
            }
        }

        public async static void LoadCurrentPlaylist()
        {
            var serializer = new DataContractSerializer(typeof(Playlist<Track>));
            
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("default"))
            {
                var result = serializer.ReadObject(stream);
                var b = result;
            }
        }

    }
}
