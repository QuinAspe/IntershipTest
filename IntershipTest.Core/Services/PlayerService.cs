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
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public PlayerService(IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        }

        public async Task<ResultModel<Player>> AddAsync(PlayerAddRequestModel playerAddRequestModel)
        {
            if (playerAddRequestModel.DateOfBirth > DateTime.Now)
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Geboortedatum kan niet in de toekomst liggen" }
                };
            }
            var team = await _teamRepository.GetByIdAsync(playerAddRequestModel.TeamId);
            if (team == null)
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Team niet gevonden" }
                };
            }
            if (string.IsNullOrWhiteSpace(playerAddRequestModel.FirstName))
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Voornaam is verplicht" }
                };
            }
            if (string.IsNullOrWhiteSpace(playerAddRequestModel.LastName))
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Familienaam is verplicht" }
                };
            }
            var player = new Player()
            {
                FirstName = playerAddRequestModel.FirstName,
                LastName = playerAddRequestModel.LastName,
                DateOfBirth = playerAddRequestModel.DateOfBirth,
                TeamId = playerAddRequestModel.TeamId
            };
            if (await _playerRepository.AddAsync(player))
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = true,
                    Value = player
                };
            }
            return new ResultModel<Player>()
            {
                IsSuccess = false,
                Errors = new List<string> { "Kon speler niet toevoegen" }
            };
        }

        public async Task<ResultModel<Player>> DeleteAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null)
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Speler niet gevonden" }
                };
            }
            if(await _playerRepository.DeleteAsync(player))
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = true,
                };
            }
            return new ResultModel<Player>()
            {
                IsSuccess = false,
                Errors = new List<string> { "Kon speler niet verwijderen" }
            };
        }

        public async Task<ResultModel<IEnumerable<Player>>> GetAllAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            if (players.Count() > 0)
            {
                return new ResultModel<IEnumerable<Player>>()
                {
                    IsSuccess = true,
                    Value = players
                };
            }
            return new ResultModel<IEnumerable<Player>>()
            {
                IsSuccess = false,
                Errors = new List<string> { "Geen spelers gevonden" }
            };
        }

        public async Task<ResultModel<Player>> GetByIdAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if(player == null)
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Speler niet gevonden" }
                };
            }
            return new ResultModel<Player>()
            {
                IsSuccess = true,
                Value = player
            };
        }

        public async Task<ResultModel<Player>> UpdateAsync(PlayerUpdateRequestModel playerUpdateRequestModel)
        {
            var player = await _playerRepository.GetByIdAsync(playerUpdateRequestModel.Id);
            if(player == null)
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Speler niet gevonden" }
                };
            }
            if(playerUpdateRequestModel.DateOfBirth > DateTime.Now)
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Geboortedatum kan niet in de toekomst liggen" }
                };
            }
            var team = await _teamRepository.GetByIdAsync(playerUpdateRequestModel.TeamId);
            if (team == null)
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Team niet gevonden" }
                };
            }
            if (string.IsNullOrWhiteSpace(playerUpdateRequestModel.FirstName))
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Voornaam is verplicht" }
                };
            }
            if (string.IsNullOrWhiteSpace(playerUpdateRequestModel.LastName))
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Familienaam is verplicht" }
                };
            }
            player.FirstName = playerUpdateRequestModel.FirstName;
            player.LastName = playerUpdateRequestModel.LastName;
            player.DateOfBirth = playerUpdateRequestModel.DateOfBirth;
            player.TeamId = playerUpdateRequestModel.TeamId;
            if (await _playerRepository.UpdateAsync(player))
            {
                return new ResultModel<Player>()
                {
                    IsSuccess = true,
                    Value = player
                };
            }
            return new ResultModel<Player>()
            {
                IsSuccess = false,
                Errors = new List<string> { "Kon speler niet updaten" }
            };
        }
    }
}
