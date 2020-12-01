using Microsoft.EntityFrameworkCore;
using System;

namespace TravelCompany.DBLayer.PostgreSQL
{
    public class PostgreSQLDbContext : BaseDBContext
    {
        private readonly string _connectionString;

        public PostgreSQLDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}
