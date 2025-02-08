using School.Domain;

namespace School.Application.Interfaces.Repository
{
    public interface IStudentRepository : IGenericRepository<StudentOfCourse>
    {
        Task<bool> IsStudentOfThisCourse(string studentGuid, int courseId, CancellationToken cancellationToken);
    }
}
