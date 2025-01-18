using AutoMapper;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Files;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Courses.Queries.PublicCourseDetails
{
    public class PublicCourseDetailsQueryHandler 
        : IRequestHandler<PublicCourseDetailsQuery, PublicCourseDetailsVm>
    {
        private readonly ICourseRepository _coursesRepository;
        private readonly IMapper _mapper;

        public PublicCourseDetailsQueryHandler(ICourseRepository coursesRepository, IMapper mapper)
        {
            _coursesRepository = coursesRepository;
            _mapper = mapper;
        }

        public async Task<PublicCourseDetailsVm> Handle(PublicCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.GetByIdAsync(
                request.Id,
                cancellationToken,
                includeProperty: "Photo");

            if (course == null)
                throw new NotFoundException(nameof(Course), request.Id);

            var vm = _mapper.Map<PublicCourseDetailsVm>(course);
            vm.Photo = _mapper.Map<FileLookupDto>(course.Photo);
            return vm;
        }
    }
}
