using SchoolWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
            .HasOne(s => s.Student)
            .WithMany(e => e.Exams)
            .HasForeignKey(s => s.StudentID);

            modelBuilder.Entity<Exam>()
            .HasOne(s => s.Subject)
            .WithMany(e => e.Exams)
            .HasForeignKey(s => s.SubjectID);

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}