using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Data.Manager.Interfaces;
using CommunityEngagementApp.Shared.Model;

namespace CommunityEngagementApp.ViewModel
{
    public class CommentsFragmentViewModel : AndroidX.Lifecycle.ViewModel
    {
        public Guid? PostGuid { get; set; }

        public TaskCompletionSource<IList<Comment>> CommentsTcs { get; set; }
        public IList<Comment> Comments { get; set; }

        IDataManager DataManager { get; }

        object _lock = new object();
        bool _isFetching;

        public CommentsFragmentViewModel(IDataManager dataManager)
        {
            DataManager = dataManager;
        }

        public Task<IList<Comment>> FetchCommentsAsync()
        {
            lock (_lock)
            {
                if (_isFetching)
                    return CommentsTcs.Task;

                _isFetching = true;
            }

            CommentsTcs = new TaskCompletionSource<IList<Comment>>();

            return DataManager.GetCommentsAsync(PostGuid.Value).
                ContinueWith(t =>
                {
                    lock (_lock)
                    {
                        CommentsTcs = null;
                        _isFetching = false;
                    }

                    if (t.Exception != null)
                        return new List<Comment>();
                    else
                        return t.Result;
                });
        }

        public bool AddCommentAsync(string text)
        {
            try
            {
                var comment = Comment.Create(DataManager.SignedInUser.Guid, text);
                Comments.Insert(0, comment);

                Task.Run(async delegate
                {
                    await DataManager.AddCommentAsync(PostGuid.Value,
                        Comment.Create(DataManager.SignedInUser.Guid, text));
                });
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
