using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using School.Application.Interfaces.Repository;


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
            var courses = (await _courseRepository.GetAllAsync(
                cancellationToken,
                filter: c => c.CoachGuid == request.CoachGuid,
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
