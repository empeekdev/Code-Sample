using TravelCompany.Model;
using TravelCompany.Repository.Repositories;

namespace TravelCompany.Repository
{
    public interface IUnitOfWork
    {
        IRepository<TravelAgency> TravelAgencyRepository { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        void SaveChanges();
    }
}
