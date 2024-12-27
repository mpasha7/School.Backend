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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
