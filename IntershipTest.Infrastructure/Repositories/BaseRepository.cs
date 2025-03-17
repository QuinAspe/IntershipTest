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
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger<IBaseRepository<T>> _logger;
        protected readonly DbSet<T> _table;

        public BaseRepository(IDbContextFactory<ApplicationDbContext> factory, ILogger<IBaseRepository<T>> logger)
        {
            _context = factory.CreateDbContext();
            _logger = logger;
            _table = _context.Set<T>();
        }

        public async Task<bool> AddAsync(T toAdd)
        {
            _table.Add(toAdd);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(T toDelete)
        {
            _table.Remove(toDelete);
            return await SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _table.AsQueryable();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            return _table.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> UpdateAsync(T toUpdate)
        {
            _table.Update(toUpdate);
            return await SaveChangesAsync();
        }
        protected async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogError(dbUpdateException.Message);
                return false;
            }
        }
    }
}
