using System;
using System.Collections.Generic;
using System.Text;
using TravelCompany.Model;
using TravelCompany.Repository.Repositories;

namespace TravelCompany.Repository
{
    public interface IUnitOfWork
    {
        IRepository<TravelAgency> TravelAgencyRepository { get; }
    }
}
