using System;
using System.Threading.Tasks;
using CommunityEngagementApp.Shared.Data;

namespace CommunityEngagementApp.Shared.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<UserDTO> SigninAsync(string username, string password);
    }
}
