using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Model;

namespace CommunityEngagementApp.Shared.Data.Manager.Interfaces
{
    public interface IDataManager
    {
        User SignedInUser { get; }
        Task<IList<Post>> GetPostsUsingCouncilAsync (Guid councilGuid);
        Task<bool> UpdateLikeForUserAsync(Guid userGuid, Guid postGuid, bool isLiked);
        Task SetSignedInUserAsync(UserDTO userDTO);
        Task<UserDTO> GetLastSignedInUserAsync();
        Task<IList<Comment>> GetCommentsAsync(Guid postGuid);
        Task AddCommentAsync(Guid postGuid, Comment comment);
    }
}
