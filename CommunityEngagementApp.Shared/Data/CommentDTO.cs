using System;
using CommunityEngagementApp.Shared.Model;

namespace CommunityEngagementApp.Shared.Data
{
    public sealed class CommentDTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public Guid CommenterId { get; set; }
        public Guid PostGuid { get; set; }
        public string Text { get; set; }

        
    }
}
