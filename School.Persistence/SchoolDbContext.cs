using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Domain;
using School.Persistence.EntityTypeConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Persistence
{
    public class SchoolDbContext : DbContext, ISchoolDbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<FileObject> Files { get; set; }
        public DbSet<StudentOfCourse> Students { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Apply> Applies { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());

            base.OnModelCreating(modelBuilder.SeedDatabase());
        }

    }
}
