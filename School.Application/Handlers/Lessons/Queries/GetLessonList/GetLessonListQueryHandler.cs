using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Courses.Queries.GetCourseDetails;
using School.Application.Handlers.Courses.Queries.GetCourseList;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Lessons.Queries.GetLessonList
{
    public class GetLessonListQueryHandler : IRequestHandler<GetLessonListQuery, LessonListVm>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetLessonListQueryHandler(
            ILessonRepository lessonRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            this._lessonRepository = lessonRepository;
            this._courseRepository = courseRepository;
            this._mapper = mapper;
        }

        public async Task<LessonListVm> Handle(GetLessonListQuery request, CancellationToken cancellationToken)
        {
            if (request.UserRole != UserRoles.Coach && request.UserRole != UserRoles.Student)
                throw new ArgumentNullException(nameof(request.UserRole));

            var course = await _courseRepository.GetByIdAsync(
                request.CourseId,
                cancellationToken,
                includeCollection: "Students");

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (request.UserRole == UserRoles.Coach 
                && course.CoachGuid != request.UserGuid)
                    throw new NoAccessException(nameof(Course), request.CourseId);
            else if (request.UserRole == UserRoles.Student
                && !course.Students.Where(s => s.StudentGuid == request.UserGuid).Any())
                    throw new NoAccessException(nameof(Course), request.CourseId);

            var lessons = (await _lessonRepository.GetAllAsync(
                cancellationToken,
                filter: les => les.CourseId == request.CourseId,
                orderBy: x => x.OrderBy(les => les.Number)
                ))
                .AsQueryable()
                .ProjectTo<LessonLookupDto>(_mapper.ConfigurationProvider)
                .ToList();

            int maxLessonNumber = lessons.Count > 0 
                ? lessons.Max(les => les.Number)
                : 0;

            return new LessonListVm 
            { 
                Lessons = lessons,
                Course = _mapper.Map<CourseDetailsVm>(course),
                MaxLessonNumber = maxLessonNumber
            };
        }
    }
}
