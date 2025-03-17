using IntershipTest.Core.Entities;
using IntershipTest.Core.Interfaces.Repositories;
using IntershipTest.Core.Interfaces.Services;
using IntershipTest.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTest.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public async Task<ResultModel<Team>> AddAsync(TeamAddRequestModel teamAddRequestModel)
        {
            if (string.IsNullOrWhiteSpace(teamAddRequestModel.Name))
            {
                return new ResultModel<Team>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Naam is verplicht" }
                };
            }
            var team = new Team
            {
                Name = teamAddRequestModel.Name
            };
            if (await _teamRepository.AddAsync(team))
            {
                return new ResultModel<Team>
                {
                    IsSuccess = true,
                    Value = team
                };
            }
            return new ResultModel<Team>
            {
                IsSuccess = false,
                Errors = new List<string> { "Kon team niet toevoegen" }
            };
        }

        public async Task<ResultModel<Team>> DeleteAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if(team == null)
            {
                return new ResultModel<Team>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Team niet gevonden" }
                };
            }
            if (await _teamRepository.DeleteAsync(team))
            {
                return new ResultModel<Team>()
                {
                    IsSuccess = true,
                    Value = team
                };
            }
            return new ResultModel<Team>()
            {
                IsSuccess = false,
                Errors = new List<string> { "Kon team niet verwijderen" }
            };
        }

        public async Task<ResultModel<IEnumerable<Team>>> GetAllAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            if(teams.Count() > 0)
            {
                return new ResultModel<IEnumerable<Team>>()
                {
                    IsSuccess = true,
                    Value = teams
                };
            }
            return new ResultModel<IEnumerable<Team>>()
            {
                IsSuccess = false,
                Errors = new List<string> { "Geen teams gevonden" }
            };
        }

        public async Task<ResultModel<Team>> GetByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null)
            {
                return new ResultModel<Team>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Team niet gevonden" }
                };
            }
            return new ResultModel<Team>()
            {
                IsSuccess = true,
                Value = team
            };
        }

        public async Task<ResultModel<Team>> UpdateAsync(TeamUpdateRequestModel teamUpdateRequestModel)
        {
            var team = await _teamRepository.GetByIdAsync(teamUpdateRequestModel.Id);
            if (team == null)
            {
                return new ResultModel<Team>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Team niet gevonden" }
                };
            }
            if (string.IsNullOrWhiteSpace(teamUpdateRequestModel.Name))
            {
                return new ResultModel<Team>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Naam is verplicht" }
                };
            }
            team.Name = teamUpdateRequestModel.Name;
            if (await _teamRepository.UpdateAsync(team))
            {
                return new ResultModel<Team>
                {
                    IsSuccess = true,
                    Value = team
                };
            }
            return new ResultModel<Team>()
            {
                IsSuccess = false,
                Errors = new List<string> { "Kon team niet updaten" }
            };
        }
    }
}
