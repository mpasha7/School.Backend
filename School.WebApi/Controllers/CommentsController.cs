using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Comments.Commands.CreateComment;
using School.Application.Handlers.Comments.Commands.UpdateComment;
using School.Application.Handlers.Comments.Queries.GetCommentList;
using School.WebApi.Models;
using School.WebApi.Models.Comment;

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
    public class CommentsController : BaseController
    {
        private readonly IMapper _mapper;

        public CommentsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of comments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET api/comments/5
        /// </remarks>
        /// <param name="courseid">Course id (int)</param>
        /// <returns>Returns CommentListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet("{courseid}")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<CommentListVm>> GetCommentList(int courseid)
        {
            var query = new GetCommentListQuery
            {
                CourseId = courseid,
                CoachGuid = UserGuid
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST api/comments
        /// {
        ///     Text: "comment text",
        ///     CourseId: 5
        /// }
        /// </remarks>
        /// <param name="dto">CreateCommentDto object</param>
        /// <returns>Returns success phrase with comment id</returns>>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<ResponseDto>> CreateComment([FromBody] CreateCommentDto dto)
        {
            var command = _mapper.Map<CreateCommentCommand>(dto);
            command.StudentGuid = UserGuid;
            command.StudentName = User.Claims
                .Where(c => c.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")
                .Select(c => c.Value)
                .SingleOrDefault() ?? "";
            var commentId = await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Create new Comment (id = {commentId}) is successful"));
        }

        /// <summary>
        /// Updates the comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT api/comments
        /// {
        ///     Id: 5
        /// }
        /// </remarks>
        /// <param name="dto">UpdateCommentDto object</param>
        /// <returns>Returns success phrase with comment id</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpPut]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ResponseDto>> UpdateComment([FromBody] UpdateCommentDto dto)
        {
            var command = _mapper.Map<UpdateCommentCommand>(dto);
            command.CoachGuid = UserGuid;
            await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Update Comment (id = {dto.Id}) is successful"));
        }
    }
}
