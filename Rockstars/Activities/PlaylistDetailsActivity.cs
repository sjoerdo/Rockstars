using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Autofac;
using Rockstars.Adapters;
using Rockstars.Implementation.Models;
using Rockstars.Implementation.ViewModels;
using Rockstars.ViewModels;
using System;

namespace Rockstars.Activities
{
    [Activity(Label = "PlaylistDetailsActivity")]
    public class PlaylistDetailsActivity : AppCompatActivity
    {
        private const string ExtraKey = "rockstars.playlist";
        private TextView playlistTitle;
        private SongsAdapter _songsAdapter;
        private RecyclerView _playlistSongsRecyclerview;
        private IPlaylistViewModel _playlistViewModel;
        private IArtistsViewModel _artistsViewModel;
        private Android.Support.V7.App.AlertDialog _dialog;
        private FloatingActionButton _fab;

        /// <summary>
        /// Helper method om Activity te starten
        /// </summary>
        /// <param name="context"></param>
        /// <param name="playlistName"></param>
        public static void Start(Context context, string playlistName)
        {
            Intent intent = new Intent(context, typeof(PlaylistDetailsActivity));
            intent.PutExtra(ExtraKey, playlistName);
            context.StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.playlistDetailsActivity);

            // Get data uit intent
            var playlistName = Intent.GetStringExtra(ExtraKey);
            playlistTitle = FindViewById<TextView>(Resource.Id.playlistTitle);
            playlistTitle.Text = playlistName;

            // Initialize viewmodels
            _playlistViewModel = App.Container.Resolve<PlaylistViewModel>();
            _artistsViewModel = App.Container.Resolve<ArtistsViewModel>();
            _playlistViewModel.InitializePlaylists();
            _playlistViewModel.InitializeSelectedPlaylist(playlistName);

            _songsAdapter = new SongsAdapter(_playlistViewModel.SelectedPlaylist.Songs);

            _playlistSongsRecyclerview = FindViewById<RecyclerView>(Resource.Id.playlistSongs);
            _playlistSongsRecyclerview.SetLayoutManager(new LinearLayoutManager(this));
            _playlistSongsRecyclerview.SetItemAnimator(new DefaultItemAnimator());
            _playlistSongsRecyclerview.SetAdapter(_songsAdapter);

            _fab = FindViewById<FloatingActionButton>(Resource.Id.fabAddSongToPlaylist);
            _fab.Click += AddPlaylistClicked;

            SupportActionBar.Title = Resources.GetString(Resource.String.playlist_activity_title);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
        }

        protected override void OnStart()
        {
            base.OnStart();

            // Abonneneer op de SelectedPlaylist property van het ViewModel zodat als
            // de songs worden ge-update de recyclerview wordt bijgewerkt.
            _playlistViewModel.PropertyChanges(vm => vm.SelectedPlaylist)
            .Subscribe(RedrawSongsInPlaylist);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _fab.Click -= AddPlaylistClicked;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }

        private void RedrawSongsInPlaylist(Playlist playlist)
        {
            _playlistSongsRecyclerview.SetAdapter(_songsAdapter);
        }

        private void AddPlaylistClicked(object sender, EventArgs eventArgs)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View view = layoutInflater.Inflate(Resource.Layout.addSongToPlaylistDialog, null);

            var adapter = new SongsAdapter(_artistsViewModel.GetSongs());
            adapter._itemClick += OnItemClick;
            var recyclerview = view.FindViewById<RecyclerView>(Resource.Id.addSongsToPlaylist);
            recyclerview.SetLayoutManager(new LinearLayoutManager(this));
            recyclerview.SetItemAnimator(new DefaultItemAnimator());
            recyclerview.SetAdapter(adapter);

            Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertbuilder.SetView(view);
            alertbuilder.SetCancelable(false)
            .SetNegativeButton("Cancel", delegate
            {
                alertbuilder.Dispose();
            });
            _dialog = alertbuilder.Create();
            _dialog.Show();
        }

        private void OnItemClick(object sender, int position)
        {
            Toast.MakeText(this, "Toegevoegd aan afspeellijst", ToastLength.Short).Show();
            var song = _artistsViewModel.GetSongs()[position];
            _playlistViewModel.AddSongToSelectedPlaylist(song);
            _dialog.Cancel();
        }
    }
}