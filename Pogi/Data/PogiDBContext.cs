using Microsoft.EntityFrameworkCore;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pogi.Models.AccountViewModels;

namespace Pogi.Data
{
    public class PogiDbContext : DbContext
    {
        public PogiDbContext(DbContextOptions<PogiDbContext> options)
            : base(options)
        {
        }
        public DbSet<Pogi.Entities.Course> Course { get; set; }
        public DbSet<Pogi.Entities.CourseDetail> CourseDetail { get; set; }
        public DbSet<Pogi.Entities.Member> Member { get; set; }
        public DbSet<Pogi.Entities.TeeTime> TeeTime { get; set; }
        public DbSet<Pogi.Entities.TeeAssign> TeeAssign { get; set; }
        public DbSet<Pogi.Entities.Score> Score { get; set; }
        public DbSet<Pogi.Entities.Player> Player { get; set; }
        public DbSet<Pogi.Entities.Handicap> Handicap { get; set; }
        public DbSet<Pogi.Entities.HandicapSchedule> HandicapSchedule { get; set; }
        public DbSet<Pogi.Entities.Tour> Tour { get; set; }
        public DbSet<Pogi.Entities.TourDay> TourDay { get; set; }
        public DbSet<Pogi.Entities.Log2> Log2 { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseDetail>()
            .HasKey(c => new { c.CourseId, c.Color });
            modelBuilder.Entity<TeeAssign>().HasIndex(
            c => new { c.TeeTimeId, c.MemberId }).IsUnique(false);
            modelBuilder.Entity<Member>().HasIndex(
            c => new { c.EmailAddr1st }).IsUnique(true);
        }
    }
}
