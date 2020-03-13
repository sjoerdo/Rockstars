using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rockstars.Implementation.Managers;

namespace Rockstars.UnitTest
{
    [TestClass]
    public class MusicManagerTests
    {
        [TestMethod]
        public void DatasourceIsLoadedCorrectly()
        {
            IMusicManager musicManager = new MusicManager();
            var artists = musicManager.GetArtists();
            var songs = musicManager.GetSongs();

            Assert.AreEqual(artists.Count(), 887);
            Assert.AreEqual(songs.Count(), 2517);
        }
    }
}
