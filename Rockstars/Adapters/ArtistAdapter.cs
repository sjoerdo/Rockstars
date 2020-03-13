
using System;
using System.Collections.Generic;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Rockstars.Implementation.Models;
using Rockstars.ViewModels;

namespace Rockstars.Adapters
{
    /// <summary>
    /// Adapter voor  Artists
    /// Toont zowel de gehele lijst met artiesten als de gefilterde lijst van artiesten 
    /// op basis van een zoekterm.
    /// </summary>
    public class ArtistAdapter : RecyclerView.Adapter, IFilterable
    {
        private IList<Artist> _currentShownArtists;
        private readonly IList<Artist> _artists;
        public event EventHandler<int> _itemClick;

        /// <summary>
        /// ItemCount
        /// </summary>
        public override int ItemCount => _currentShownArtists.Count();

        /// <summary>
        /// ArtistFilter
        /// </summary>
        public Filter Filter
        {
            get { return FilterHelper.NewInstance(_artists, this); }
        }

        /// <summary>
        /// ArtistAdapter
        /// </summary>
        /// <param name="artistsViewModel"></param>
        public ArtistAdapter(IArtistsViewModel artistsViewModel)
        {
            _artists = artistsViewModel.Artists;
            _currentShownArtists = artistsViewModel.Artists;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecylerViewHolder h = holder as RecylerViewHolder;
            if (h != null)
            {
                // Toon de naam van de artiest
                h.NameTxt.Text = _currentShownArtists[position].Name;
            }
        }

        /// <summary>
        /// Update de adapterview met de gefilterde artiesten op basis van de zoekterm
        /// </summary>
        /// <param name="filteredArtists"></param>
        public void SetFilteredArtists(IList<Artist> filteredArtists)
        {
            _currentShownArtists = filteredArtists;
        }

        /// <summary>
        /// OnCreateViewHolder
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.artistItemView, parent, false);
            RecylerViewHolder holder = new RecylerViewHolder(v, OnClick);
            return holder;
        }

        private void OnClick(int position)
        {
            var artistId = _currentShownArtists[position].Id;
            _itemClick?.Invoke(this, artistId);
        }
    }
}