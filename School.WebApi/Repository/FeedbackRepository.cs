using School.Application.Interfaces.Repository;
using School.Domain;
using School.Persistence;

namespace School.WebApi.Repository
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(SchoolDbContext context) : base(context)
        {
        }
    }
}
