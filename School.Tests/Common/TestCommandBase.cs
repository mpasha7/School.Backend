using School.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly SchoolDbContext Context;

        protected TestCommandBase()
        {
            Context = CoursesContextFactory.Create();
        }

        public void Dispose()
        {
            CoursesContextFactory.Destroy(Context);
        }
    }
}
