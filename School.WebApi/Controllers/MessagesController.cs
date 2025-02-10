using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Messages.Commands.CreateMessage;
using School.Application.Handlers.Messages.Commands.DeleteMessage;
using School.Application.Handlers.Messages.Queries.GetMessageList;
using School.WebApi.Models;
using School.WebApi.Models.Message;

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
    public class MessagesController : BaseController
    {
        private readonly IMapper _mapper;

        public MessagesController(IMapper mapper)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// Gets the list of messages
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /messages?courseid=5
        /// </remarks>
        /// <param name="courseid">Course id (int?)</param>
        /// <returns>>Returns MessageListVm</returns>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet]
        [Authorize(Roles = "Coach,Student")]
        public async Task<ActionResult<MessageListVm>> GetMessageList([FromQuery] int? courseid = null)
        {
            var query = new GetMessageListQuery
            {
                CourseId = courseid,
                UserGuid = UserGuid,
                UserRole = GetUserRole()
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the message
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /messages
        /// {
        ///     RecipientGuid: "e414564f-7d6d-4929-aa54-48414cb2103c",
        ///     Theme: "message theme",
        ///     Text: "message text",
        ///     QuestionId: 15,
        ///     CourseId: 5
        /// }
        /// </remarks>
        /// <param name="dto">CreateMessageDto object</param>
        /// <returns>Returns success phrase with message id</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="404">Object is not found</response>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateMessage([FromBody] CreateMessageDto dto)
        {
            var command = _mapper.Map<CreateMessageCommand>(dto);
            command.SenderGuid = UserGuid;            
            command.SenderName = User.Claims
                .Where(c => c.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")
                .Select(c => c.Value)
                .SingleOrDefault() ?? "";
            command.SenrerRole = GetUserRole();
            var messageId = await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Create new Message (id = {messageId}) is successful"));
        }

        /// <summary>
        /// Deletes the message by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /messages/2?courseid=5
        /// </remarks>
        /// <param name="id">Message id (int)</param>
        /// <param name="courseid">Message id (int?)</param>
        /// <returns>Returns success phrase with message id</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Coach,Student")]
        public async Task<ActionResult<ResponseDto>> DeleteMessage(int id, [FromQuery] int? courseid = null)
        {
            var command = new DeleteMessageCommand
            {
                Id = id,
                CourseId = courseid,
                SenderGuid = UserGuid
            };
            await Mediator!.Send(command);
            var response = new ResponseDto();
            return Ok(response.Success($"Delete Message (id = {id}) is successful"));
        }
    }
}
