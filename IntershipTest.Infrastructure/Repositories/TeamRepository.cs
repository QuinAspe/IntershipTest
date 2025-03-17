using IntershipTest.Core.Entities;
using IntershipTest.Core.Interfaces.Repositories;
using IntershipTest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTest.Infrastructure.Repositories
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(IDbContextFactory<ApplicationDbContext> factory, ILogger<IBaseRepository<Team>> logger) : base(factory, logger)
        {
        }
        public override IQueryable<Team> GetAll()
        {
            return _table.Include(t => t.Players).AsQueryable();
        }
        public async override Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _table.Include(t => t.Players).ToListAsync();
        }
        public async override Task<Team> GetByIdAsync(int id)
        {
            return await _table.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
