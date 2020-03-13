using Rockstars.Implementation.Models;
using System.Collections.Generic;
using System.Linq;

namespace Rockstars.Implementation.Managers
{
    /// <summary>
    /// PlaylistManager
    /// </summary>
    public class PlaylistManager : IPlaylistManager
    {
        private readonly IList<Playlist> _playlists = new List<Playlist>();

        /// <inheritdoc/>
        public IList<Playlist> AddPlaylist(string name)
        {
            var newPLaylist = new Playlist
            {
                Name = name
            };

            _playlists.Add(newPLaylist);
            return _playlists;
                     
        }

        /// <inheritdoc/>
        public IList<Playlist> GetPlaylists()
        {
            return _playlists;
        }

        /// <inheritdoc/>
        public IList<Song> AddSongToPlaylist(string playlistName, Song song)
        {
            // Ophalen van playlist die moet worden geupdate
            var updatedPlaylist = GetPlaylists().Where(x => x.Name == playlistName).FirstOrDefault();
            
            // Voeg de nieuwe song toe aan de playlist
            updatedPlaylist.Songs.Add(song);
            return updatedPlaylist.Songs;
        }
    }
}
