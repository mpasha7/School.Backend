using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using School.Application.Handlers.Students.Queries.GetStudentsIds;
using School.Application.Interfaces.Repository;

namespace School.Application.Handlers.Students.Queries.GetStudentIds
{
    public class GetStudentsIdsQueryHandler : IRequestHandler<GetStudentsIdsQuery, StudentsIdsVm>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetStudentsIdsQueryHandler(
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            this._studentRepository = studentRepository;
            this._courseRepository = courseRepository;
            this._mapper = mapper;
        }

        public async Task<StudentsIdsVm> Handle(GetStudentsIdsQuery request, CancellationToken cancellationToken)
        {
            var students = (await _studentRepository.GetAllAsync(
                cancellationToken,
                filter: s => s.Course.CoachGuid == request.CoachGuid,
                includeProperties: "Course"))
                .GroupBy(s => s.StudentGuid);

            var studentDtoList = new List<StudentLookupDto>();
            foreach (var student in students)
            {
                var studentCourses = new List<StudentCourseLookupDto>();
                foreach (var course in student)
                {
                    studentCourses.Add(_mapper.Map<StudentCourseLookupDto>(course.Course));
                }

                studentDtoList.Add(new StudentLookupDto 
                {
                    StudentGuid = student.Key,
                    Courses = studentCourses
                });
            }

            var allCourses = (await _courseRepository.GetAllAsync(
                cancellationToken,
                filter: c => c.CoachGuid == request.CoachGuid))
                .AsQueryable()
                .ProjectTo<StudentCourseLookupDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new StudentsIdsVm { StudentsIds = studentDtoList, AllCourses = allCourses };
        }
    }
}
