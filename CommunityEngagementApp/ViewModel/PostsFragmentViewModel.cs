using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Data.Manager.Interfaces;
using CommunityEngagementApp.Shared.Model;
using CommunityEngagementApp.View.Adapters;

namespace CommunityEngagementApp.ViewModel
{
    public class PostsFragmentViewModel : AndroidX.Lifecycle.ViewModel
    {
        public TaskCompletionSource<IList<Post>> PostsTcs { get; set; }
        public IList<Post> Posts { get; set; }

        IDataManager DataManager { get; }

        object _lock = new object();
        bool _isFetching;

        public PostsFragmentViewModel(IDataManager dataManager)
        {
            DataManager = dataManager;
        }

        public bool ToggleLike(int itemPosition)
        {
            try
            {
                var post = Posts[itemPosition];
                post.IsLiked = !post.IsLiked;

                Task.Run(async delegate
                {
                    await DataManager.UpdateLikeForUserAsync(
                        DataManager.SignedInUser.Guid,
                        post.PostGUID,
                        post.IsLiked);
                });
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Task<IList<Post>> FetchPostsAsync()
        {
            lock (_lock)
            {
                if (_isFetching)
                    return PostsTcs.Task;

                _isFetching = true;
            }

            PostsTcs = new TaskCompletionSource<IList<Post>>();

            return DataManager.GetPostsUsingCouncilAsync(
                DataManager.SignedInUser.CouncilGuid).
                ContinueWith(t =>
                {
                    lock (_lock)
                    {
                        PostsTcs = null;
                        _isFetching = true;
                    }

                    if (t.Exception != null)
                        return new List<Post>();
                    else
                        return t.Result;
                });
        }

        internal Post GetPost(int itemPosition)
        {
            try
            {
                return Posts[itemPosition];
            }
            catch
            {
                return null;
            }
        }
    }
}
