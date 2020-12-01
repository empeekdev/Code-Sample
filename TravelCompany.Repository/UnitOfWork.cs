using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelCompany.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction() => _dbContext.Database.BeginTransaction();

        public void Commit() => _dbContext.Database.CommitTransaction();

        public void Rollback() => _dbContext.Database.RollbackTransaction();


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
