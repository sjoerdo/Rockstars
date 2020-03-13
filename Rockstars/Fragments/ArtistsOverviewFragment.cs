using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Rockstars.Adapters;
using SearchView = Android.Widget.SearchView;
using Rockstars.ViewModels;
using Rockstars.Implementation.Models;
using Android.OS;

namespace Rockstars.Fragments
{
    /// <summary>
    /// ArtistsOverviewFragment
    /// Toont de details van een Artist
    /// </summary>
    public class ArtistsOverviewFragment : Android.Support.V4.App.Fragment
    {
        private SearchView _artistSearchView;
        private RecyclerView _artistRecyclerview;
        private ArtistAdapter _artistAdapter;
        private IArtistsViewModel _artistsViewModel;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.artistsOverviewFragment, container, false);

            // Initialize viewmodel
            _artistsViewModel = App.Container.Resolve<ArtistsViewModel>();
            _artistsViewModel.Initialize();

            _artistSearchView = view.FindViewById<SearchView>(Resource.Id.searchView);

            _artistRecyclerview = view.FindViewById<RecyclerView>(Resource.Id.artistRecyclerView);
            _artistRecyclerview.SetLayoutManager(new LinearLayoutManager(this.Context));
            _artistRecyclerview.SetItemAnimator(new DefaultItemAnimator());
            _artistSearchView.QueryTextChange += ArtistSearchTextChanged;

            _artistAdapter = new ArtistAdapter(_artistsViewModel);
            _artistAdapter._itemClick += OnItemClick;

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            // Abonneer op de propery changes van Artists op het viewmodel zodat de recyclerview 
            // wordt geupdate indien nodig
            _artistsViewModel.PropertyChanges(vm => vm.Artists)
            .Subscribe(RedrawArtists);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _artistAdapter._itemClick -= OnItemClick;
        }

        private void RedrawArtists(IList<Artist> artists)
        {
            _artistRecyclerview.SetAdapter(_artistAdapter);
        }

        private void OnItemClick(object sender, int artistId)
        {
            ArtistDetailsAcitivty.Start(this.Context, artistId);
        }

        private void ArtistSearchTextChanged(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            _artistAdapter.Filter.InvokeFilter(e.NewText);
        }
    }
}