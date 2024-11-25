using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Courses.Commands.UpdateCourse;
using School.Domain;
using School.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Handlers.Courses.Commands
{
    public class UpdateCourseCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateCourseCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateCourseCommandHandler(Context);
            string title = "new title";
            string desc = "new description";
            string shDesc = "new short description";
            string pubDesc = "new public description";
            string photoPath = "new photo path";
            string beginQuest = "new begin questionaire";
            string endQuest = "new end questionaire";

            // Act
            await handler.Handle(
                new UpdateCourseCommand
                {
                    Id = CoursesContextFactory.CourseIdForUpdate,
                    CoachGuid = CoursesContextFactory.UserBGuid,

                    Title = title,
                    Description = desc,
                    ShortDescription = shDesc,
                    PublicDescription = pubDesc,
                    PhotoPath = photoPath,
                    BeginQuestionnaire = beginQuest,
                    EndQuestionnaire = endQuest
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Courses.SingleOrDefaultAsync(
                c => c.Id == CoursesContextFactory.CourseIdForUpdate
                  && c.CoachGuid == CoursesContextFactory.UserBGuid
                  && c.Title == title
                  && c.Description == desc
                  && c.ShortDescription == shDesc
                  && c.PublicDescription == pubDesc
                  && c.PhotoPath == photoPath
                  && c.BeginQuestionnaire == beginQuest
                  && c.EndQuestionnaire == endQuest));
        }

        [Fact]
        public async Task UpdateCourseCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateCourseCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateCourseCommand
                    {
                        Id = 5,
                        CoachGuid = CoursesContextFactory.UserBGuid,
                        Description = "new description"
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateCourseCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateCourseCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new UpdateCourseCommand
                    {
                        Id = CoursesContextFactory.CourseIdForUpdate,
                        CoachGuid = CoursesContextFactory.UserAGuid,
                        Description = "new description"
                    },
                    CancellationToken.None));
        }
    }
}
