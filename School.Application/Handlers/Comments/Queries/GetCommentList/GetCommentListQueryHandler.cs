using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Comments.Queries.GetCommentList
{
    public class GetCommentListQueryHandler : IRequestHandler<GetCommentListQuery, CommentListVm>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetCommentListQueryHandler(
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            this._courseRepository = courseRepository;
            this._mapper = mapper;
        }

        public async Task<CommentListVm> Handle(GetCommentListQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(
                request.CourseId,
                cancellationToken,
                includeCollection: "Comments");

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.CourseId);

            return new CommentListVm
            {
                Comments = course.Comments
                                 .AsQueryable()
                                 .OrderByDescending(c => c.CreatedAt)
                                 .ProjectTo<CommentLookupDto>(_mapper.ConfigurationProvider)
                                 .ToList()
            };
        }
    }
}
