using Rockstars.Implementation.Models;
using System.Collections.Generic;

namespace Rockstars.Implementation.Managers
{
    /// <summary>
    /// IPlaylistManager
    /// Verantwoordelijk voor  het ophalen van playlists
    /// </summary>
    public interface IPlaylistManager
    {
        /// <summary>
        /// Ophalen van alle playlists
        /// </summary>
        /// <returns></returns>
        IList<Playlist> GetPlaylists();

        /// <summary>
        /// Toevoegen van een playlist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<Playlist> AddPlaylist(string name);

        /// <summary>
        /// Toevoegen van een song aan een playlist
        /// </summary>
        /// <param name="playlistName"></param>
        /// <param name="song"></param>
        /// <returns></returns>
        IList<Song> AddSongToPlaylist(string playlistName, Song song);
    }
}
