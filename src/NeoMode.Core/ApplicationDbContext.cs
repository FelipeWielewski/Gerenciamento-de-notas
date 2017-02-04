using Microsoft.EntityFrameworkCore;
using NeoMode.Core.Domain;
using NeoMode.Core.Domain.ExamConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoMode.Core
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<City> City { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<ExamConfig> ExamConfig { get; set; }
        public DbSet<Student> Student { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new System.Data.SqlClient.SqlConnectionStringBuilder
            {
                InitialCatalog = "neomodeAtividadeDB",
                DataSource = "neomodeatividadedb.database.windows.net",
                IntegratedSecurity = false,
                UserID = "felipew",
                Password = "apolec,109",               // hide the password

            }.ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
