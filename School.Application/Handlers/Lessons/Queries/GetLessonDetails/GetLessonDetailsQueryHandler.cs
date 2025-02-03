using AutoMapper;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Courses.Queries.GetCourseList;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Lessons.Queries.GetLessonDetails
{
    public class GetLessonDetailsQueryHandler 
        : IRequestHandler<GetLessonDetailsQuery, LessonDetailsVm>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly Dictionary<string, string> includes = new Dictionary<string, string>()
        {
            { "Coach", "Photo" },
            { "Student", "Photo,Students" }
        };

        public GetLessonDetailsQueryHandler(
            ILessonRepository lessonRepository,
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            this._lessonRepository = lessonRepository;
            this._studentRepository = studentRepository;
            this._mapper = mapper;
        }

        public async Task<LessonDetailsVm> Handle(GetLessonDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.UserRole != UserRoles.Coach && request.UserRole != UserRoles.Student)
                throw new ArgumentNullException(nameof(request.UserRole));

            var lesson = await _lessonRepository.GetByIdAsync(
                request.Id,
                cancellationToken,
                includeReference: "Course"
            );

            if (lesson == null)
                throw new NotFoundException(nameof(Lesson), request.Id);
            else if (lesson.Course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (lesson.CourseId != request.CourseId)
                throw new NotContainsException(nameof(Course), request.CourseId, nameof(Lesson), request.Id);
            else if (request.UserRole == UserRoles.Coach 
                && lesson.Course.CoachGuid != request.UserGuid)
                    throw new NoAccessException(nameof(Lesson), request.Id);
            else if (request.UserRole == UserRoles.Student
                && !(await _studentRepository.IsStudentOfThisCourse(request.UserGuid, lesson.CourseId, cancellationToken)))
                    throw new NoAccessException(nameof(Lesson), request.Id);

            var lessonDto = _mapper.Map<LessonDetailsVm>(lesson);
            lessonDto.Course = _mapper.Map<CourseLookupDto>(lesson.Course);
            return lessonDto;
        }
    }
}
