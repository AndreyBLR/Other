using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using SmartMusic.Core.Helpers;

namespace SmartMusic.Core.DataModel.MusicCollection
{
    [DataContract(IsReference = true)]
    public class Album
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public char GrouperKey { get; set; }
        [DataMember]
        public Artist Artist { get; set; }
        [DataMember]
        public ObservableCollection<Track> Tracks { get; set; } 

        public void AddTrack(Track track)
        {
            if (Tracks == null)
                Tracks = new ObservableCollection<Track>();

            if(!Tracks.Contains(track))
                Tracks.Add(track);
        }
    }
}
