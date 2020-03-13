using Rockstars.Implementation.Models;
using System.Collections.Generic;

namespace Rockstars.Implementation.Managers
{
    /// <summary>
    /// IMusicManager
    /// Verantwoordelijk voor het ophalen van muziek
    /// </summary>
    public interface IMusicManager
    {
        /// <summary>
        /// Ophalen van alle artiesten
        /// </summary>
        /// <returns></returns>
        IList<Artist> GetArtists();

        /// <summary>
        /// Ophalen van alle songs
        /// </summary>
        /// <returns></returns>
        IList<Song> GetSongs();

        /// <summary>
        /// Ophalen van songs van een artiest
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        IList<Song> GetSongsFromArtist(int artistId);
    }
}
