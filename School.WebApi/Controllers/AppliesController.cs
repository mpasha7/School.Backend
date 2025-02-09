using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Applies.Commands.CreateApply;
using School.Application.Handlers.Applies.Commands.DeleteApply;
using School.Application.Handlers.Applies.Commands.UpdateApply;
using School.Application.Handlers.Applies.Queries.GetApplyList;
using School.Domain;
using School.WebApi.Models;
using School.WebApi.Models.Apply;
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
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    public class AppliesController : BaseController
    {
        private readonly IMapper _mapper;

        public AppliesController(IMapper mapper)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// Gets the list of applies
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET api/applies?courseid=5
        /// </remarks>
        /// <param name="courseid">Course id (int)</param>
        /// <returns>Returns ApplyListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ApplyListVm>> GetApplyList([FromQuery] int courseid)
        {
            var query = new GetApplyListQuery
            {
                CoachGuid = UserGuid,
                CourseId = courseid
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the apply
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST api/lessons
        /// {
        ///     StudentName: "Alex",
        ///     CourseId: 5
        /// }
        /// </remarks>
        /// <param name="dto">CreateApplyDto object</param>
        /// <returns>Returns success phrase with apply id</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        /// <response code="405">Action aleady completed</response>
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<ResponseDto>> CreateApply([FromBody] CreateApplyDto dto)
        {
            var command = _mapper.Map<CreateApplyCommand>(dto);
            command.StudentGuid = UserGuid;
            command.StudentName = User.Claims
                .Where(c => c.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")
                .Select(c => c.Value)
                .SingleOrDefault() ?? "";
            var applyId = await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Create new Apply (id = {applyId}) is successful"));
        }

        /// <summary>
        /// Updates the apply and create new StudentOfCourse object
        /// </summary>
        /// <remarks>
        /// PUT api/lessons
        /// {
        ///     Id: 7,
        ///     StudentGuid: "ffd7d2fc-d772-4abf-8960-a6b3d4322f69",
        ///     CourseId: 5
        /// }
        /// </remarks>
        /// <param name="dto">UpdateApplyDto object</param>
        /// <returns>Returns success phrase with apply id</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        /// <response code="405">Action aleady completed</response>
        [HttpPut]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ResponseDto>> UpdateApply([FromBody] UpdateApplyDto dto)
        {
            var command = _mapper.Map<UpdateApplyCommand>(dto);
            command.CoachGuid = UserGuid;
            await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Update Apply (id = {dto.Id}) is successful"));
        }

        /// <summary>
        /// Deletes the apply by id
        /// </summary>
        /// <remarks>
        /// PUT api/lessons
        /// {
        ///     Id: 7,
        ///     StudentGuid: "ffd7d2fc-d772-4abf-8960-a6b3d4322f69",
        ///     CourseId: 5
        /// }
        /// </remarks>
        /// <param name="id">Apply id (int)</param>
        /// <param name="courseid">Course id (int)</param>
        /// <returns>Returns success phrase with apply id</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ResponseDto>> DeleteApply(int id, [FromQuery] int courseid)
        {
            var command = new DeleteApplyCommand
            {
                Id = id,
                CoachGuid = UserGuid,
                CourseId = courseid
            };
            await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Delete Apply (id = {id}) is successful"));
        }
    }
}
