using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Autofac;
using Rockstars.Adapters;
using Rockstars.ViewModels;

namespace Rockstars
{
    [Activity(Label = "ArtistDetails")]
    public class ArtistDetailsAcitivty : AppCompatActivity
    {
        private const string ExtraKey = "rockstars.artist";
        private TextView _artistName;
        private IArtistsViewModel _artistsViewModel;
        private SongsAdapter _songsAdapter;
        private RecyclerView _artistSongsRecyclerview;

        public static void Start(Context context, int artistId)
        {
            Intent intent = new Intent(context, typeof(ArtistDetailsAcitivty));
            intent.PutExtra(ExtraKey, artistId);

            context.StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.artistDetailActivity);

            // Ophalen van de ArtistId uit de Intent
            var selectedArtistId = Intent.GetIntExtra(ExtraKey, -1);

            // Initialize viewmodel
            _artistsViewModel = App.Container.Resolve<ArtistsViewModel>();
            _artistsViewModel.Initialize();

            _songsAdapter = new SongsAdapter(_artistsViewModel.GetSongsFromArtist(selectedArtistId));

            _artistSongsRecyclerview = FindViewById<RecyclerView>(Resource.Id.ArtistSongs);
            _artistSongsRecyclerview.SetLayoutManager(new LinearLayoutManager(this));
            _artistSongsRecyclerview.SetItemAnimator(new DefaultItemAnimator());
            _artistSongsRecyclerview.SetAdapter(_songsAdapter);

            _artistName = FindViewById<TextView>(Resource.Id.artistName);
            _artistName.Text = _artistsViewModel.GetArtist(selectedArtistId).Name;

            SupportActionBar.Title = Resources.GetString(Resource.String.artist_activity_title);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
    }
}