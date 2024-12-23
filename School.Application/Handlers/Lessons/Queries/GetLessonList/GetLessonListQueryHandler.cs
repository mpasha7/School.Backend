using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Interfaces;
using School.Application.Interfaces.Repository;
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
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetLessonListQueryHandler(ILessonRepository lessonRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            this._lessonRepository = lessonRepository;
            this._courseRepository = courseRepository;
            this._mapper = mapper;
        }

        public async Task<LessonListVm> Handle(GetLessonListQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.CourseId);

            var lessons = (await _lessonRepository.GetAllAsync(cancellationToken, filter: les => les.CourseId == request.CourseId))
                .AsQueryable()
                .ProjectTo<LessonLookupDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new LessonListVm { Lessons = lessons };
        }
    }
}
