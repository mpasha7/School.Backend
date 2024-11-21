using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace School.Application.Handlers.Lessons.Queries.GetLessonList
{
    public class GetLessonListQueryHandler : IRequestHandler<GetLessonListQuery, LessonListVm>
    {
        private readonly ISchoolDbContext context;
        private readonly IMapper mapper;

        public GetLessonListQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<LessonListVm> Handle(GetLessonListQuery request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == request.CourseId, cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.CourseId);

            var lessons = await context.Lessons
                .Where(les => les.CourseId == request.CourseId)
                .ProjectTo<LessonLookupDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new LessonListVm { Lessons = lessons };
        }
    }
}
