using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rockstars.Implementation.Managers;
using Rockstars.Implementation.Models;
using System.Linq;

namespace Rockstars.UnitTest
{
    [TestClass]
    public class PlaylistManagerTests
    {
        [TestMethod]
        public void SongIsCorrectlyAddedToPlaylist()
        {
            // Setup some testdata
            var playlistName = "Sjoerds playlist";
            var song = new Song()
            {
                Artist = "Robbie Williams",
                Name = "Angels"
            };

            IPlaylistManager playlistManager = new PlaylistManager();
            var currentPlaylists = playlistManager.GetPlaylists();

            // Controleer of playlist leeg is
            Assert.IsTrue(currentPlaylists.Count() == 0);

            // Voeg playlist toe
            playlistManager.AddPlaylist(playlistName);

            // Controleer of de playlist is toegevoegd
            Assert.IsTrue(currentPlaylists.Count() == 1);

            // Voeg een song toe aan de playlist
            playlistManager.AddSongToPlaylist(playlistName, song);

            // Ophalen van de songs in de playlist
            var songsInPlaylist = playlistManager.GetPlaylists().Where(x => x.Name == playlistName).FirstOrDefault().Songs;

            // Controleer of de song is toegevoegd
            Assert.IsTrue(songsInPlaylist.Count() == 1);
        }
    }
}
