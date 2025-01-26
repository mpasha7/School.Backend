using MediatR;
using School.Application.Handlers.Students.Queries.GetStudentIds;

namespace School.Application.Handlers.Students.Queries.GetStudentsIds
{
    public class GetStudentsIdsQuery : IRequest<StudentsIdsVm>
    {
        public string? CoachGuid { get; set; }
    }
}
