using IntershipTest.Core.Entities;
using IntershipTest.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTest.Core.Interfaces.Services
{
    public interface ITeamService
    {
        Task<ResultModel<IEnumerable<Team>>> GetAllAsync();
        Task<ResultModel<Team>> GetByIdAsync(int id);
        Task<ResultModel<Team>> AddAsync(TeamAddRequestModel teamAddRequestModel);
        Task<ResultModel<Team>> UpdateAsync(TeamUpdateRequestModel teamUpdateRequestModel);
        Task<ResultModel<Team>> DeleteAsync(int id);
    }
}
