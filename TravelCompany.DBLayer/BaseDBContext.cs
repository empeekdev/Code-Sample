using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TravelCompany.Model;

namespace TravelCompany.DBLayer
{
    public class BaseDBContext: DbContext
    {
        public DbSet<TravelAgency> TravelAgencies { get; }

        public BaseDBContext() : base()
        {

        }
    }
}
