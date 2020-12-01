using Microsoft.EntityFrameworkCore;
using System;

namespace TravelCompany.DBLayer.MSSQL
{
    public class MSSQLDbContext : DbContext
    {
        private readonly string _connectionString;

        public MSSQLDbContext()
        {

        }

        public MSSQLDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
