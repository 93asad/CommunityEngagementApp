
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Lifecycle;
using AndroidX.RecyclerView.Widget;
using CommunityEngagementApp.View.Adapters;
using CommunityEngagementApp.ViewModel;

namespace CommunityEngagementApp.Fragments
{
    public class PostsFragment : BaseFragment, PostsAdapter.IDelegate
    {
        public interface IDelegate : IBaseDelegeate
        {
            void OnCommentButtonClicked(Guid postGuid);
        }

        public new IDelegate Delegate
        {
            get => base.Delegate as IDelegate;
            set => base.Delegate = value;
        }

        protected override string Title => "Posts";

        PostsFragmentViewModel _viewModel;
        RecyclerView _recyclerView;
        PostsAdapter _postsAdapter { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _viewModel = (PostsFragmentViewModel)new ViewModelProvider(Activity, new PostsFragmentViewModelFactory(App.DataManager))
                .Get(Java.Lang.Class.FromType(typeof(PostsFragmentViewModel)));
        }

        public override void OnViewCreated(Android.Views.View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.posts);

            _recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            _recyclerView.SetItemAnimator(new DefaultItemAnimator());

            _postsAdapter = new PostsAdapter(this);
            _recyclerView.SetAdapter(_postsAdapter);

            Task.Run(SetPostsAsync);
        }

        async Task SetPostsAsync()
        {
            if (_viewModel.Posts == null || _viewModel.Posts.Count == 0)
            {
                _viewModel.Posts = await _viewModel.FetchPostsAsync();
            }

            if (_viewModel.Posts != null && _viewModel.Posts.Count > 0)
            {
                Activity.RunOnUiThread(delegate
                {
                    _postsAdapter.SetPosts(_viewModel.Posts);
                });
            }
        }

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);

            if (context is IDelegate postsFragmentDelegate)
            {
                Delegate = postsFragmentDelegate;
            }
        }
        
        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            => inflater.Inflate(Resource.Layout.fragment_posts, container, false);

        void PostsAdapter.IDelegate.OnLikeButtonClicked(int itemPosition)
        {
            if (_viewModel.ToggleLike(itemPosition))
            {
                _postsAdapter.NotifyItemChanged(itemPosition);
            }
        }

        void PostsAdapter.IDelegate.OnCommentsButtonClicked(int itemPosition)
        {
            var post = _viewModel.GetPost(itemPosition);

            if (post == null)
                return;

            Delegate?.OnCommentButtonClicked(post.PostGUID);
        }
    }
}
