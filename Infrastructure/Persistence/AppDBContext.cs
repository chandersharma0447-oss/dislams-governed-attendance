using System;
using DISLAMS_Assignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DISLAMS_Assignment.Infrastructure.Persistence
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        { 
        
        }
        public DbSet<AttendanceRecord> Records => Set<AttendanceRecord>();
        public DbSet<AttendanceVersion> Versions => Set<AttendanceVersion>();
        public DbSet<AuditLog> Audits => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttendanceVersion>()
                .Property(v => v.StudentAttendanceJson)
                .IsRequired();
        }
    }
}
