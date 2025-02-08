using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Application.Interfaces.Services;
using School.Domain;

namespace School.Application.Handlers.Reports.Commands.CreateReport
{
    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, int>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IFileService _fileService;

        public CreateReportCommandHandler(
            IReportRepository reportRepository,
            IStudentRepository studentRepository,
            ILessonRepository lessonRepository,
            IFileService fileService)
        {
            this._reportRepository = reportRepository;
            this._studentRepository = studentRepository;
            this._lessonRepository = lessonRepository;
            this._fileService = fileService;
        }

        public async Task<int> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(
                request.LessonId,
                cancellationToken,
                includeReference: "Course");

            if (lesson == null)
                throw new NotFoundException(nameof(Lesson), request.LessonId);
            else if (lesson.Course == null)
                throw new NotFoundException(nameof(Course), lesson.CourseId);
            else if (lesson.CourseId != request.CourseId)
                throw new NotContainsException(nameof(Course), request.CourseId, nameof(Lesson), request.LessonId);
            else if (!(await _studentRepository.IsStudentOfThisCourse(
                request.StudentGuid,
                lesson.CourseId,
                cancellationToken)))
                throw new NoAccessException(nameof(Course), lesson.CourseId);

            var report = new Report
            {
                StudentGuid = request.StudentGuid,
                StudentName = request.StudentName,
                CreatedAt = DateTime.Now,
                Text = request.Text,
                LessonId = request.LessonId
            };

            await _reportRepository.AddAsync(report, cancellationToken);

            if (request.FormFiles != null)
            {
                foreach (var formFile in request.FormFiles)
                {
                    await _fileService.SaveFileAsync(formFile, FileTypes.Photo, FileOwners.Report, report.Id, cancellationToken);
                }
            }

            return report.Id;
        }
    }
}
