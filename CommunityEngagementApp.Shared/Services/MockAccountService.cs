using System;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Data;
using CommunityEngagementApp.Shared.Services.Interfaces;

namespace CommunityEngagementApp.Shared.Services
{
    public class MockAccountService : IAccountService
    {
        public Task<UserDTO> SigninAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
