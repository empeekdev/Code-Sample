using Microsoft.EntityFrameworkCore;
using TravelCompany.DataAccess;

namespace TravelCompany.DBLayer
{
    public class BaseDBContext: DbContext
    {
        public DbSet<Agency> Agencies { get; }
        public DbSet<Agent> Agents { get; }        

        public BaseDBContext(DbContextOptions option) : base(option)
        {

        }
    }
}
