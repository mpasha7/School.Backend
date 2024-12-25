using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Courses.Queries.GetCourseList;
using School.Application.Interfaces;
using School.Application.Interfaces.Repository;
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
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public GetLessonDetailsQueryHandler(ILessonRepository lessonRepository, IMapper mapper)
        {
            this._lessonRepository = lessonRepository;
            this._mapper = mapper;
        }

        public async Task<LessonDetailsVm> Handle(GetLessonDetailsQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken, includeProperty: "Course");

            if (lesson == null)
                throw new NotFoundException(nameof(Lesson), request.Id);
            else if (lesson.Course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (lesson.CourseId != request.CourseId)
                throw new NotContainsException(nameof(Course), request.CourseId, nameof(Lesson), request.Id);
            else if (lesson.Course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Lesson), request.Id);

            var lessonDto = _mapper.Map<LessonDetailsVm>(lesson);
            lessonDto.Course = _mapper.Map<CourseLookupDto>(lesson.Course);
            return lessonDto;
        }
    }
}
