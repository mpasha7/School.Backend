using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Lessons.Commands.CreateLesson;
using School.Application.Handlers.Lessons.Commands.DeleteLesson;
using School.Application.Handlers.Lessons.Commands.UpdateLesson;
using School.Application.Handlers.Lessons.Queries.GetLessonDetails;
using School.Application.Handlers.Lessons.Queries.GetLessonList;
using School.WebApi.Models.Lesson;

namespace School.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
    public class LessonsController : BaseController
    {
        private readonly IMapper _mapper;

        public LessonsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of lessons
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /lessons?courseid=5
        /// </remarks>
        /// <returns>Returns LessonListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet(Name = nameof(GetLessonsList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LessonListVm>> GetLessonsList([FromQuery] int courseid)
        {
            var query = new GetLessonListQuery
            {
                CourseId = courseid,
                CoachGuid = UserGuid
            };
            var vm = await Mediator!.Send(query);   // TODO: Нужен ли "!" ?
            return Ok(vm);
        }

        /// <summary>
        /// Gets the lesson by id
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// GET /lessons/2?courseid=5
        /// </remarks>
        /// <param name="id">Lesson id (int)</param>
        /// <param name="courseid">Course id (int)</param>
        /// <returns>Returns LessonDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpGet("{id}", Name = nameof(GetLesson))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LessonDetailsVm>> GetLesson(int id, [FromQuery] int courseid)
        {
            var query = new GetLessonDetailsQuery
            {
                Id = id,
                CourseId = courseid,
                CoachGuid = UserGuid
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the lesson
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /lessons
        /// {
        ///     Number: "lesson number in the course",
        ///     Title: "lesson title",
        ///     Description: "lesson description",
        ///     VideoLink: "link to lesson video"
        /// }
        /// </remarks>
        /// <param name="createLessonDto">CreateLessonDto object</param>
        /// <returns>Returns lesson id (int)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpPost(Name = nameof(CreateLesson))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> CreateLesson([FromBody] CreateLessonDto createLessonDto)
        {
            var command = _mapper.Map<CreateLessonCommand>(createLessonDto);
            command.CoachGuid = UserGuid;
            var lessonId = await Mediator!.Send(command);
            return Ok(lessonId);
        }

        /// <summary>
        /// Updates the lesson
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /lessons
        /// {
        ///     Description: "updated lesson description",
        ///     VideoLink: "updated link to lesson video"
        /// }
        /// </remarks>
        /// <param name="updateLessonDto">UpdateLessonDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpPut(Name = nameof(UpdateLesson))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonDto updateLessonDto) // TODO: Id in Sample request
        {
            var command = _mapper.Map<UpdateLessonCommand>(updateLessonDto);
            command.CoachGuid = UserGuid;
            await Mediator!.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the lesson by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /lessons/2?courseid=5
        /// </remarks>
        /// <param name="id">Lesson id (int)</param>
        /// <param name="courseid">Course id (int)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpDelete("{id}", Name = nameof(DeleteLesson))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteLesson(int id, [FromQuery]int courseid)
        {
            var command = new DeleteLessonCommand
            {
                Id = id,
                CourseId = courseid,
                CoachGuid = UserGuid
            };
            await Mediator!.Send(command);
            return NoContent();
        }
    }
}
