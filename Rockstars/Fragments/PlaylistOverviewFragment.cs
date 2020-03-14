using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Autofac;
using Rockstars.Activities;
using Rockstars.Adapters;
using Rockstars.Implementation.Models;
using Rockstars.Implementation.ViewModels;
using Rockstars.ViewModels;
using System;
using System.Collections.Generic;

namespace Rockstars.Fragments
{
    public class PlaylistOverviewFragment : Android.Support.V4.App.Fragment
    {
        private PlaylistAdapter _playlistAdapter;
        private IPlaylistViewModel _playlistViewModel;
        private RecyclerView _playlistRecyclerview;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.playlistOverviewFragment, container, false);

            // Initialize viewmodel
            _playlistViewModel = App.Container.Resolve<PlaylistViewModel>();
            _playlistViewModel.InitializePlaylists();

            _playlistAdapter = new PlaylistAdapter(_playlistViewModel);
            _playlistAdapter._itemClick += OnItemClick;

            FloatingActionButton fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += AddPlaylistClicked;

            _playlistRecyclerview = view.FindViewById<RecyclerView>(Resource.Id.playlistsRecyclerView);
            _playlistRecyclerview.SetLayoutManager(new LinearLayoutManager(this.Context));
            _playlistRecyclerview.SetItemAnimator(new DefaultItemAnimator());

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            // Abonneneer op de Playlist property op het viewmodel om
            // wanneer de Playlist wordt geupdate de recyclerview wordt bijgewerkt.
            _playlistViewModel.PropertyChanges(vm => vm.Playlists)
            .Subscribe(RedrawPlaylists);
        }

        private void RedrawPlaylists(IList<Playlist> playlists)
        {
            _playlistRecyclerview.SetAdapter(_playlistAdapter);
        }

        private void OnItemClick(object sender, int position)
        {
            string playlistName = _playlistViewModel.Playlists[position].Name;
            PlaylistDetailsActivity.Start(this.Context, playlistName);
        }

        private void AddPlaylistClicked(object sender, EventArgs eventArgs)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(this.Context);
            View view = layoutInflater.Inflate(Resource.Layout.newPlaylistDialog, null);
            Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this.Context);
            alertbuilder.SetView(view);
            var userdata = view.FindViewById<EditText>(Resource.Id.editText);
            alertbuilder.SetCancelable(false)
            .SetPositiveButton("Submit", delegate
            {
                _playlistViewModel.AddPlaylist(userdata.Text);
            })
            .SetNegativeButton("Cancel", delegate
            {
                alertbuilder.Dispose();
            });
            Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
            dialog.Show();
        }
    }
}