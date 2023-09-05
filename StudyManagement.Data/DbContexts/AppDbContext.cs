using Microsoft.EntityFrameworkCore;
using StudyManagement.Domain.Entities.Grades;
using StudyManagement.Domain.Entities.Sciences;
using StudyManagement.Domain.Entities.Students;
using StudyManagement.Domain.Entities.StudentSciences;
using StudyManagement.Domain.Entities.Teachers;

namespace StudyManagement.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<GradeEntity> Grades { get; set; }
    public DbSet<ScienceEntity> Sciences { get; set; }
    public DbSet<StudentEntity> Students { get; set; }
    public DbSet<StudentScienceEntity> StudentSciences { get; set; }
    public DbSet<TeacherEntity> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Flunt Api

        modelBuilder.Entity<ScienceEntity>()
            .HasOne(s => s.Teacher)
            .WithMany(t => t.Sciences)
            .HasForeignKey(s => s.TeacherId);

        modelBuilder.Entity<StudentScienceEntity>()
            .HasOne(ss => ss.Science)
            .WithMany(s => s.StudentSciences)
            .HasForeignKey(ss => ss.ScienceId);

        modelBuilder.Entity<StudentScienceEntity>()
            .HasOne(ss => ss.Student)
            .WithMany(s => s.StudentSciences)
            .HasForeignKey(ss => ss.StudentId);         

        modelBuilder.Entity<GradeEntity>()
            .HasOne(g=> g.StudentScience)
            .WithOne(ss=> ss.Grade)
            .HasForeignKey<GradeEntity>(g=> g.StudentScienceId)
            .IsRequired();

        #endregion
    }
}
