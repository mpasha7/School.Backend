using AutoMapper;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Files;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQueryHandler 
        : IRequestHandler<GetCourseDetailsQuery, CourseDetailsVm>
    {
        private readonly ICourseRepository _coursesRepository;
        private readonly IMapper _mapper;

        public GetCourseDetailsQueryHandler(ICourseRepository repository, IMapper mapper)
        {
            this._coursesRepository = repository;
            this._mapper = mapper;
        }

        public async Task<CourseDetailsVm> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            string includeCollection = "";

            switch (request.UserRole)
            {
                case UserRoles.Admin:
                    throw new ArgumentNullException(nameof(request.UserRole));
                case UserRoles.Coach:
                    break;
                case UserRoles.Student:
                    includeCollection = "Students";
                    break;
                default:
                    throw new ArgumentNullException(nameof(request.UserRole));
            }

            var course = await _coursesRepository.GetByIdAsync(
                request.Id,
                cancellationToken,
                includeReference: "Photo",
                includeCollection: includeCollection
            );

            if (course == null)
                throw new NotFoundException(nameof(Course), request.Id);
            else if (request.UserRole == UserRoles.Coach 
                && course.CoachGuid != request.UserGuid)
                    throw new NoAccessException(nameof(Course), request.Id);
            else if (request.UserRole == UserRoles.Student 
                && !course.Students.Where(s => s.StudentGuid == request.UserGuid).Any())
                    throw new NoAccessException(nameof(Course), request.Id);

            var vm = _mapper.Map<CourseDetailsVm>(course);
            vm.Photo = _mapper.Map<FileLookupDto>(course.Photo);
            return vm;
        }
    }
}
