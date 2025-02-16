using MediatR;

namespace School.Application.Handlers.Applies.Commands.UpdateApply
{
    public class UpdateApplyCommand : IRequest
    {
        public int Id { get; set; }
        public string StudentGuid { get; set; }
        public string CoachGuid { get; set; }

        public int CourseId { get; set; }
    }
}
