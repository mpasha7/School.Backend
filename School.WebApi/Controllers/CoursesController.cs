using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Courses.Commands.CreateCourse;
using School.Application.Handlers.Courses.Commands.DeleteCourse;
using School.Application.Handlers.Courses.Commands.UpdateCourse;
using School.Application.Handlers.Courses.Queries.GetCourseDetails;
using School.Application.Handlers.Courses.Queries.GetCourseList;
using School.Domain;
using School.WebApi.Models;
using School.WebApi.Models.Course;

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
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet(Name = nameof(GetCourseList))]
        [Authorize(Roles = "Coach,Student")]
        public async Task<ActionResult<CourseListVm>> GetCourseList()
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
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet("{id}", Name = nameof(GetCourse))]
        [Authorize(Roles = "Coach,Student")]
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
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpPost(Name = nameof(CreateCourse))]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ResponseDto>> CreateCourse([FromForm] CreateCourseDto createCourseDto)
        {
            var command = _mapper.Map<CreateCourseCommand>(createCourseDto);
            command.CoachGuid = UserGuid;
            if (HttpContext.Request.Form.Files.Count > 0)
                command.FormFile = HttpContext.Request.Form.Files[0];
            var courseId = await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Create new Course (id = {courseId}) is successful"));
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
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpPut(Name = nameof(UpdateCourse))]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ResponseDto>> UpdateCourse([FromForm] UpdateCourseDto updateCourseDto) // TODO: Id in Sample request
        {
            var command = _mapper.Map<UpdateCourseCommand>(updateCourseDto);
            command.CoachGuid = UserGuid;
            if (HttpContext.Request.Form.Files.Count > 0)
                command.FormFile = HttpContext.Request.Form.Files[0];
            await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Update Course (id = {updateCourseDto.Id}) is successful"));
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
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpDelete("{id}", Name = nameof(DeleteCourse))]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ResponseDto>> DeleteCourse(int id)
        {
            var command = new DeleteCourseCommand
            {
                Id = id,
                CoachGuid = UserGuid
            };
            await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Delete Course (id = {id}) is successful"));
        }
    }
}
