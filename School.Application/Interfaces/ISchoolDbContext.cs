using Microsoft.EntityFrameworkCore;
using School.Domain;

namespace School.Application.Interfaces
{
    public interface ISchoolDbContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Lesson> Lessons { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
