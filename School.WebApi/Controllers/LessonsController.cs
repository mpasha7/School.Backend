using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Lessons.Commands.CreateLesson;
using School.Application.Handlers.Lessons.Commands.DeleteLesson;
using School.Application.Handlers.Lessons.Commands.UpdateLesson;
using School.Application.Handlers.Lessons.Queries.GetLessonDetails;
using School.Application.Handlers.Lessons.Queries.GetLessonList;
using School.WebApi.Models;
using School.WebApi.Models.Lesson;

namespace School.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet(Name = nameof(GetLessonList))]
        [Authorize(Roles = "Coach,Student")]
        public async Task<ActionResult<LessonListVm>> GetLessonList([FromQuery] int courseid)
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
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet("{id}", Name = nameof(GetLesson))]
        [Authorize(Roles = "Coach,Student")]
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
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpPost(Name = nameof(CreateLesson))]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ResponseDto>> CreateLesson([FromBody] CreateLessonDto createLessonDto)
        {
            var command = _mapper.Map<CreateLessonCommand>(createLessonDto);
            command.CoachGuid = UserGuid;
            var lessonId = await Mediator!.Send(command);
            var response = new ResponseDto();
            return Ok(response.Success($"Create new Lesson (id = {lessonId}) is successful"));
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
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpPut(Name = nameof(UpdateLesson))]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonDto updateLessonDto) // TODO: Id in Sample request
        {
            var command = _mapper.Map<UpdateLessonCommand>(updateLessonDto);
            command.CoachGuid = UserGuid;
            await Mediator!.Send(command);
            var response = new ResponseDto();
            return Ok(response.Success($"Update Lesson (id = {updateLessonDto.Id}) is successful"));
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
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpDelete("{id}", Name = nameof(DeleteLesson))]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> DeleteLesson(int id, [FromQuery]int courseid)
        {
            var command = new DeleteLessonCommand
            {
                Id = id,
                CourseId = courseid,
                CoachGuid = UserGuid
            };
            await Mediator!.Send(command);
            var response = new ResponseDto();
            return Ok(response.Success($"Delete Lesson (id = {id}) is successful"));
        }
    }
}
