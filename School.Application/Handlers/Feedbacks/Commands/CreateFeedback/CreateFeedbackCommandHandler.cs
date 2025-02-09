using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Feedbacks.Commands.CreateFeedback
{
    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, int>
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IReportRepository _reportRepository;
        private readonly ILessonRepository _lessonRepository;

        public CreateFeedbackCommandHandler(
            IFeedbackRepository feedbackRepository,
            IReportRepository reportRepository,
            ILessonRepository lessonRepository)
        {
            this._feedbackRepository = feedbackRepository;
            this._reportRepository = reportRepository;
            this._lessonRepository = lessonRepository;
        }

        public async Task<int> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
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
            else if (lesson.Course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), lesson.CourseId);

            var report = await _reportRepository.GetByIdAsync(
                request.ReportId,
                cancellationToken);

            if (report == null)
                throw new NotFoundException(nameof(Report), request.ReportId);
            else if (report.LessonId != request.LessonId)
                throw new NotContainsException(nameof(Lesson), request.LessonId, nameof(Report), request.ReportId);

            var feedback = new Feedback
            {
                CreatedAt = DateTime.Now,
                Text = request.Text,
                ReportId = request.ReportId
            };

            await _feedbackRepository.AddAsync(feedback, cancellationToken);

            return feedback.Id;
        }
    }
}
