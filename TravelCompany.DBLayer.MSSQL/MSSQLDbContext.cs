using Microsoft.EntityFrameworkCore;

namespace TravelCompany.DBLayer.MSSQL
{
    public class MSSQLDbContext : BaseDBContext
    {
        public MSSQLDbContext(DbContextOptions option) : base(option)
        {

        }
    }
}
