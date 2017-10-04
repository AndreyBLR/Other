using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMusic.Core.Enums
{
    public class BackgroundMediaMessages
    {
        public const string SetAutoPlay = "Autoplay"; 

        public const string ClearCurrentPlaylist = "ClearPlaylist";
        public const string AddTrackToCurrentPlaylist = "Playlist";
        public const string SetPositionInCurrentPlaylist = "Position";

        public const string PlayNext = "PlayNext";
        public const string PlayCurrent = "PlayCurrent";
        public const string PlayPrevious = "PlayPrevious";
        public const string Pause = "Stop";
        
    }
}
