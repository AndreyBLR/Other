using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using SmartMusic.Core.Helpers;

namespace SmartMusic.Core.DataModel.MusicCollection
{
    [DataContract(IsReference = true)]
    public class Artist
    {
        [DataMember]
        public char GrouperKey { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public ObservableCollection<Album> Albums { get; private set; }
        [DataMember]
        public ObservableCollection<Track> Tracks { get; private set; } 
        
        public void AddAlbum(Album album)
        {
            if(Albums == null)
                Albums = new ObservableCollection<Album>();

            if(Albums.All(item => item.Name != album.Name))
                Albums.Add(album);
        }

        public void AddTrack(Track track)
        {
            if (Tracks == null)
                Tracks = new ObservableCollection<Track>();

            if(!Tracks.Contains(track))
                Tracks.Add(track);
        }
    }
}
