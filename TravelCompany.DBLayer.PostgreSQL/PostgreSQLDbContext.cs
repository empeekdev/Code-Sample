using Microsoft.EntityFrameworkCore;
using System;

namespace TravelCompany.DBLayer.PostgreSQL
{
    public class PostgreSQLDbContext : BaseDBContext
    {
        public PostgreSQLDbContext(DbContextOptions option) : base(option)
        {

        }
    }
}
