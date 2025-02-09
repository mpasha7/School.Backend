using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Feedbacks.Commands.CreateFeedback;
using School.Application.Handlers.Feedbacks.Queries.GetFeedbackDetails;
using School.Domain;
using School.WebApi.Models;
using School.WebApi.Models.Feedback;

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
    public class FeedbacksController : BaseController
    {
        private readonly IMapper _mapper;

        public FeedbacksController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the feedback by report id
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// GET api/feedbacks/2/5/7/12
        /// </remarks>
        /// <param name="courseid">Course id (int)</param>
        /// <param name="lessonid">Lesson id (int)</param>
        /// <param name="reportid">Report id (int)</param>
        /// <param name="id">Feedback id (int)</param>
        /// <returns>Returns FeedbackDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet("{courseid}/{lessonid}/{reportId}")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<FeedbackDetailsVm>> GetFeedback(int courseid, int lessonid, int reportid)
        {
            var query = new GetFeedbackDetailsQuery
            {
                ReportId = reportid,
                LessonId = lessonid,
                CourseId = courseid,
                StudentGuid = UserGuid
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/feedbacks
        /// {
        ///     Text: "feedbacks text",
        ///     ReportId: 12,
        ///     LessonId: 7,
        ///     CourseId: 2
        /// }
        /// </remarks>
        /// <param name="dto">CreateFeedbackDto object</param>
        /// <returns>Returns success phrase with report id</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpPost]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ResponseDto>> CreateFeedback([FromBody] CreateFeedbackDto dto)
        {
            var command = _mapper.Map<CreateFeedbackCommand>(dto);
            command.CoachGuid = UserGuid;

            var feedbackId = await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Create new Feedback (id = {feedbackId}) is successful"));
        }
    }
}
