using Microsoft.EntityFrameworkCore;
using School_Core.Domain.Models;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Students;
using School_Core.Domain.Models.Teachers;

namespace School_Core.Contexts
{
    public class SchoolCoreDbContext : DbContext
    {

        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public SchoolCoreDbContext(DbContextOptions<SchoolCoreDbContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelbuilderExtentions.Seed(modelBuilder);
        }
    }
}