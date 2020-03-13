using Rockstars.Implementation.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rockstars.ViewModels
{
    /// <summary>
    /// IPlaylistViewModel
    /// 
    /// </summary>
    public interface IPlaylistViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initialiseert de playlists
        /// </summary>
        void InitializePlaylists();

        /// <summary>
        /// Initialiseert de geselecteerde playlist
        /// </summary>
        /// <param name="playlistName"></param>
        void InitializeSelectedPlaylist(string playlistName);

        /// <summary>
        /// Playlists
        /// </summary>
        IList<Playlist> Playlists { get; }

        /// <summary>
        /// Geselevcteerde playlist
        /// </summary>
        Playlist SelectedPlaylist { get; }

        /// <summary>
        /// Voegt een playlist toe
        /// </summary>
        /// <param name="name"></param>
        void AddPlaylist(string name);

        /// <summary>
        /// Voegt een song toe aan de geselecteerde playlist
        /// </summary>
        /// <param name="song"></param>
        void AddSongToSelectedPlaylist(Song song);
    }
}
