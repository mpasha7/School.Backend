using Microsoft.EntityFrameworkCore;
using School.Domain;
using School.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Common
{
    public class CoursesContextFactory
    {
        private static int counter = 0;

        // TODO: Расширить тест-кейсы
        // TODO: Добавить объекты Users (верный / не верный)
        // TODO: Для Lessons отдельный Factory?
        public static string UserAGuid = Guid.NewGuid().ToString();
        public static string UserBGuid = Guid.NewGuid().ToString();

        public static int CourseIdForUpdate = 3;
        public static int CourseIdForDelete = 4;

        public static SchoolDbContext Create()
        {
            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new SchoolDbContext(options);
            context.Database.EnsureCreated();
            context.Courses.AddRange(
                new Course
                {
                    Id = 1,
                    CoachGuid = UserAGuid,
                    CreatedDate = DateTime.Today,
                    Title = "Title 1",
                    Description = "Description 1",
                    ShortDescription = "ShortDescription 1",
                    PublicDescription = "PublicDescription 1",
                    PhotoPath = "PhotoPath 1",
                    BeginQuestionnaire = "BeginQuestionnaire 1",
                    EndQuestionnaire = "EndQuestionnaire 1"
                },
                new Course
                {
                    Id = 2,
                    CoachGuid = UserBGuid,
                    CreatedDate = DateTime.Today,
                    Title = "Title 2",
                    Description = "Description 2",
                    ShortDescription = "ShortDescription 2",
                    PublicDescription = "PublicDescription 2",
                    PhotoPath = "PhotoPath 2",
                    BeginQuestionnaire = "BeginQuestionnaire 2",
                    EndQuestionnaire = "EndQuestionnaire 2"
                },
                new Course
                {
                    Id = CourseIdForDelete,
                    CoachGuid = UserAGuid,
                    CreatedDate = DateTime.Today,
                    Title = "Title 3",
                    Description = "Description 3",
                    ShortDescription = "ShortDescription 3",
                    PublicDescription = "PublicDescription 3",
                    PhotoPath = "PhotoPath 3",
                    BeginQuestionnaire = "BeginQuestionnaire 3",
                    EndQuestionnaire = "EndQuestionnaire 3"
                },
                new Course
                {
                    Id = CourseIdForUpdate,
                    CoachGuid = UserBGuid,
                    CreatedDate = DateTime.Today,
                    Title = "Title 4",
                    Description = "Description 4",
                    ShortDescription = "ShortDescription 4",
                    PublicDescription = "PublicDescription 4",
                    PhotoPath = "PhotoPath 4",
                    BeginQuestionnaire = "BeginQuestionnaire 4",
                    EndQuestionnaire = "EndQuestionnaire 4"
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
