using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Data.Source.Interfaces;
using CommunityEngagementApp.Shared.Helper;

namespace CommunityEngagementApp.Shared.Data.Source
{
    public class MockRemoteServer : IRemoteDataSource
    {
        public MockRemoteServer()
        {
        }

        public Task<IList<CommentDTO>> GetCommentsAsync(Guid postGuid)
        {
            return MockHelper.CreateMockComments(postGuid);
        }

        public Task<IList<PostDTO>> GetPostsUsingCouncilAsync(Guid councilGuid)
        {
            return MockHelper.CreateMockPostsWithCouncilAsync(councilGuid);
        }

        public Task<bool> UpdateLikeForUserAsync(Guid userGuid, Guid postGuid, bool isLiked)
        {
            // not required for mock

            return Task.FromResult(true);
        }

        public Task UploadCommentAsync(CommentDTO comment)
        {
            // not required for mock

            return Task.CompletedTask;
        }
    }
}
