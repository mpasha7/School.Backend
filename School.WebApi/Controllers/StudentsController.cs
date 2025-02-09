using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Students.Commands.AddStudentToCourse;
using School.Application.Handlers.Students.Commands.RemoveStudentFromCourse;
using School.Application.Handlers.Students.Queries.GetStudentIds;
using School.Application.Handlers.Students.Queries.GetStudentsIds;
using School.Domain;
using School.WebApi.Models;
using School.WebApi.Models.Student;

namespace School.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Coach")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    public class StudentsController : BaseController
    {
        private readonly IMapper _mapper;

        public StudentsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of students ids
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/students
        /// </remarks>
        /// <returns>Returns StudentsIdsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet]
        public async Task<ActionResult<StudentsIdsVm>> GetStudentsIds()
        {
            var query = new GetStudentsIdsQuery
            {
                CoachGuid = UserGuid
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Add student to course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/students/add
        /// {
        ///     StudentGuid: "477e4fc4-2e97-4938-b25f-f6b388f606e4",
        ///     CourseId: 5
        /// }
        /// </remarks>
        /// <param name="dto">AddToCourseDto object</param>
        /// <returns>Returns success phrase with studentOfCourse id</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        /// <response code="405">Action aleady completed</response>
        [HttpPost("add")]
        public async Task<ActionResult<ResponseDto>> AddStudentToCourse(AddToCourseDto dto)
        {
            var command = _mapper.Map<AddStudentToCourseCommand>(dto);
            command.CoachGuid = UserGuid;
            var id = await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Create new StudentOfCourse (id = {id}) is successful"));
        }

        /// <summary>
        /// Remove student from course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/students/remove
        /// {
        ///     StudentGuid: "477e4fc4-2e97-4938-b25f-f6b388f606e4",
        ///     CourseId: 5
        /// }
        /// </remarks>
        /// <param name="dto">RemoveFromCourseDto object</param>
        /// <returns>Returns success phrase</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        /// <response code="405">Action aleady completed</response>
        [HttpPost("remove")]
        public async Task<ActionResult<ResponseDto>> RemoveStudentFromCourse(RemoveFromCourseDto dto)
        {
            var command = _mapper.Map<RemoveStudentFromCourseCommand>(dto);
            command.CoachGuid = UserGuid;
            await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Remove StudentOfCourse is successful"));
        }
    }
}
