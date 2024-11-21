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

namespace School.Application.Handlers.Lessons.Queries.GetLessonDetails
{
    public class GetLessonDetailsQueryHandler : IRequestHandler<GetLessonDetailsQuery, LessonDetailsVm>
    {
        private readonly ISchoolDbContext context;
        private readonly IMapper mapper;

        public GetLessonDetailsQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<LessonDetailsVm> Handle(GetLessonDetailsQuery request, CancellationToken cancellationToken)
        {
            var lesson = await context.Lessons.Include(les => les.Course).FirstOrDefaultAsync(les => les.Id == request.Id, cancellationToken);

            if (lesson == null)
                throw new NotFoundException(nameof(Lesson), request.Id);
            else if (lesson.Course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (lesson.CourseId != request.CourseId)
                throw new NotContainsException(nameof(Course), request.CourseId, nameof(Lesson), request.Id);
            else if (lesson.Course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Lesson), request.Id);

            return mapper.Map<LessonDetailsVm>(lesson);
        }
    }
}
