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

        internal static CommentDTO From(Comment comment, Guid postGuid)
        {
            return new CommentDTO
            {
                CommenterId = comment.CommenterId,
                PostGuid = postGuid,
                Guid = Guid.NewGuid(),
                Id = GenerateRandomNumber(),
                Text = comment.Text
            };
        }

        static int GenerateRandomNumber()
        {
            //TODO: add logic
            return 1;
        }
    }
}
