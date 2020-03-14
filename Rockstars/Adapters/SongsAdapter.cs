using Android.Support.V7.Widget;
using Android.Views;
using Rockstars.Implementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rockstars.Adapters
{
    /// <summary>
    /// SongsAdapter
    /// Toont de songs
    /// </summary>
    public class SongsAdapter : RecyclerView.Adapter
    {
        private readonly IList<Song> _songs = new List<Song>();
        public event EventHandler<int> _itemClick;

        /// <summary>
        /// ItemCount
        /// </summary>
        public override int ItemCount
        {
            get
            {
                if (_songs != null)
                {
                    return _songs.Count();
                }
                return 0;
            }
        }

        /// <summary>
        /// SongsAdapter
        /// </summary>
        /// <param name="songs"></param>
        public SongsAdapter(IList<Song> songs)
        {
            _songs = songs;
        }

        /// <summary>
        /// OnBindViewHolder
        /// </summary>
        /// <param name="holder"></param>
        /// <param name="position"></param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is RecylerViewHolder h) h.NameTxt.Text = _songs[position].Name;
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
            _itemClick?.Invoke(this, position);
        }
    }
}