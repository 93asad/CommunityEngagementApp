using System;
using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.DrawerLayout.Widget;
using AndroidX.Lifecycle;
using CommunityEngagementApp.Fragments;
using CommunityEngagementApp.ViewModel;
using Google.Android.Material.Navigation;

namespace CommunityEngagementApp.Activity
{
    [Activity(Theme = "@style/AppTheme.NoActionBar")]
    public class HomeActivity : AppCompatActivity ,
        PostsFragment.IDelegate,
        CommentsFragment.IDelegate
    {
        DrawerLayout _drawerLayout;
        NavigationView _navigationView;
        HomeActivityViewModel _viewModel;
        
        #region Overrides
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_home);

            Init();
            Setup();
        }

        public override void OnAttachFragment(AndroidX.Fragment.App.Fragment fragment)
        {
            base.OnAttachFragment(fragment);

            if (fragment is PostsFragment postsFragment)
            {
                postsFragment.Delegate = this;
            }
            else if (fragment is CommentsFragment postsDetailFragment)
            {
                postsDetailFragment.Delegate = this;
            }
        }

        #endregion

        void Init()
        {
            _viewModel = (HomeActivityViewModel)new ViewModelProvider(
                this)
                .Get(Java.Lang.Class.FromType(typeof(HomeActivityViewModel)));

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
        }

        void Setup()
        {
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SetDrawerToggle(toolbar);
            SetupDrawerContent(_navigationView);
            ShowInitialFragment();
        }

        void SetDrawerToggle(Toolbar toolbar)
        {
            var drawerToggle = new ActionBarDrawerToggle(
                this,
                _drawerLayout,
                toolbar,
                Resource.String.drawer_open,
                Resource.String.drawer_close);

            _drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
        }

        void ShowInitialFragment()
        {
            var currentFragmentTag = _viewModel.CurrentFragmentTag;

            if (string.IsNullOrWhiteSpace(currentFragmentTag) ||
                currentFragmentTag == BaseFragment.GetTag<PostsFragment>())
            {
                var item = _navigationView.Menu.FindItem(Resource.Id.nav_posts);
                SwapFragmentForMenuItem(item);
            }
            else if (currentFragmentTag == BaseFragment.GetTag<CommentsFragment>())
            {
                SwapFragmentForNonMenuItem(new CommentsFragment());
            }

        }

        void SwapFragmentForNonMenuItem(BaseFragment fragment)
        {
            _navigationView.SetCheckedItem(Resource.Id.menu_none);
            ShowFragment(fragment);
        }

        void SwapFragmentForMenuItem(Android.Views.IMenuItem item)
        {
            // set item selected in the navigation
            item.SetChecked(true);

            switch (item.ItemId)
            {
                case Resource.Id.nav_posts:
                    ShowFragment(new PostsFragment());
                    break;
                default: break; 
            }
        }

        void ShowFragment(BaseFragment fragment)
        {
            var tag = fragment.GetTag();

            var transaction = SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.fragment_container, fragment, tag);

            transaction.AddToBackStack(tag);
            transaction.Commit();

            _viewModel.CurrentFragmentTag = tag;
        }
        
        void SetupDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) => {

                _drawerLayout.CloseDrawers();

                if (e.MenuItem.IsChecked)
                    return; // already on display

                e.MenuItem.SetChecked(true);
                

                SwapFragmentForMenuItem(e.MenuItem);
            };
        }

        #region Interfaces

        void PostsFragment.IDelegate.OnCommentButtonClicked(Guid postGuid)
            => SwapFragmentForNonMenuItem(new CommentsFragment(postGuid));

        void BaseFragment.IBaseDelegeate.SetToolbarTitle(string title)
            => SupportActionBar.Title = title;

        #endregion
    }
}
