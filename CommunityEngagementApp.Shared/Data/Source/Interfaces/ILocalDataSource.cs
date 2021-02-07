using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Model;

namespace CommunityEngagementApp.Shared.Data.Source.Interfaces
{
    public interface ILocalDataSource
    {
        Task<UserDTO> GetUserAsync(Guid userGuid);
        Task SetUserAsync(UserDTO userDTO);
        Task<IList<PostDTO>> GetPostsAsync();
        Task<bool> UpdateLikeForUserAsync(Guid userGuid, Guid postGuid, bool isLiked);
        Task SavePostsAsync(IList<PostDTO> posts);
        Task<UserDTO> GetLastSignedInUserAsync();
        Task<IList<CommentDTO>> GetCommentsAsync(Guid postGuid);
        Task AddCommentsAsync(IList<CommentDTO> comments);
    }
}
