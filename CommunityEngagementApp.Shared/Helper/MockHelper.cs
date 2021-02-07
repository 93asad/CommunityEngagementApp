#if MOCK

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Data;
using CommunityEngagementApp.Shared.Model;

namespace CommunityEngagementApp.Shared.Helper
{
    public static class MockHelper
    {
        static Guid MockGuid => Guid.Parse("329ec1b7-99f3-4a85-ad78-186cb6843845");

        static Guid MockGuid1 => Guid.Parse("329ec1b7-99f3-4a85-ad78-186cb6843846");
        static Guid MockGuid2 => Guid.Parse("329ec1b7-99f3-4a85-ad78-186cb6843847");

        internal static Task<IList<CommentDTO>> CreateMockComments(Guid postGuid)
        {
            return Task.FromResult<IList<CommentDTO>>(
                new List<CommentDTO>()
            {
                new CommentDTO
                {
                    Guid = MockGuid1,
                    CommenterId = MockGuid1,
                    PostGuid = postGuid,
                    Text = "Comment1 weeewew "
                },
                new CommentDTO
                {
                    Guid = MockGuid2,
                    CommenterId = MockGuid2,
                    PostGuid = postGuid,
                    Text = "Comment2 fewfewf"
                },
                new CommentDTO
                {
                    Guid = MockGuid3,
                    CommenterId = MockGuid3,
                    PostGuid = postGuid,
                    Text = "Comment3rw fwf we fewfwee ter er gergrgrg4rt54hju6jytjty j6j56h5h567j jtyjtyjtyjtyj ytj6ji76uyhrtg3t43t  "
                }
            });
        }

        static Guid MockGuid3 => Guid.Parse("329ec1b7-99f3-4a85-ad78-186cb6843848");
        static Guid MockGuid4 => Guid.Parse("329ec1b7-99f3-4a85-ad78-186cb6843849");
        static Guid MockGuid5 => Guid.Parse("329ec1b7-99f3-4a85-ad78-186cb6843840");
        static Guid MockGuid6 => Guid.Parse("329ec1b7-99f3-4a85-ad78-186cb6843855");

        internal static UserDTO CreateMockUserDTO()
            => new UserDTO
            {
                Id = 1,
                Guid = MockGuid,
                Name = "MU",
                CouncilGuid = MockGuid,
                IsConcilStaffMember = false,
                SubscribedTo = new HashSet<Guid>(),
                Likes = new HashSet<Guid>()

            };

        internal static Task<IList<PostDTO>> CreateMockPostsWithCouncilAsync(Guid councilGuid)
        {
            return Task.FromResult<IList<PostDTO>>(
                new List<PostDTO>()
            {
                new PostDTO
                {
                    Guid = MockGuid1,
                    PosterGuid = MockGuid1,
                    Text = "Post wjrwqnjwq rwqnrqw rqw rjqw jrbqw bjrbqwj rbjqwbj rbjqw  rjbqwjb rbqjwr bjqw bjrqwbjr "
                },
                new PostDTO
                {
                    Guid = MockGuid2,
                    PosterGuid = MockGuid2,
                    Text = "Post2 wjrwqnjwq rwqnrqw rqw rjqw jrbqw bjrbqwj rbjqwbj rbjqw  rjbqwjb rbqjwr bjqw bjrqwbjr "
                },
                new PostDTO
                {
                    Guid = MockGuid3,
                    PosterGuid = MockGuid3,
                    Text = "Post3 wjrwqnjwq rwqnrqw rqw rjqw jrbqw bjrbqwj rbjqwbj rbjqw  rjbqwjb rbqjwr bjqw bjrqwbjr "
                },
                new PostDTO
                {
                    Guid = MockGuid4,
                    PosterGuid = MockGuid4,
                    Text = "Post4 wjrwqnjwq rwqnrqw rqw rjqw jrbqw bjrbqwj rbjqwbj rbjqw  rjbqwjb rbqjwr bjqw bjrqwbjr "
                },
                new PostDTO
                {
                    Guid = MockGuid5,
                    PosterGuid = MockGuid5,
                    Text = "Post5 wjrwqnjwq rwqnrqw rqw rjqw jrbqw bjrbqwj rbjqwbj rbjqw  rjbqwjb rbqjwr bjqw bjrqwbjr "
                },
                new PostDTO
                {
                    Guid = MockGuid6,
                    PosterGuid = MockGuid6,
                    Text = "Post6 wjrwqnjwq rwqnrqw rqw rjqw jrbqw bjrbqwj rbjqwbj rbjqw  rjbqwjb rbqjwr bjqw bjrqwbjr "
                }
            });
        }
    }
}

#endif
