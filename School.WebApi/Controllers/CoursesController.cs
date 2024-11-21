using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Courses.Commands.CreateCourse;
using School.Application.Handlers.Courses.Commands.DeleteCourse;
using School.Application.Handlers.Courses.Commands.UpdateCourse;
using School.Application.Handlers.Courses.Queries.GetCourseDetails;
using School.Application.Handlers.Courses.Queries.GetCourseList;
using School.WebApi.Models.Course;

namespace School.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : BaseController
    {
        private readonly IMapper _mapper;

        public CoursesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of courses
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /courses
        /// </remarks>
        /// <returns>Returns CourseListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CourseListVm>> GetCoursesList()
        {
            var query = new GetCourseListQuery
            {
                CoachGuid = UserGuid
            };
            var vm = await Mediator!.Send(query);   // TODO: Нужен ли "!" ?
            return Ok(vm);
        }

        /// <summary>
        /// Gets the course by id
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// GET /courses/5
        /// </remarks>
        /// <param name="id">Course id (int)</param>
        /// <returns>Returns CourseDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpGet("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CourseDetailsVm>> GetCourse(int id)
        {
            var query = new GetCourseDetailsQuery
            {
                Id = id,
                CoachGuid = UserGuid
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /courses
        /// {
        ///     Title: "course title",
        ///     Description: "course description",
        ///     ShortDescription: "short course description",
        ///     PublicDescription: "public course description",
        ///     PhotoPath: "course photo path",
        ///     BeginQuestionnaire: "begin course questionnaire",
        ///     EndQuestionnaire: "end course questionnaire"
        /// }
        /// </remarks>
        /// <param name="createCourseDto">CreateCourseDto object</param>
        /// <returns>Returns course id (int)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpPost]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> CreateCourse([FromBody] CreateCourseDto createCourseDto)
        {
            var command = _mapper.Map<CreateCourseCommand>(createCourseDto);
            command.CoachGuid = UserGuid;
            var courseId = await Mediator!.Send(command);
            return Ok(courseId);
        }

        /// <summary>
        /// Updates the course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /courses
        /// {
        ///     Description: "updated course description",
        ///     PhotoPath: "updated course photo path"
        /// }
        /// </remarks>
        /// <param name="updateCourseDto">UpdateCourseDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpPut]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDto updateCourseDto) // TODO: Id in Sample request
        {
            var command = _mapper.Map<UpdateCourseCommand>(updateCourseDto);
            command.CoachGuid = UserGuid;
            await Mediator!.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the course by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /courses/5
        /// </remarks>
        /// <param name="id">Course id (int)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpDelete("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var command = new DeleteCourseCommand
            {
                Id = id,
                CoachGuid = UserGuid
            };
            await Mediator!.Send(command);
            return NoContent();
        }
    }
}
