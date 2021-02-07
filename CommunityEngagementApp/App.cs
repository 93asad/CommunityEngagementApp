using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using CommunityEngagementApp.Activity;
using CommunityEngagementApp.Data.Source;
using CommunityEngagementApp.Shared.Data.Manager;
using CommunityEngagementApp.Shared.Data.Manager.Interfaces;
using CommunityEngagementApp.Shared.Data.Source;
using CommunityEngagementApp.Shared.Helper;

namespace CommunityEngagementApp
{
    [Application]
    public class App : Application
    {
        public static IDataManager DataManager { get; private set; }

        public App(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            SetupDataManager();
        }

        void SetupDataManager()
        {
#if MOCK
            DataManager = new DataManager(new LocalSource(), new MockRemoteServer());
#endif
        }
    }
}
