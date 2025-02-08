using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Applies.Commands.DeleteApply
{
    public class DeleteApplyCommand : IRequest
    {
        public int Id { get; set; }
        public string CoachGuid { get; set; }

        public int CourseId { get; set; }
    }
}
