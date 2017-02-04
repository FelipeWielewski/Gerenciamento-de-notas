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
        public DbSet<City> Cities { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamConfig> ExamConfigs { get; set; }
        public DbSet<Student> Student { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"DataProvider: sqlserver;DataConnectionString: Data Source=neomodeatividadedb.database.windows.net;Initial Catalog=neomodeAtividadeDB;Integrated Security=False;Persist Security Info=False;User ID=felipew;Password=apolec,109");
        }
    }
}
