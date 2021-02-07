using System;
using CommunityEngagementApp.Shared.Data.Manager.Interfaces;
using CommunityEngagementApp.View.Adapters;
using Java.Lang;

namespace CommunityEngagementApp.ViewModel
{
    public class PostsFragmentViewModelFactory : Java.Lang.Object, AndroidX.Lifecycle.ViewModelProvider.IFactory
    {
        IDataManager _dataManager;

        public PostsFragmentViewModelFactory()
        {

        }

        public PostsFragmentViewModelFactory(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Java.Lang.Object Create(Class p0)
        {
            return new PostsFragmentViewModel(_dataManager);
        }
    }
}
