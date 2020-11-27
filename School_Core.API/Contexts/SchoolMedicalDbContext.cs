using System;
using Microsoft.EntityFrameworkCore;
using School_Core.API.Models;

namespace School_Core.API.Contexts
{
    public class SchoolMedicalDbContext : DbContext
    {
        public DbSet<Medical> Medicals { get; set; }

        public SchoolMedicalDbContext(DbContextOptions<SchoolMedicalDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var studentId = Guid.NewGuid();
            modelBuilder.Entity<Medical>().HasData(
                new Medical(Guid.NewGuid(), Guid.NewGuid(), "DummyReason1"),
                new Medical(Guid.NewGuid(), studentId, "DummyReason2"),
                new Medical(Guid.NewGuid(), studentId, "DummyReason3")
            );
        }
    }
}