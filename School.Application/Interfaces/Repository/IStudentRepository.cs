using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Interfaces.Repository
{
    public interface IStudentRepository : IGenericRepository<StudentOfCourse>
    {
        Task<bool> IsStudentOfThisCourse(string studentGuid, int courseId);
    }
}
