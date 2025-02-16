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
        private static string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private static string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";
        private static string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
        private static string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

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
                    StudentGuid = tomId,
                    StudentName = "Том",
                    CreatedAt = DateTime.Today,
                    Text = "Comment Text",
                    IsPublic = true,
                    CourseId = 2
                }
            );
            context.Reports.Add(
                new Report
                {
                    StudentGuid = tomId,
                    StudentName = "Том",
                    CreatedAt = DateTime.Today,
                    Text = "Report Text for 4 Lesson",
                    LessonId = 4
                }
            );
            context.Files.Add(
                new FileObject
                {
                    CreatedAt = DateTime.Today,
                    UniqueFileName = "ReportPhoto.png",
                    FileName = "ReportPhoto.png",
                    FileNameExt = ".png",
                    FileSize = 1000,
                    FileType = FileTypes.Photo,
                    FileOwner = FileOwners.Report,
                    ReportId = 1
                }
            );
            context.Messages.AddRange(
                new Message
                {
                    CourseId = 1,
                    SenderGuid = tomId,
                    SenderName = "Том",
                    RecipientGuid = irinaId,
                    CreatedAt = DateTime.Today,
                    Theme = "Гимнастика на шею",
                    Text = "Question Text",
                    SenredRole = UserRoles.Student,
                    QuestionId = null
                },
                new Message
                {
                    CourseId = 1,
                    SenderGuid = irinaId,
                    SenderName = "Ирина",
                    RecipientGuid = tomId,
                    CreatedAt = DateTime.Today,
                    Theme = "Гимнастика на шею",
                    Text = "Answer Text",
                    SenredRole = UserRoles.Coach,
                    QuestionId = 1
                }
            );
            context.Applies.Add(
                new Apply
                {
                    StudentGuid = tomId,
                    StudentName = "Том",
                    IsAssepted = false,
                    CourseId = 1
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
