using System.Collections.Generic;
using System.Linq;
using Rockstars.Implementation.Managers;
using Rockstars.Implementation.Models;
using Rockstars.ViewModels;

namespace Rockstars.Implementation.ViewModels
{
    /// <summary>
    /// PlaylistVieModel
    /// </summary>
    public class PlaylistViewModel : ViewModelBase, IPlaylistViewModel
    {
        private readonly IPlaylistManager _playlistManager;
        private IList<Playlist> _playlists = new List<Playlist>();
        private Playlist _selectedPlaylist;

        /// <summary>
        /// PlaylistViewModel
        /// </summary>
        /// <param name="playlistManager"></param>
        public PlaylistViewModel(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        /// <inheritdoc/>
        public IList<Playlist> Playlists
        {
            get
            {
                return _playlists;
            }
            private set
            {
                _playlists = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public Playlist SelectedPlaylist
        {
            get
            {
                return _selectedPlaylist;
            }
            private set
            {
                _selectedPlaylist = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public void InitializePlaylists()
        {
            Playlists = _playlistManager.GetPlaylists();
        }

        /// <inheritdoc/>
        public void InitializeSelectedPlaylist(string playlistName)
        {
             SelectedPlaylist =  Playlists.Where(x => x.Name == playlistName).FirstOrDefault();
        }

        /// <inheritdoc/>
        public void AddPlaylist(string name)
        {
            Playlists = _playlistManager.AddPlaylist(name);
        }

        /// <inheritdoc/>
        public void AddSongToSelectedPlaylist(Song song)
        {
            SelectedPlaylist.Songs = _playlistManager.AddSongToPlaylist(SelectedPlaylist.Name, song);
            OnPropertyChanged(() => SelectedPlaylist);
        }
    }
}