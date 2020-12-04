using Microsoft.EntityFrameworkCore.Storage;
using TravelCompany.DataAccess;
using TravelCompany.Repository.Repositories;

namespace TravelCompany.Repository
{
    public interface IUnitOfWork
    {
        IRepository<Agency> AgencyRepository { get; }
        IRepository<Agent> AgentRepository { get; }

        IDbContextTransaction BeginTransaction();
        void Commit();
        void Rollback();
        void SaveChanges();
    }
}
