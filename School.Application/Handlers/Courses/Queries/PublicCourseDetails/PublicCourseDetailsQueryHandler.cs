using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Comments.Queries.GetCommentList;
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
                includeReference: "Photo",
                includeCollection: "Comments");

            if (course == null)
                throw new NotFoundException(nameof(Course), request.Id);

            var vm = _mapper.Map<PublicCourseDetailsVm>(course);
            vm.Photo = _mapper.Map<FileLookupDto>(course.Photo);
            vm.Comments = course.Comments
                                .AsQueryable()
                                .Where(c => c.IsPublic == true)
                                .OrderByDescending(c => c.CreatedAt)
                                .ProjectTo<CommentLookupDto>(_mapper.ConfigurationProvider)
                                .ToList();
            return vm;
        }
    }
}
