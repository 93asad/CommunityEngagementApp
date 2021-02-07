using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Data.Manager.Interfaces;
using CommunityEngagementApp.Shared.Data.Source.Interfaces;
using CommunityEngagementApp.Shared.Model;

namespace CommunityEngagementApp.Shared.Data.Manager
{
    public class DataManager : IDataManager
    {
        public User SignedInUser { get; private set; }

        ILocalDataSource LocalDataSource { get; }
        IRemoteDataSource RemoteDataSource { get; }

        //TODO 1: Add sync logic

        public DataManager(
            ILocalDataSource localDataSource,
            IRemoteDataSource remoteDataSource)
        {
            LocalDataSource = localDataSource;
            RemoteDataSource = remoteDataSource;
        }

        public async Task<UserDTO> GetLastSignedInUserAsync()
        {
            try
            {
                return await LocalDataSource.GetLastSignedInUserAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task SetSignedInUserAsync(UserDTO userDTO)
        {
            await LocalDataSource.SetUserAsync(userDTO);
            SignedInUser = userDTO.ToUser();
        }

        public async Task<IList<Post>> GetPostsUsingCouncilAsync(Guid councilGuid)
        {
            try
            {
                var result = await LocalDataSource.GetPostsAsync();

                if (result != null && result.Count > 0)
                    return ConvertToPostsForUser(result, SignedInUser);

                result = await RemoteDataSource.GetPostsUsingCouncilAsync(councilGuid);

                if (result != null && result.Count > 0)
                {
                    await LocalDataSource.SavePostsAsync(result);
                }

                return ConvertToPostsForUser(result, SignedInUser);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IList<Comment>> GetCommentsAsync(Guid postGuid)
        {
            try
            {
                var result = await LocalDataSource.GetCommentsAsync(postGuid);

                if (result != null && result.Count > 0)
                    return ConvertToComments(result);

                result = await RemoteDataSource.GetCommentsAsync(postGuid);

                if (result != null && result.Count > 0)
                {
                    await LocalDataSource.AddCommentsAsync(result);
                }

                return ConvertToComments(result);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        IList<Comment> ConvertToComments(IList<CommentDTO> comments)
        {
            if (comments == null)
                return null;

            var result = comments.Select(c => new Comment
            {
                Guid = c.Guid,
                CommenterId = c.CommenterId,
                Text = c.Text
            });

            return result.ToList();
        }

        public async Task<bool> UpdateLikeForUserAsync(Guid userGuid, Guid postGuid, bool isLiked)
        {
            try
            {
                await LocalDataSource.UpdateLikeForUserAsync(userGuid, postGuid, isLiked);
                await RemoteDataSource.UpdateLikeForUserAsync(userGuid, postGuid, isLiked);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task AddCommentAsync(Guid postGuid, Comment comment)
        {
            try
            {
                var dto = CommentDTO.From(comment, postGuid);
                await LocalDataSource.AddCommentsAsync(new List<CommentDTO> { dto });
                await RemoteDataSource.UploadCommentAsync(dto);
            }
            catch
            {
            }
        }

        IList<Post> ConvertToPostsForUser(IList<PostDTO> posts, User user)
        {
            if (posts == null || posts.Count == 0 || user == null)
                return null;

            var result = new List<Post>(posts.Count);

            foreach(var post in posts)
            {
                result.Add(new Post
                {
                    PostGUID = post.Guid,
                    PostText = post.Text,
                    IsLiked = user.Likes.Contains(post.Guid)
                });
            }

            return result;
        }
    }
}
