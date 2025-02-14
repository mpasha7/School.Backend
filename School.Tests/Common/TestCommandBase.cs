using School.Persistence;

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
