using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using TravelCompany.DataAccess;
using TravelCompany.Repository.Repositories;

namespace TravelCompany.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;

        private IRepository<Agency> _agencyRepository;
        private IRepository<Agent> _agentRepository;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.OpenConnection();
        }

        public IRepository<Agency> AgencyRepository
        {
            get
            {
                return _agencyRepository ?? (_agencyRepository = GetRepositoryOfType<Agency>());
            }
        }

        public IRepository<Agent> AgentRepository
        {
            get
            {
                return _agentRepository ?? (_agentRepository = GetRepositoryOfType<Agent>());
            }
        }

        public IRepository<T> GetRepositoryOfType<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public IDbContextTransaction BeginTransaction() => _dbContext.Database.BeginTransaction();

        public void Commit() => _dbContext.Database.CommitTransaction();

        public void Rollback() => _dbContext.Database.RollbackTransaction();

        public void SaveChanges() => _dbContext.SaveChanges();


        private volatile bool _disposed;
        public void Dispose()
        {
            if (!_disposed)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
