using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification;
using TicketBox.Persistance.Context;
using TicketBox.Persistance.Specifications;

namespace TicketBox.Persistance.Repositories
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        private readonly TicketContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(TicketContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task<List<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec).ToListAsync();
        }

        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec).FirstOrDefaultAsync();
        }
    }
}