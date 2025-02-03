using MediatR;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Messages.Queries.GetMessageList
{
    public class GetMessageListQuery : IRequest<MessageListVm>
    {
        public int? CourseId { get; set; }
        public string UserGuid { get; set; }
        public UserRoles UserRole { get; set; }
    }
}
