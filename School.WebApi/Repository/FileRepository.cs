using School.Application.Interfaces.Repository;
using School.Domain;
using School.Persistence;

namespace School.WebApi.Repository
{
    public class FileRepository : GenericRepository<FileObject>, IFileRepository
    {
        public FileRepository(SchoolDbContext context) : base(context)
        {
        }
    }
}
