using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces.Repository;
using School.Domain;
using School.Persistence;

namespace School.WebApi.Repository
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private readonly SchoolDbContext _context;

        public LessonRepository(SchoolDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<int> GetMaxLessonNumber(int courseId, CancellationToken cancellationToken)
        {
            return await _context.Lessons.MaxAsync(les => les.Number, cancellationToken) ?? 0;
        }
    }
}
