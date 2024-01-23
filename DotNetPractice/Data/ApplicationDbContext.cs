using DotNetPractice.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace DotNetPractice.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            MySqlConnectionStringBuilder builder = new()
            {
                Server = "localhost",
                UserID = "root",
                Password = "example",
                Database = "practice"
            };

            optionsBuilder.UseMySQL(builder.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
