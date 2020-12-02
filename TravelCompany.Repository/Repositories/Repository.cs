using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Repository.Repositories
{
	public sealed class Repository<T> : IRepository<T> where T : class
	{
		private readonly DbContext _db;
		
		public Repository(DbContext db)
		{
			_db = db;
		}
		public T GetById(long id)
		{
			return _db.Set<T>().Find(id);
		}

		public async Task<T> GetByIdAsync(long id)
		{
			return await _db.Set<T>().FindAsync(id);
		}

		public IEnumerable<T> GetAll()
		{
			return _db.Set<T>().ToList();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _db.Set<T>().ToListAsync();
		}

		public IQueryable<T> Select()
		{
			return _db.Set<T>();
		}

		public T Add(T entity)
		{
			return _db.Set<T>().Add(entity).Entity;
		}

		public T Detach(T entity)
		{
			_db.Entry(entity).State = EntityState.Detached;
			return entity;
		}

		public T Delete(T entityToDelete)
		{
			if (_db.Entry(entityToDelete).State == EntityState.Detached)
			{
				_db.Set<T>().Attach(entityToDelete);
			}
			return _db.Set<T>().Remove(entityToDelete).Entity;
		}

		public T Update(T entityToUpdate)
		{
			_db.Set<T>().Attach(entityToUpdate);
			_db.Entry(entityToUpdate).State = EntityState.Modified;
			return entityToUpdate;
		}

		public void Delete(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
			{
				Delete(entity);
			}
		}
	}
}
