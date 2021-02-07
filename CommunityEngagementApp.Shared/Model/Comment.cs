using System;
namespace CommunityEngagementApp.Shared.Model
{
    public sealed class Comment
    {
        public Guid Guid { get; set; }
        public Guid CommenterId { get; set; }
        public string Text { get; set; }

        internal static Comment Create(Guid commenterGuid, string comment)
            => new Comment
            {
                CommenterId = commenterGuid,
                Text = comment,
                Guid = Guid.NewGuid()
            };
    }
}
