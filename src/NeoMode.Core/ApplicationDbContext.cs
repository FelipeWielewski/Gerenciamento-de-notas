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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<City>().HasKey(x => x.Id);
            modelBuilder.Entity<City>().Property(x => x.Description).HasMaxLength(255);
            modelBuilder.Entity<City>().Property(x => x.Initials).HasMaxLength(20);

            modelBuilder.Entity<Exam>().ToTable("Exam");
            modelBuilder.Entity<Exam>().HasKey(x => x.Id);
            modelBuilder.Entity<Exam>().HasOne(x => x.Student).WithMany().HasForeignKey(X => X.StudentId);

            modelBuilder.Entity<ExamConfig>().ToTable("ExamConfig");
            modelBuilder.Entity<ExamConfig>().HasKey(x => x.Id);

            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Student>().HasKey(x => x.Id);
            modelBuilder.Entity<Student>().HasOne(x => x.City).WithMany().HasForeignKey(X => X.CityId);
        }
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
