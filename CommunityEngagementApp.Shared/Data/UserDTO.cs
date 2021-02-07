using System;
using System.Collections.Generic;
using CommunityEngagementApp.Shared.Model;

namespace CommunityEngagementApp.Shared.Data
{
    public sealed class UserDTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public Guid CouncilGuid { get; set; }
        public bool IsConcilStaffMember { get; set; }

        /// <summary>
        /// Collection of 'UserDTO.Guid' this user is subcribed to
        /// </summary>
        public ISet<Guid> SubscribedTo { get; set; }

        /// <summary>
        /// Collection of 'PostDTO.Guid' this user likes
        /// </summary>
        public ISet<Guid> Likes { get; set; }

        
    }
}
