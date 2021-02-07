using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;
using CommunityEngagementApp.Activity;
using CommunityEngagementApp.Shared.Helper;

namespace CommunityEngagementApp.Activities
{
    [Activity(
        Label = "@string/app_name",
        Theme = "@style/AppTheme.NoActionBar",
        Icon = "@drawable/logo",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        MainLauncher = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.splash);
            Task.Run(SetupAsync);
        }

        async Task SetupAsync()
        {
            // try to retrieve last signed in user
            var userDto = await App.DataManager.GetLastSignedInUserAsync()
                .ConfigureAwait(false);

            if (userDto == null)
            {
#if MOCK
                userDto = MockHelper.CreateMockUserDTO();
#endif
            }

            if (userDto != null)
            {
                await App.DataManager.SetSignedInUserAsync(userDto)
                    .ConfigureAwait(false);
            }

            RunOnUiThread(ShowActivity);
        }

        void ShowActivity()
        {
            if (App.DataManager.SignedInUser == null)
            {
                //TODO: start login/signup process
            }
            else
            {
                var intent = new Intent(this, typeof(HomeActivity));
                StartActivity(intent);
            }
        }
    }
}
