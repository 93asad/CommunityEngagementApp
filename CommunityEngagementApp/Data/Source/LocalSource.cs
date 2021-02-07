using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using CommunityEngagementApp.Shared.Data;
using CommunityEngagementApp.Shared.Data.Source.Interfaces;
using CommunityEngagementApp.Shared.Model;
using Newtonsoft.Json;

namespace CommunityEngagementApp.Data.Source
{
    public class LocalSource : ILocalDataSource
    {
        const string PREFERENCES_NAME = "Preferences";
        const string USER = "User";
        const string POSTS = "Posts";
        const string COMMENTS = "Comments";

        static ISharedPreferences Preferences
            => Android.App.Application.Context.GetSharedPreferences(
                PREFERENCES_NAME,
                FileCreationMode.Private);

        Task<UserDTO> GetUserInternalAsync(Guid? userGuid)
        {
            var userStr = Preferences.GetString(USER, string.Empty);

            if (string.IsNullOrWhiteSpace(userStr))
                return Task.FromResult<UserDTO>(null);

            var userDTO = JsonConvert.DeserializeObject<UserDTO>(userStr);

            if (userGuid != null && (userDTO == null || userDTO.Guid != userGuid))
                return Task.FromResult<UserDTO>(null);

            return Task.FromResult(userDTO);
        }

        public Task<UserDTO> GetUserAsync(Guid userGuid)
            => GetUserInternalAsync(userGuid);

        public Task<UserDTO> GetLastSignedInUserAsync()
            => GetUserInternalAsync(null);

        public Task SetUserAsync(UserDTO userDTO)
        {
            var userStr = JsonConvert.SerializeObject(userDTO);

            Preferences
                .Edit()
                .PutString(USER, userStr)
                .Apply();

            return Task.CompletedTask;
        }

        public Task<IList<PostDTO>> GetPostsAsync()
        {
            var postsStr = Preferences.GetString(POSTS, string.Empty);

            if (string.IsNullOrWhiteSpace(postsStr))
                return Task.FromResult((IList<PostDTO>)new List<PostDTO>());

            var posts = JsonConvert.DeserializeObject<IList<PostDTO>>(postsStr);

            if (posts == null || posts.Count == 0)
                return Task.FromResult((IList<PostDTO>)new List<PostDTO>());

            return Task.FromResult(posts);
        }

        public Task SavePostsAsync(IList<PostDTO> posts)
        {
            var postsStr = JsonConvert.SerializeObject(posts);

            Preferences
                .Edit()
                .PutString(POSTS, postsStr)
                .Apply();

            return Task.CompletedTask;
        }

        Task<IList<CommentDTO>> GetCommentsInternalAsync(Guid? postGuid)
        {
            var commentsStr = Preferences.GetString(COMMENTS, string.Empty);

            if (string.IsNullOrWhiteSpace(commentsStr))
                return Task.FromResult((IList<CommentDTO>)new List<CommentDTO>());

            var comments = JsonConvert.DeserializeObject<IList<CommentDTO>>(commentsStr);

            if (comments == null || comments.Count == 0)
                return Task.FromResult((IList<CommentDTO>)new List<CommentDTO>());

            if (postGuid == null)
                return Task.FromResult((IList<CommentDTO>)comments.ToList());

            return Task.FromResult((IList<CommentDTO>)comments
                .Where(c => c.PostGuid == postGuid).ToList());
        }

        public Task<IList<CommentDTO>> GetCommentsAsync(Guid postGuid)
            => GetCommentsInternalAsync(postGuid);

        public async Task AddCommentsAsync(IList<CommentDTO> commentsParam)
        {
            var comments = await GetCommentsInternalAsync(null);

            if (comments == null)
                comments = new List<CommentDTO>();

            (comments as List<CommentDTO>).AddRange(commentsParam);

            var commentsStr = JsonConvert.SerializeObject(comments);

            Preferences
                .Edit()
                .PutString(COMMENTS, commentsStr)
                .Apply();
        }

        public async Task<bool> UpdateLikeForUserAsync(Guid userGuid, Guid postGuid, bool isLiked)
        {
            try
            {
                var userDTO = await GetUserAsync(userGuid);

                if (userDTO == null)
                    return false;

                var changed = isLiked ?
                    userDTO.Likes.Add(postGuid) : userDTO.Likes.Remove(postGuid);

                if (!changed)
                    return false;

                await SetUserAsync(userDTO);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
