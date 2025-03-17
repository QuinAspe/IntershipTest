using IntershipTest.Core.Entities;
using IntershipTest.Core.Services.Models;

namespace IntershipTest.Web.Mapping
{
    public static class MappingExtensions
    {
        public static PlayerUpdateRequestModel MapToPlayerUpdateRequestModel(this Player player) 
        {
            return new PlayerUpdateRequestModel
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                DateOfBirth = player.DateOfBirth,
                TeamId = player.TeamId
            };
        }
        public static PlayerAddRequestModel MapToPlayerAddRequestModel(this Player player)
        {
            return new PlayerAddRequestModel
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                DateOfBirth = player.DateOfBirth,
                TeamId = player.TeamId
            };
        }
        public static TeamUpdateRequestModel MapToPlayerUpdateRequestModel(this Team team)
        {
            return new TeamUpdateRequestModel
            {
                Id = team.Id,
                Name = team.Name
            };
        }
        public static TeamAddRequestModel MapToTeamAddRequestModel(this Team team)
        {
            return new TeamAddRequestModel
            {
                Name = team.Name
            };
        }
    }
}
