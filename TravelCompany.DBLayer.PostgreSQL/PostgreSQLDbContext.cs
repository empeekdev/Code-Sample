using Microsoft.EntityFrameworkCore;

namespace TravelCompany.DBLayer.PostgreSQL
{
    public class PostgreSQLDbContext : BaseDBContext
    {
        public PostgreSQLDbContext(DbContextOptions option) : base(option)
        {

        }
    }
}
