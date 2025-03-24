using Microsoft.EntityFrameworkCore;
using MVC_Day06.Models;

namespace MVC_Day06.DbContexts
{
    public class AcademyDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StuCrsRes> StuCrsRes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Academy;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(d =>
            {
                d.HasKey(d => d.Id);
                d.Property(d => d.Id).UseIdentityColumn();
                d.Property(d => d.Name).IsRequired().HasMaxLength(100);
                d.Property(d => d.MgrName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Student>(s =>
            {
                s.HasKey(s => s.Id);
                s.Property(s => s.Id)
                 .UseIdentityColumn();

                s.Property(s => s.Name).IsRequired().HasMaxLength(100);
                s.Property(s => s.Age).IsRequired();

                // Student-Department Relationship (One-to-Many)
                s.HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId);
            });

            modelBuilder.Entity<Course>(c =>
            {
                c.HasKey(c => c.Id);
                c.Property(s => s.Id)
                 .UseIdentityColumn();
                c.Property(c => c.Name).IsRequired().HasMaxLength(130);
                c.Property(c => c.Degree).IsRequired().HasColumnType("nvarchar(50)");
                c.Property(c => c.MinDegree).IsRequired().HasColumnType("nvarchar(50)");

                // Course-Department Relationship (One-to-Many)
                c.HasOne(c => c.Department)
                 .WithMany(d => d.Courses)
                 .HasForeignKey( c =>  c.DepartmentId);
            });

            modelBuilder.Entity<Teacher>(t =>
            {
                t.HasKey(t => t.Id);
                t.Property(s => s.Id)
                 .UseIdentityColumn();
                t.Property(t => t.Name).IsRequired().HasMaxLength(100);
                t.Property(t => t.Salary).HasColumnType("decimal(18,2)").IsRequired();

                t.Property(t => t.Address)
                 .HasMaxLength(200);

                // Teacher-Department Relationship (One-to-Many)
                t.HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

                // Teacher-Course Relationship (One-to-Many)
                t.HasOne(t => t.Course)
                .WithMany(c => c.Teachers)
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<StuCrsRes>(sc =>
            {
                sc.HasKey(sc => new {sc.StudentId, sc.CourseId});
                sc.Property(sc =>  sc.Grade).IsRequired().HasColumnType("decimal(5,2)");

                // Student-Course Relationship (Many-to-Many via StuCrsRes)
                sc.HasOne(sc => sc.Student)
                .WithMany(s => s.StuCrsRes)
                .HasForeignKey(sc => sc.StudentId);

                sc.HasOne(sc => sc.Course)
                .WithMany(c => c.StuCrsRes)
                .HasForeignKey(sc => sc.CourseId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
