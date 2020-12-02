using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TravelCompany.Model;
using TravelCompany.Repository.Repositories;

namespace TravelCompany.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;

        private IRepository<TravelAgency> _travelAgencyRepository;
        private IRepository<Agent> _agentRepository;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.OpenConnection();
        }

        public IRepository<TravelAgency> TravelAgencyRepository
        {
            get
            {
                return _travelAgencyRepository ?? (_travelAgencyRepository = GetRepositoryOfType<TravelAgency>());
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

        public void BeginTransaction() => _dbContext.Database.BeginTransaction();

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
