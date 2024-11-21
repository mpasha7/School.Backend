using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class GetCourseListQueryHadler : IRequestHandler<GetCourseListQuery, CourseListVm>
    {
        private readonly ISchoolDbContext context;
        private readonly IMapper mapper;

        public GetCourseListQueryHadler(ISchoolDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CourseListVm> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses
                .Where(c => c.CoachGuid == request.CoachGuid)
                .ProjectTo<CourseLookupDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CourseListVm { Courses = courses };
        }
    }
}
