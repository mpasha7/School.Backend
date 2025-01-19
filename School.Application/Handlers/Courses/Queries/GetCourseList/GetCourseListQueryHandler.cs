using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using School.Application.Interfaces.Repository;
using School.Domain;
using System.Linq.Expressions;


namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, CourseListVm>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetCourseListQueryHandler(ICourseRepository repository, IMapper mapper)
        {
            this._courseRepository = repository;
            this._mapper = mapper;
        }

        public async Task<CourseListVm> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Course, bool>> queryFilter;
            switch (request.UserRole)
            {
                case UserRoles.Admin:
                    throw new ArgumentNullException(nameof(request.UserRole));
                case UserRoles.Coach:
                    queryFilter = c => c.CoachGuid == request.UserGuid;
                    break;
                case UserRoles.Student:
                    queryFilter = c => c.Students.Where(s => s.StudentGuid == request.UserGuid).Any();
                    break;
                default:
                    throw new ArgumentNullException(nameof(request.UserRole));
            }

            var courses = (await _courseRepository.GetAllAsync(
                cancellationToken,
                filter: queryFilter,
                orderBy: x => x.OrderByDescending(c => c.CreatedDate),
                includeProperties: "Photo"
                ))
                .AsQueryable()
                .ProjectTo<CourseLookupDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new CourseListVm { Courses = courses };
        }
    }
}
