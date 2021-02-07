using System;
namespace CommunityEngagementApp.Shared.Model
{
    public sealed class Post
    {
        public Guid PostGUID { get; set; }
        public string PostText { get; set; }
        public bool IsLiked { get; set; }
    }
}
