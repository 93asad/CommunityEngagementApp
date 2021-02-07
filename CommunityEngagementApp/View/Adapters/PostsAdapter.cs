using System;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CommunityEngagementApp.Shared.Model;

namespace CommunityEngagementApp.View.Adapters
{
    public class PostsAdapter : RecyclerView.Adapter
    {
        public interface IDelegate
        {
            void OnLikeButtonClicked(int itemPosition);
            void OnCommentsButtonClicked(int itemPosition);
        }

        public override int ItemCount => Posts.Count;

        IDelegate Delegate { get; }

        IList<Post> Posts { get; set; } = new List<Post>();

        public PostsAdapter(IDelegate del)
        {
            Delegate = del;
        }

        public void SetPosts (IList<Post> posts)
        {
            Posts.Clear();
            ((List<Post>)Posts).AddRange(posts);
            NotifyDataSetChanged();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            (holder as PostViewHolder).BindView(Posts[position], position);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            => new PostViewHolder(
                LayoutInflater.From(parent.Context).Inflate(Resource.Layout.post_cell, parent, false),
                Delegate.OnLikeButtonClicked,
                Delegate.OnCommentsButtonClicked);

        class PostViewHolder : RecyclerView.ViewHolder
        {
            TextView _postTextView;
            ImageButton _likeButton;
            Button _commentsButton;
            int _position;

            public PostViewHolder(
                Android.Views.View view,
                Action<int> onLikeButtonClicked,
                Action<int> onCommentsButtonClicked) : base(view)
            {
                _postTextView = view.FindViewById<TextView>(Resource.Id.post_text);
                _likeButton = view.FindViewById<ImageButton>(Resource.Id.post_like_btn);
                _commentsButton = view.FindViewById<Button>(Resource.Id.post_comment_btn);

                _likeButton.Click += delegate { onLikeButtonClicked(_position); };
                _commentsButton.Click += delegate { onCommentsButtonClicked(_position); };
            }

            internal void BindView(Post post, int position)
            {
                _position = position;
                UpdateViews(post);
            }

            void UpdateViews(Post post)
            {
                _postTextView.Text = post.PostText;
                UpdateLikeButton(post.IsLiked);
            }

            void UpdateLikeButton(bool isLiked)
            {
                var res = isLiked ?
                    Resource.Drawable.liked : Resource.Drawable.not_liked;

                _likeButton.SetImageResource(res);
            }
        }
    }
}
