using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Files;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Reports.Queries.GetReportDetails
{
    public class GetReportDetailsQueryHandler : IRequestHandler<GetReportDetailsQuery, ReportDetailsVm>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public GetReportDetailsQueryHandler(
            ILessonRepository lessonRepository,
            IReportRepository reportRepository,
            IMapper mapper)
        {
            this._lessonRepository = lessonRepository;
            this._reportRepository = reportRepository;
            this._mapper = mapper;
        }

        public async Task<ReportDetailsVm> Handle(GetReportDetailsQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(
                request.LessonId,
                cancellationToken,
                includeReference: "Course");

            if (lesson == null)
                throw new NotFoundException(nameof(Lesson), request.LessonId);
            else if (lesson.Course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (lesson.CourseId != request.CourseId)
                throw new NotContainsException(nameof(Course), request.CourseId, nameof(Lesson), request.LessonId);
            else if (lesson.Course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Lesson), request.Id);

            var report = await _reportRepository.GetByIdAsync(
                request.Id,
                cancellationToken,
                includeCollection: "Files");

            if (report == null)
                throw new NotFoundException(nameof(Report), request.Id);
            else if (report.LessonId != request.LessonId)
                throw new NotContainsException(nameof(Lesson), request.LessonId, nameof(Report), request.Id);

            var reportVm = _mapper.Map<ReportDetailsVm>(report);
            reportVm.Photos = report.Files
                .AsQueryable()
                .ProjectTo<FileLookupDto>(_mapper.ConfigurationProvider)
                .ToList();

            return reportVm;
        }
    }
}
