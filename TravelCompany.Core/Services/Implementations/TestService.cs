using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TravelCompany.Repository;

namespace TravelCompany.Core.Services.Implementations
{
    public class TestService: ITestService
    {        
        private readonly IUnitOfWork _uow;

        public TestService(DbContext dbContext)
        {            
            _uow = new UnitOfWork(dbContext);

            var x =_uow.TravelAgencyRepository.GetAll();
        }

    }
}
