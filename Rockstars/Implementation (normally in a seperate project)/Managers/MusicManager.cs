using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Rockstars.Implementation.Models;
using Rockstars.Implementation.ViewModels;
using Rockstars.Implementation.Datasource;

namespace Rockstars.Implementation.Managers
{
    /// <summary>
    /// MusicManager
    /// </summary>
    public class MusicManager : ViewModelBase, IMusicManager
    {
        private IList<Song> _songData;
        private IList<Artist> _artistData;

        /// <inheritdoc/>
        public IList<Artist> GetArtists()
        {
            // Eenmalig ophalen van de data (singleton)
            if (_artistData == null)
            {
                _artistData = JsonConvert.DeserializeObject<List<Artist>>(Data.Artists);
            }

            return _artistData;
        }

        /// <inheritdoc/>
        public IList<Song> GetSongsFromArtist(int artistId)
        {
            var artistName = GetArtists().Where(x => x.Id == artistId).FirstOrDefault().Name;
            var artistSongs = GetSongs().Where(x => x.Artist == artistName).ToList();

            return artistSongs;
        }

        /// <inheritdoc/>
        public IList<Song> GetSongs()
        {
            // Eenmalig ophalen van de data (singleton)
            if (_songData == null)
            {
                _songData = JsonConvert.DeserializeObject<List<Song>>(Data.Songs);
            }

            return _songData;
        }
    }
}
