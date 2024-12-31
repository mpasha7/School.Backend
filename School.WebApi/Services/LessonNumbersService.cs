using School.Application.Interfaces.Repository;
using School.Application.Interfaces.Services;
using School.Domain;
using System.Security.Cryptography;

namespace School.WebApi.Services
{
    public class LessonNumbersService : ILessonNumbersService
    {
        private readonly ILessonRepository _repository;

        public LessonNumbersService(ILessonRepository repository)
        {
            _repository = repository;
        }

        public async Task ShiftNumbersIfCreateLesson(
            int? lessNumber,
            int courseId,
            CancellationToken cancellationToken)
        {
            var shiftableLessons = await _repository.GetAllAsync(
                cancellationToken, 
                filter: les => les.CourseId == courseId && les.Number >= lessNumber);

            foreach (var lesson in shiftableLessons)
            {
                lesson.Number++;
                await _repository.UpdateAsync(lesson, cancellationToken);  // TODO: Использовать UnitOfWork
            }
        }

        public async Task ShiftNumbersIfUpdateLesson(
            int? oldLessNumber,
            int? newLessNumber,
            int lessId,
            int courseId,
            CancellationToken cancellationToken)
        {
            if (newLessNumber < oldLessNumber)
            {
                var shiftableLessons = await _repository.GetAllAsync(
                    cancellationToken,
                    filter: les => les.CourseId == courseId && les.Id != lessId && les.Number >= newLessNumber && les.Number < oldLessNumber);

                foreach (var lesson in shiftableLessons)
                {
                    lesson.Number++;
                    await _repository.UpdateAsync(lesson, cancellationToken);
                }
            }
            else if (newLessNumber > oldLessNumber)
            {
                var shiftableLessons = await _repository.GetAllAsync(
                    cancellationToken,
                    filter: les => les.CourseId == courseId && les.Id != lessId && les.Number <= newLessNumber && les.Number > oldLessNumber);

                foreach (var lesson in shiftableLessons)
                {
                    lesson.Number--;
                    await _repository.UpdateAsync(lesson, cancellationToken);
                }
            }
        }

        public async Task ShiftNumbersIfDeleteLesson(
            int? lessNumber,
            int courseId,
            CancellationToken cancellationToken)
        {
            var shiftableLessons = await _repository.GetAllAsync(
                cancellationToken,
                filter: les => les.CourseId == courseId && les.Number > lessNumber);

            foreach (var lesson in shiftableLessons)
            {
                lesson.Number--;
                await _repository.UpdateAsync(lesson, cancellationToken);
            }
        }
    }
}
