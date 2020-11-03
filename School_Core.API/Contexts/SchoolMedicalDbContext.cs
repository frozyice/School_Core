using System;
using Microsoft.EntityFrameworkCore;
using School_Core.API.Models;

namespace School_Core.API.Contexts
{
    public class SchoolMedicalDbContext : DbContext
    {
        public DbSet<SickLeave> SickLeaves { get; set; }

        public SchoolMedicalDbContext(DbContextOptions<SchoolMedicalDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SickLeave>().HasData(
                new SickLeave(Guid.NewGuid(), Guid.NewGuid(), "DummyReason1"),
                new SickLeave(Guid.NewGuid(), Guid.NewGuid(), "DummyReason2"),
                new SickLeave(Guid.NewGuid(),Guid.NewGuid(), "DummyReason3")
            );
        }
    }
}