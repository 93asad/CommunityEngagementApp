using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Data.Source.Interfaces;

namespace CommunityEngagementApp.Shared.Data.Source
{
    public class RemoteServer : IRemoteDataSource
    {
        public RemoteServer()
        {
        }

        public Task<IList<CommentDTO>> GetCommentsAsync(Guid postGuid)
        {
            throw new NotImplementedException();
        }

        public Task<IList<PostDTO>> GetPostsUsingCouncilAsync(Guid councilGuid)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLikeForUserAsync(Guid userGuid, Guid postGuid, bool isLiked)
        {
            throw new NotImplementedException();
        }

        public Task UploadCommentAsync(CommentDTO comment)
        {
            throw new NotImplementedException();
        }
    }
}
