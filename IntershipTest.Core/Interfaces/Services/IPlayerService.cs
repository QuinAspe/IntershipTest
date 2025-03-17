using IntershipTest.Core.Entities;
using IntershipTest.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTest.Core.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<ResultModel<IEnumerable<Player>>> GetAllAsync();
        Task<ResultModel<Player>> GetByIdAsync(int id);
        Task<ResultModel<Player>> AddAsync(PlayerAddRequestModel playerAddRequestModel);
        Task<ResultModel<Player>> UpdateAsync(PlayerUpdateRequestModel playerUpdateRequestModel);
        Task<ResultModel<Player>> DeleteAsync(int id);
    }
}
