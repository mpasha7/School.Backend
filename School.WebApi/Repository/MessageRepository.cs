using School.Application.Interfaces.Repository;
using School.Domain;
using School.Persistence;

namespace School.WebApi.Repository
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(SchoolDbContext context) : base(context)
        {
        }
    }
}
