using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces.Repository;
using School.Domain;
using School.Persistence;

namespace School.WebApi.Repository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {


        public CourseRepository(SchoolDbContext context) : base(context)
        {

        }
    }
}
