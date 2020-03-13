using System;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using Rockstars.ViewModels;

namespace Rockstars.Adapters
{
    /// <summary>
    /// PlaylistAdapter
    /// Toont de playlists
    /// </summary>
    public class PlaylistAdapter : RecyclerView.Adapter
    {
        private readonly IPlaylistViewModel _playlistViewModel;
        public event EventHandler<int> _itemClick;

        /// <summary>
        /// ItemCount
        /// </summary>
        public override int ItemCount => _playlistViewModel.Playlists.Count();

        /// <summary>
        /// PlaylistAdapter
        /// </summary>
        /// <param name="playlistViewModel"></param>
        public PlaylistAdapter(IPlaylistViewModel playlistViewModel)
        {
            _playlistViewModel = playlistViewModel;
        }

        /// <summary>
        /// OnBindViewHolder
        /// </summary>
        /// <param name="holder"></param>
        /// <param name="position"></param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecylerViewHolder h = holder as RecylerViewHolder;
            if (h != null) h.NameTxt.Text = _playlistViewModel.Playlists[position].Name;
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