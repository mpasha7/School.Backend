using AutoMapper;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Feedbacks.Queries.GetFeedbackDetails
{
    public class GetFeedbackDetailsQueryHandler : IRequestHandler<GetFeedbackDetailsQuery, FeedbackDetailsVm>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetFeedbackDetailsQueryHandler(
            ILessonRepository lessonRepository,
            IReportRepository reportRepository,
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            this._lessonRepository = lessonRepository;
            this._reportRepository = reportRepository;
            this._studentRepository = studentRepository;
            this._mapper = mapper;
        }

        public async Task<FeedbackDetailsVm> Handle(GetFeedbackDetailsQuery request, CancellationToken cancellationToken)
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
            else if (!(await _studentRepository.IsStudentOfThisCourse(
                request.StudentGuid,
                lesson.CourseId,
                cancellationToken)))
                throw new NoAccessException(nameof(Course), lesson.CourseId);

            var report = await _reportRepository.GetByIdAsync(
                request.ReportId,
                cancellationToken,
                includeReference: "Feedback");

            if (report == null)
                throw new NotFoundException(nameof(Report), request.ReportId);
            else if (report.Feedback == null)
                throw new NotFoundException(nameof(Feedback), 0);

            return _mapper.Map<FeedbackDetailsVm>(report.Feedback);
        }
    }
}
