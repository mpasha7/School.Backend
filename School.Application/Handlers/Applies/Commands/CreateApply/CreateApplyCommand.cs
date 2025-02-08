using MediatR;

namespace School.Application.Handlers.Applies.Commands.CreateApply
{
    public class CreateApplyCommand : IRequest<int>
    {
        public string StudentGuid { get; set; }
        public string StudentName { get; set; }

        public int CourseId { get; set; }
    }
}
