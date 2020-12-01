using Microsoft.EntityFrameworkCore;
using System;
using TravelCompany.Model;

namespace TravelCompany.DBLayer.MSSQL
{
    public class MSSQLDbContext : BaseDBContext
    {
        private readonly string _connectionString;
        
        public DbSet<TravelAgency> TravelAgencies { get; set; }

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
