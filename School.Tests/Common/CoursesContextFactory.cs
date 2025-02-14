using Microsoft.EntityFrameworkCore;
using School.Domain;
using School.Persistence;

namespace School.Tests.Common
{
    public class CoursesContextFactory
    {
        public static string TestCoachGuid = Guid.NewGuid().ToString();
        public static string TestStudentGuid = Guid.NewGuid().ToString();

        // Data from DataManager.SeedDatabase() method
        private static string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";

        public static SchoolDbContext Create()
        {
            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new SchoolDbContext(options);
            context.Database.EnsureCreated();
            context.Comments.Add(
                new Comment
                {
                    //Id = 1,
                    StudentGuid = tomId,
                    StudentName = "Том",
                    CreatedAt = DateTime.Today,
                    Text = "Comment Text",
                    IsPublic = true,
                    CourseId = 2
                }
            );
            context.SaveChanges();

            return context;
        }

        public static void Destroy(SchoolDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
