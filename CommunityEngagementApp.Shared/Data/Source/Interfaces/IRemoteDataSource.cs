using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Model;

namespace CommunityEngagementApp.Shared.Data.Source.Interfaces
{
    public interface IRemoteDataSource
    {
        public Task<IList<PostDTO>> GetPostsUsingCouncilAsync(Guid councilGuid);
        Task<bool> UpdateLikeForUserAsync(Guid userGuid, Guid postGuid, bool isLiked);
        Task<IList<CommentDTO>> GetCommentsAsync(Guid postGuid);
        Task UploadCommentAsync(CommentDTO comment);
    }
}
