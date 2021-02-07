using System;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Data;
using CommunityEngagementApp.Shared.Services.Interfaces;

namespace CommunityEngagementApp.Shared.Services
{
    /// <summary>
    /// Used to manage user account using back-end server
    /// </summary>
    public class AccountService : IAccountService
    {
        public Task<UserDTO> SigninAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
