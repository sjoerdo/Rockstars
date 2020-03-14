using Rockstars.Implementation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockstars.ViewModels
{
    /// <summary>
    /// IArtistsViewModel
    /// </summary>
    public interface IArtistsViewModel: INotifyPropertyChanged
    {
        /// <summary>
        /// Initialiseert het viewmodel 
        /// </summary>
        void Initialize();

        /// <summary>
        /// Lijst met artists
        /// </summary>
        IList<Artist> Artists { get; }

        /// <summary>
        /// Ophalen van een artist by id
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        Artist GetArtist(int artistId);

        /// <summary>
        /// Ophalen van songs van een artist
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        IList<Song> GetSongsFromArtist(int artistId);

        /// <summary>
        /// Ophalen van alle songs
        /// </summary>
        /// <returns></returns>
        IList<Song> GetSongs();
    }
}
