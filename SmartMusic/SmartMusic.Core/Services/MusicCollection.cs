using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Core;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Helpers;

namespace SmartMusic.Core.Services
{
    public class MusicCollection
    {
        #region Fields

        private string _alphabet = "abcdefghijklmnopqrstuvwxyz#";

        public IDictionary<char, GroupObservableCollection<Track>> _tracksDictionary { get; set; }
        public IDictionary<char, GroupObservableCollection<Album>> _albumsDictionary { get; set; }
        public IDictionary<char, GroupObservableCollection<Artist>> _artistsDictionary { get; set; }

        #endregion

        #region Events

        public event EventHandler<Track> NewTrackAdded;
        public event EventHandler<Artist> NewArtistAdded;
        public event EventHandler<Album> NewAlbumAdded; 

        #endregion

        public IList<Track> Tracks { get; set; }
        public IList<Album> Albums { get; set; }
        public IList<Artist> Artists { get; set; }

        public ObservableCollection<GroupObservableCollection<Track>> GroupedTracks { get; set; }
        public ObservableCollection<GroupObservableCollection<Album>> GroupedAlbums { get; set; }
        public ObservableCollection<GroupObservableCollection<Artist>> GroupedArtists { get; set; }

        public MusicCollection()
        {
            Initialize();
        }

        #region Public Methods

        public async Task<Track> CreateNewTrack(StorageFile storageFile)
        {
            var musicProperties = await storageFile.Properties.GetMusicPropertiesAsync();
            var basicProperties = await storageFile.GetBasicPropertiesAsync();


            var title = !string.IsNullOrEmpty(musicProperties.Title) ? musicProperties.Title : storageFile.Name;

            char titleFirstLetter = title.TrimStart().ToLower()[0];
            if (!_alphabet.Contains(titleFirstLetter))
                titleFirstLetter = '#';

            var newTrack = new Track
            {
                GrouperKey = titleFirstLetter, 
                Name = storageFile.Name, 
                Title = title,
                Path = storageFile.Path,
                Duration = musicProperties.Duration,
                Bitrate = musicProperties.Bitrate,
                Size = basicProperties.Size
            };

            var artist = GetArtist(musicProperties.Artist);
            if (artist == null)
            {
                artist = new Artist()
                {
                    Name = !string.IsNullOrEmpty(musicProperties.Artist) ? musicProperties.Artist : "Unknown"
                };

                char artistFirstLetter = artist.Name.TrimStart().ToLower()[0];
                if (!_alphabet.Contains(artistFirstLetter))
                    artistFirstLetter = '#';

                artist.GrouperKey = artistFirstLetter;

                AddArtist(artist);
            }

            var album = GetAlbum(musicProperties.Album);
            if (album == null)
            {
                album = new Album
                {
                    Name = !string.IsNullOrEmpty(musicProperties.Album) ? musicProperties.Album : "Unknown",
                    Artist = artist
                };

                char albumFirstLetter = album.Name.TrimStart().ToLower()[0];
                if (!_alphabet.Contains(albumFirstLetter))
                    albumFirstLetter = '#';

                album.GrouperKey = albumFirstLetter;

                AddAlbum(album);
            }

            newTrack.Album = album;
            newTrack.Artist = artist;

            artist.AddAlbum(album);
            artist.AddTrack(newTrack);

            album.AddTrack(newTrack);

            AddTrack(newTrack);

            return newTrack;
        }

        public void AddTrack(Track track)
        {
            if(Tracks == null)
                Tracks = new List<Track>();

            if (track != null && !Tracks.Contains(track))
            {
                _tracksDictionary[track.GrouperKey].Add(track);
                Tracks.Add(track);

                if (NewTrackAdded != null)
                    NewTrackAdded(this, track);
            }
        }
        
        public void AddAlbum(Album album)
        {
            if(Albums == null)
                Albums = new List<Album>();

            if (album != null && Albums.All(item => item.Name != album.Name))
            {
                _albumsDictionary[album.GrouperKey].Add(album);
                Albums.Add(album);
                if (NewAlbumAdded != null)
                    NewAlbumAdded(this, album);
            }
        }

        public void AddArtist(Artist artist)
        {
            if(Artists == null)
                Artists = new List<Artist>();

            if (artist != null && Artists.All(item => item.Name != artist.Name))
            {
                _artistsDictionary[artist.GrouperKey].Add(artist);
                Artists.Add(artist);
                if (NewArtistAdded != null)
                    NewArtistAdded(this, artist);
            }
        }

        public Track GetTrackByPath(string path)
        {
            return Tracks.FirstOrDefault(item => item.Path == path);
        }

        public Artist GetArtist(string name)
        {
            if (Artists == null)
                return null;
            return (from artist in Artists where artist.Name == name select artist).FirstOrDefault();
        }

        public Album GetAlbum(string name)
        {
            if (Albums == null)
                return null;
            return (from album in Albums where album.Name == name select album).FirstOrDefault();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            _tracksDictionary = new Dictionary<char, GroupObservableCollection<Track>>();
            _albumsDictionary = new Dictionary<char, GroupObservableCollection<Album>>();
            _artistsDictionary = new Dictionary<char, GroupObservableCollection<Artist>>();

            GroupedTracks = new ObservableCollection<GroupObservableCollection<Track>>();
            GroupedAlbums = new ObservableCollection<GroupObservableCollection<Album>>();
            GroupedArtists = new ObservableCollection<GroupObservableCollection<Artist>>();

            foreach (var letter in _alphabet)
            {
                _tracksDictionary.Add(letter, new GroupObservableCollection<Track>(letter));
                GroupedTracks.Add(_tracksDictionary[letter]);

                _albumsDictionary.Add(letter, new GroupObservableCollection<Album>(letter));
                GroupedAlbums.Add(_albumsDictionary[letter]);

                _artistsDictionary.Add(letter, new GroupObservableCollection<Artist>(letter));
                GroupedArtists.Add(_artistsDictionary[letter]);
            }

            Tracks = new List<Track>();
            Albums = new List<Album>();
            Artists = new List<Artist>();
        }

        #endregion
    }
}
