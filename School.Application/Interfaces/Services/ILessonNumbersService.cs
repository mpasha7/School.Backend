using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Interfaces.Services
{
    public interface ILessonNumbersService
    {
        Task ShiftNumbersIfCreateLesson(
            int? lessNumber,
            int courseId,
            CancellationToken cancellationToken);

        Task ShiftNumbersIfUpdateLesson(
            int? oldLessNumber,
            int? newLessNumber,
            int lessId,
            int courseId,
            CancellationToken cancellationToken);

        Task ShiftNumbersIfDeleteLesson(
            int? lessNumber,
            int courseId,
            CancellationToken cancellationToken);
    }
}
