using Microsoft.EntityFrameworkCore;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Persistence
{
    public static class DataManager
    {
        public static ModelBuilder SeedDatabase(this ModelBuilder builder)
        {
            string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
            string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";
            string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
            string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

            string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";
            string lorem1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            string lorem2 = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem";
            string videoLink = @"https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea";


            builder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    CoachGuid = irinaId,
                    CreatedDate = DateTime.Now,
                    Title = "Гимнастика на шею",
                    Description = lorem1,
                    ShortDescription = lorem,
                    PublicDescription = lorem1
                },
                new Course
                {
                    Id = 2,
                    CoachGuid = olgaId,
                    CreatedDate = DateTime.Now,
                    Title = "Йога кундалини",
                    Description = lorem2,
                    ShortDescription = lorem,
                    PublicDescription = lorem2
                },
                new Course
                {
                    Id = 3,
                    CoachGuid = irinaId,
                    CreatedDate = DateTime.Now,
                    Title = "Гимнастика на стопы",
                    Description = lorem1,
                    ShortDescription = lorem,
                    PublicDescription = lorem2
                },
                new Course
                {
                    Id = 4,
                    CoachGuid = irinaId,
                    CreatedDate = DateTime.Now,
                    Title = "Нейрогимнастика",
                    Description = lorem2,
                    ShortDescription = lorem,
                    PublicDescription = lorem1
                }
            );

            builder.Entity<FileObject>().HasData(
                new FileObject
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    UniqueFileName = "Гимнастика на шею.jpg",
                    FileNameExt = ".jpg",
                    FileSize = 1000,
                    FileType = FileTypes.Photo,
                    FileOwner = FileOwners.Course,
                    CourseId = 1
                },
                new FileObject
                {
                    Id = 2,
                    CreatedAt = DateTime.Now,
                    UniqueFileName = "Йога кундалини.jpg",
                    FileNameExt = ".jpg",
                    FileSize = 1000,
                    FileType = FileTypes.Photo,
                    FileOwner = FileOwners.Course,
                    CourseId = 2
                },
                new FileObject
                {
                    Id = 3,
                    CreatedAt = DateTime.Now,
                    UniqueFileName = "Гимнастика на стопы.jpg",
                    FileNameExt = ".jpg",
                    FileSize = 1000,
                    FileType = FileTypes.Photo,
                    FileOwner = FileOwners.Course,
                    CourseId = 3
                },
                new FileObject 
                { 
                    Id = 4, 
                    CreatedAt = DateTime.Now, 
                    UniqueFileName = "Нейрогимнастика.jpg",
                    FileNameExt = ".jpg",
                    FileSize = 1000,
                    FileType = FileTypes.Photo,
                    FileOwner = FileOwners.Course,
                    CourseId = 4
                }
            );

            builder.Entity<Lesson>().HasData(
                new Lesson { Id = 1, Number = 1, Title = "Первый урок", Description = lorem1, VideoLink = videoLink, CourseId = 1 },
                new Lesson { Id = 2, Number = 2, Title = "Второй урок", Description = lorem1, VideoLink = videoLink, CourseId = 1 },
                new Lesson { Id = 3, Number = 3, Title = "Третий урок", Description = lorem1, VideoLink = videoLink, CourseId = 1 },
                new Lesson { Id = 4, Number = 1, Title = "Первый урок", Description = lorem1, VideoLink = videoLink, CourseId = 2 },
                new Lesson { Id = 5, Number = 2, Title = "Второй урок", Description = lorem1, VideoLink = videoLink, CourseId = 2 },
                new Lesson { Id = 6, Number = 3, Title = "Третий урок", Description = lorem1, VideoLink = videoLink, CourseId = 2 },
                new Lesson { Id = 7, Number = 1, Title = "Первый урок", Description = lorem1, VideoLink = videoLink, CourseId = 3 },
                new Lesson { Id = 8, Number = 2, Title = "Второй урок", Description = lorem1, VideoLink = videoLink, CourseId = 3 },
                new Lesson { Id = 9, Number = 3, Title = "Третий урок", Description = lorem1, VideoLink = videoLink, CourseId = 3 },
                new Lesson { Id = 10, Number = 1, Title = "Первый урок", Description = lorem1, VideoLink = videoLink, CourseId = 4 },
                new Lesson { Id = 11, Number = 2, Title = "Второй урок", Description = lorem1, VideoLink = videoLink, CourseId = 4 },
                new Lesson { Id = 12, Number = 3, Title = "Третий урок", Description = lorem1, VideoLink = videoLink, CourseId = 4 }
            );

            builder.Entity<StudentOfCourse>().HasData(
                new StudentOfCourse { Id = 1, StudentGuid = tomId, CourseId = 2 },
                new StudentOfCourse { Id = 2, StudentGuid = tomId, CourseId = 3 },
                new StudentOfCourse { Id = 3, StudentGuid = tomId, CourseId = 4 },
                new StudentOfCourse { Id = 4, StudentGuid = alexId, CourseId = 3 }
            );

            return builder;
        }
    }
}
