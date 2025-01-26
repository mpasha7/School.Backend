using Azure.Core;
using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces.Repository;
using School.Domain;
using School.Persistence;

namespace School.WebApi.Repository
{
    public class StudentRepository : GenericRepository<StudentOfCourse>, IStudentRepository
    {
        private readonly SchoolDbContext context;

        public StudentRepository(SchoolDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> IsStudentOfThisCourse(string studentGuid, int courseId, CancellationToken cancellationToken)
        {
            return await context.Students
                .Where(s => s.StudentGuid == studentGuid && s.CourseId == courseId)
                .AnyAsync(cancellationToken);
        }
    }
}
