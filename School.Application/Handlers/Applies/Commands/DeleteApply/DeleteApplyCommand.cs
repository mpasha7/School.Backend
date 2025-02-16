using MediatR;

namespace School.Application.Handlers.Applies.Commands.DeleteApply
{
    public class DeleteApplyCommand : IRequest
    {
        public int Id { get; set; }
        public string CoachGuid { get; set; }

        public int CourseId { get; set; }
    }
}
