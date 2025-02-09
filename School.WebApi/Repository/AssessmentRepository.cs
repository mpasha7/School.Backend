using School.Application.Interfaces.Repository;
using School.Domain;
using School.Persistence;

namespace School.WebApi.Repository
{
    public class AssessmentRepository : GenericRepository<Assessment>, IAssessmentRepository
    {
        public AssessmentRepository(SchoolDbContext context) : base(context)
        {
        }
    }
}
