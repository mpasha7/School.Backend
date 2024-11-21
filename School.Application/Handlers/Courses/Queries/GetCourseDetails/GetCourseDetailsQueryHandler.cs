using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Interfaces;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, CourseDetailsVm>
    {
        private readonly ISchoolDbContext context;
        private readonly IMapper mapper;

        public GetCourseDetailsQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CourseDetailsVm> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.Id);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.Id);

            return mapper.Map<CourseDetailsVm>(course);
        }
    }
}
