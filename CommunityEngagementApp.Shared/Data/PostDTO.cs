using System;
namespace CommunityEngagementApp.Shared.Data
{
    public class PostDTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public Guid PosterId { get; set; }
        public string Text { get; set; }
    }
}
