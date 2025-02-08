using School.Domain;

namespace School.Application.Interfaces.Repository
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task<int> GetMaxLessonNumber(int courseId, CancellationToken cancellationToken);
    }
}
