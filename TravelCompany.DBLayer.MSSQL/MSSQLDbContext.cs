using Microsoft.EntityFrameworkCore;
using System;
using TravelCompany.DataAccess;

namespace TravelCompany.DBLayer.MSSQL
{
    public class MSSQLDbContext : BaseDBContext
    {
        public MSSQLDbContext(DbContextOptions option) : base(option)
        {
                     
        }
    }
}
