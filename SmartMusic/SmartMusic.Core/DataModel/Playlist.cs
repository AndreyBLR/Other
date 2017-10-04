using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using WinRTXamlToolkit.IO.Serialization;

namespace SmartMusic.Core.DataModel
{
    public sealed class Playlist<T>
    {
        private int _position;

        public event EventHandler<T> CurrentTrackChanged;

        public T Current
        {
            get
            {
                if (Tracks != null && Tracks.Count > _position)
                    return Tracks[_position];
                return default(T);
            }
        }

        public string Name { get; set; }

        public int Position
        {
            get { return _position; }
        }

        public ObservableCollection<T> Tracks
        {
            get; set;
        }

        public Playlist()
        {
            Name = "default";
            Tracks = new ObservableCollection<T>();
        }

        public void ResetPlaylist()
        {
            ResetPosition();
            Tracks.Clear();
        }

        public void ResetPosition()
        {
            _position = 0;

            if (CurrentTrackChanged != null)
                CurrentTrackChanged(this, Current);
        }

        public bool MoveToPosition(int position)
        {
            if (position >= Tracks.Count || position < 0)
                return false;

            _position = position;

            if (CurrentTrackChanged != null)
                CurrentTrackChanged(this, Current);

            return true;
        }

        public bool MoveNext()
        {
            if (Tracks.Count == 0)
                return false;

            if (_position == (Tracks.Count - 1))
                ResetPosition();
            else
            {
                _position++;

                if (CurrentTrackChanged != null)
                    CurrentTrackChanged(this, Current);
            }
               
            return true;
        }

        public bool MovePrevious()
        {
            if (Tracks.Count == 0)
                return false;

            if (_position == 0)
                _position = Tracks.Count - 1;
            else
                _position--;

            if (CurrentTrackChanged != null)
                CurrentTrackChanged(this, Current);


            return true;
        }

    }
}
