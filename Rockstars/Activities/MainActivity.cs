using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Rockstars.Fragments;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Rockstars
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", Icon = "@drawable/logo", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private BottomNavigationView _bottomNavigation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            _bottomNavigation.SetOnNavigationItemSelectedListener(this);

            // Inladen default fragment Artists
            LoadFragment(Resource.Id.artists);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            LoadFragment(item.ItemId);
            return true;
        }

        private void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;

            switch (id)
            {
                case Resource.Id.artists:
                    fragment = new ArtistsOverviewFragment();
                    break;
                case Resource.Id.playlists:
                    fragment = new PlaylistOverviewFragment();
                    break;
            }

            if (fragment == null)
                return;

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
    }
}

