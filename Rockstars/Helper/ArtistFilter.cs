using Android.Runtime;
using Android.Widget;
using Java.Lang;
using Rockstars.Adapters;
using Rockstars.Implementation.Models;
using System.Collections.Generic;

namespace Rockstars
{
    /// <summary>
    /// FilterHelper
    /// Klasse die helpt bij het filteren van de Artists in de recyclerview
    /// </summary>
    public class FilterHelper : Filter
    {
        static IList<Artist> _artists;
        static ArtistAdapter _artistAdapter;

        /// <summary>
        /// FilterHelper
        /// </summary>
        /// <param name="currentList"></param>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public static FilterHelper NewInstance(IList<Artist> currentList, ArtistAdapter adapter)
        {
            _artistAdapter = adapter;
            _artists = currentList;
            return new FilterHelper();
        }

        /// <summary>
        /// PerformFiltering
        /// Filtered de artists op basis van een zoekterm
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected override FilterResults PerformFiltering(ICharSequence query)
        {
            FilterResults foundMatchingArtists = new FilterResults();
            if (query != null && query.Length() > 0)
            {
                List<Artist> matchingArtists = new List<Artist>();

                foreach (var artist in _artists)
                {
                    // Check of de desbetreffende artiest overeenkomt met de gezochte artiest (query)
                    if (artist.Name.ToUpper().Contains(query.ToString().ToUpper()))
                    {
                        matchingArtists.Add(artist);
                    }
                }

                // Voeg matching artists toe aan het resultaat
                foundMatchingArtists.Values = new JavaList<Artist>(matchingArtists);
            }
            else
            {
                // Geen filteroptie meegegeven, lijst met artists wordt in zijn geheel teruggegeven.
                foundMatchingArtists.Values = new JavaList<Artist>(_artists);
            }

            return foundMatchingArtists;
        }

        /// <summary>
        /// PublishResults
        /// </summary>
        /// <param name="constraint"></param>
        /// <param name="results"></param>
        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            var result = (JavaList<Artist>)results.Values;
            _artistAdapter.SetFilteredArtists(result);
            _artistAdapter.NotifyDataSetChanged();
        }
    }
}