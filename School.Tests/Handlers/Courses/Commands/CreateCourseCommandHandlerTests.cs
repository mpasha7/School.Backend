using Microsoft.EntityFrameworkCore;
using School.Application.Handlers.Courses.Commands.CreateCourse;
using School.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Handlers.Courses.Commands
{
    public class CreateCourseCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateCourseCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateCourseCommandHandler(Context);
            string title = "course title";
            string desc = "course description";
            string shDesc = "course short description";
            string pubDesc = "course public description";
            string photoPath = "course photo path";
            string beginQuest = "course begin questionaire";
            string endQuest = "course end questionaire";

            // Act
            var courseId = await handler.Handle(
                new CreateCourseCommand
                {
                    CoachGuid = CoursesContextFactory.UserAGuid,

                    Title = title,
                    Description = desc,
                    ShortDescription = shDesc,
                    PublicDescription = pubDesc,
                    PhotoPath = photoPath,
                    BeginQuestionnaire = beginQuest,
                    EndQuestionnaire = endQuest
                },
                CancellationToken.None
            );

            // Assert
            Assert.NotNull(
                await Context.Courses.SingleOrDefaultAsync(
                    c => c.Id == courseId
                      && c.CoachGuid == CoursesContextFactory.UserAGuid
                      && c.Title == title
                      && c.Description == desc
                      && c.ShortDescription == shDesc
                      && c.PublicDescription == pubDesc
                      && c.PhotoPath == photoPath
                      && c.BeginQuestionnaire == beginQuest
                      && c.EndQuestionnaire == endQuest));
        }


    }
}
