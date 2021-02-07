using System;
namespace CommunityEngagementApp.Shared.Data
{
    public sealed class CouncilDTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int PrimaryColorHex { get; set; }
        public string SecondaryColorHex { get; set; }
    }
}
