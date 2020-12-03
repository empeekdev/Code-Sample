using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Repository.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id);
        Task<T> GetByUUIDAsync(Guid uuid);
        T GetById(long id);
        T GetByUUID(Guid uuid);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Select();
        T Add(T entity);
        void Add(IEnumerable<T> entities);
        T Detach(T entity);
        T SetModified(T entity);
        T Delete(T entityToDelete);
        T Update(T entityToUpdate);
        void Delete(IEnumerable<T> entities);
    }
}
