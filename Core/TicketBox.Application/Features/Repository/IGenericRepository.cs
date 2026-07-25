using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Specification;

namespace TicketBox.Application.Features.Repository
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task<T?> GetByIdAsync(TKey id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<List<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);
    }
}
