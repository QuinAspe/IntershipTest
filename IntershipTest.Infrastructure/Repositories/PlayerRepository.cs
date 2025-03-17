using IntershipTest.Core.Entities;
using IntershipTest.Core.Interfaces.Repositories;
using IntershipTest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTest.Infrastructure.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(IDbContextFactory<ApplicationDbContext> factory, ILogger<IBaseRepository<Player>> logger) : base(factory, logger)
        {
        }
        public override IQueryable<Player> GetAll()
        {
            return _table.Include(t => t.Team).AsQueryable();
        }
        public async override Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _table.Include(t => t.Team).ToListAsync();
        }
        public async override Task<Player> GetByIdAsync(int id)
        {
            return await _table.Include(t => t.Team).FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
