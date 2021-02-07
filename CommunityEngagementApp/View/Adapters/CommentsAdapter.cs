using System;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CommunityEngagementApp.Shared.Model;

namespace CommunityEngagementApp.View.Adapters
{
    public class CommentsAdapter : RecyclerView.Adapter
    {
        public override int ItemCount => Comments.Count;

        IList<Comment> Comments { get; set; } = new List<Comment>();
        
        public void SetComments (IList<Comment> comments)
        {
            Comments.Clear();
            ((List<Comment>)Comments).AddRange(comments);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            (holder as CommentViewHolder).BindView(Comments[position]);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            => new CommentViewHolder(
                LayoutInflater.From(parent.Context).Inflate(Resource.Layout.comment_cell, parent, false));

        class CommentViewHolder : RecyclerView.ViewHolder
        {
            TextView _commentTextView;

            public CommentViewHolder(Android.Views.View view) : base(view)
            {
                _commentTextView = view.FindViewById<TextView>(Resource.Id.comment_text);
            }

            internal void BindView(Comment comment)
            {
                UpdateViews(comment);
            }

            void UpdateViews(Comment comment)
            {
                _commentTextView.Text = comment.Text;
            }
        }
    }
}
