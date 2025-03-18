using IntershipTest.Core.Entities;
using IntershipTest.Core.Interfaces.Services;
using IntershipTest.Core.Services;

namespace IntershipTest.Web.Services
{
    public class DataService
    {
        private readonly IPlayerService PlayerService;
        private readonly ITeamService TeamService;
        private readonly ErrorHandelingService ErrorHandelingService;

        public DataService(IPlayerService playerService, ITeamService teamService, ErrorHandelingService errorHandelingService)
        {
            PlayerService = playerService;
            TeamService = teamService;
            ErrorHandelingService = errorHandelingService;
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            var result = await PlayerService.GetAllAsync();
            if (!result.IsSuccess)
            {
                ErrorHandelingService.HandleError(result);
                return new List<Player>();
            }
            return result.Value.ToList();
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            var result = await TeamService.GetAllAsync();
            if (!result.IsSuccess)
            {
                ErrorHandelingService.HandleError(result);
                return new List<Team>();
            }
            return result.Value.ToList();
        }
        public async Task<Team> GetTeamAsync(int id)
        {
            var result = await TeamService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                ErrorHandelingService.HandleError(result);
                return new Team();
            }
            return result.Value;
        }
        public async Task<Player> GetPlayerAsync(int id)
        {
            var result = await PlayerService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                ErrorHandelingService.HandleError(result);
                return new Player();
            }
            return result.Value;
        }
    }
}
