using System.Collections.Generic;

namespace Rockstars.Implementation.Models
{
    /// <summary>
    /// Playlist
    /// </summary>
    public class Playlist
    {
        /// <summary>
        /// Naam
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Songs in playlist
        /// </summary>
        public IList<Song> Songs { get; set; } = new List<Song>();

    }
}