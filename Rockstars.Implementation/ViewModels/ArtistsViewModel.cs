using Autofac;
using Rockstars.Implementation.Managers;
using Rockstars.Implementation.Models;
using Rockstars.Implementation.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Rockstars.ViewModels
{
    /// <summary>
    /// ArtistsViewModel
    /// </summary>
    public class ArtistsViewModel : ViewModelBase, IArtistsViewModel
    {
        private IMusicManager _musicManager;
        private IList<Artist> _artists = new List<Artist>();

        /// <summary>
        /// ArtistViewModel
        /// </summary>
        /// <param name="musicManager"></param>
        public ArtistsViewModel(IMusicManager musicManager)
        {
            _musicManager = musicManager;
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            var artists = _musicManager.GetArtists();
            Artists = artists;
        }


        /// <inheritdoc/>
        public IList<Artist> Artists
        {
            get
            {
                return _artists;
            }
            private set
            {
                _artists = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public Artist GetArtist(int artistId)
        {
            return Artists.Where(x => x.Id == artistId).FirstOrDefault();
        }

        /// <inheritdoc/>
        public IList<Song> GetSongs()
        {
            return _musicManager.GetSongs();
        }

        /// <inheritdoc/>
        public IList<Song> GetSongsFromArtist(int artistId)
        {
            return _musicManager.GetSongsFromArtist(artistId);
        }
    }
}
