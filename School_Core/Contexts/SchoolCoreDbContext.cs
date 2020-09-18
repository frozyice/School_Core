using Microsoft.EntityFrameworkCore;
using School_Core.Domain.Models;
using School_Core.Domain.Models.Students;

namespace School_Core.Contexts
{
    public class SchoolCoreDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        //public SchoolCoreDbContext(string connectionString) 
        //{
        //    //Database.EnsureCreated();
        //    _connectionString = connectionString;
        //}

        public SchoolCoreDbContext(DbContextOptions<SchoolCoreDbContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelbuilderExtentions.Seed(modelBuilder);
        }
    }
}