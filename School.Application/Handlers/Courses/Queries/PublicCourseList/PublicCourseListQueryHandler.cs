using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using School.Application.Interfaces.Repository;

namespace School.Application.Handlers.Courses.Queries.PublicCourseList
{
    public class PublicCourseListQueryHandler : IRequestHandler<PublicCourseListQuery, PublicCourseListVm>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public PublicCourseListQueryHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<PublicCourseListVm> Handle(PublicCourseListQuery request, CancellationToken cancellationToken)
        {
            var courses = (await _courseRepository.GetAllAsync(
                cancellationToken,
                orderBy: x => x.OrderByDescending(c => c.CreatedDate),
                includeProperties: "Photo"
                ))
                .AsQueryable()
                .ProjectTo<PublicCourseLookupDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new PublicCourseListVm { Courses = courses };
        }
    }
}
