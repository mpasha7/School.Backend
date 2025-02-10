using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Assessments.Commands.CreateAssessment;
using School.Application.Handlers.Assessments.Queries.GetAssessmentDetails;
using School.WebApi.Models;
using School.WebApi.Models.Assessment;

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
    public class AssessmentsController : BaseController
    {
        private readonly IMapper _mapper;

        public AssessmentsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the asssessment by course id
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// GET api/asssessments/5
        /// </remarks>
        /// <param name="courseid">Course id (int)</param>
        /// <returns>Returns AssessmentDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet("{courseid}")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<AssessmentDetailsVm>> GetAssessment(int courseid)
        {
            var query = new GetAssessmentDetailsQuery
            {
                StudentGuid = UserGuid,
                CourseId = courseid
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the assessment
        /// </summary>
        /// <remarks>
        /// POST /api/assessments
        /// {
        ///     Text: "assessment text",
        ///     CourseId: 5
        /// }
        /// </remarks>
        /// <param name="dto">CreateAssessmentDto object</param>
        /// <returns>Returns success phrase with assessment id</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpPost]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ResponseDto>> CreateAssessment([FromBody] CreateAssessmentDto dto)
        {
            var command = _mapper.Map<CreateAssessmentCommand>(dto);
            command.CoachGuid = UserGuid;

            var assessmentId = await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Create new Assessment (id = {assessmentId}) is successful"));
        }

    }
}
