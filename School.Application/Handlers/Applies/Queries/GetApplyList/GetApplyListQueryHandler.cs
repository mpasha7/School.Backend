using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Applies.Queries.GetApplyList
{
    public class GetApplyListQueryHandler : IRequestHandler<GetApplyListQuery, ApplyListVm>
    {
        private readonly IApplyRepository _applyRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetApplyListQueryHandler(
            IApplyRepository applyRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            this._applyRepository = applyRepository;
            this._courseRepository = courseRepository;
            this._mapper = mapper;
        }

        public async Task<ApplyListVm> Handle(GetApplyListQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(
                request.CourseId,
                cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.CourseId);

            var applies = (await _applyRepository.GetAllAsync(
                cancellationToken,
                filter: a => a.CourseId == request.CourseId && !a.IsAssepted,
                orderBy: x => x.OrderByDescending(a => a.Id)
                ))
                .AsQueryable()
                .ProjectTo<ApplyLookupDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new ApplyListVm { Applies = applies };
        }
    }
}
