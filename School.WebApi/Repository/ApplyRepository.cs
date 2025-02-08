using School.Application.Interfaces.Repository;
using School.Domain;
using School.Persistence;

namespace School.WebApi.Repository
{
    public class ApplyRepository : GenericRepository<Apply>, IApplyRepository
    {
        public ApplyRepository(SchoolDbContext context) : base(context)
        {
        }
    }
}
