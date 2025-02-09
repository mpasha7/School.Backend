using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Reports.Commands.CreateReport;
using School.Application.Handlers.Reports.Queries.GetReportDetails;
using School.Application.Handlers.Reports.Queries.GetReportList;
using School.WebApi.Models;
using School.WebApi.Models.Report;

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
    public class ReportsController : BaseController
    {
        private readonly IMapper _mapper;

        public ReportsController(IMapper mapper)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// Gets the list of reports
        /// </summary>
        /// <remarks>
        /// GET /api/reports/5
        /// </remarks>
        /// <param name="courseid">Course id (int)</param>
        /// <returns>Returns ReportListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet("{courseid}")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ReportListVm>> GetReportList(int courseid)
        {
            var query = new GetReportListQuery
            {
                CourseId = courseid,
                CoachGuid = UserGuid
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the report by id
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// GET api/reports/2/5/7
        /// </remarks>
        /// <param name="courseid">Course id (int)</param>
        /// <param name="lessonid">Lesson id (int)</param>
        /// <param name="id">Report id (int)</param>
        /// <returns>Returns ReportDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet("{courseid}/{lessonid}/{id}")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ReportDetailsVm>> GetReport(int courseid, int lessonid, int id)
        {
            var query = new GetReportDetailsQuery
            {
                Id = id,
                LessonId = lessonid,
                CourseId = courseid,
                CoachGuid = UserGuid
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the report
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/reports
        /// {
        ///     Text: "reports text",
        ///     LessonId: 7,
        ///     CourseId: 2
        /// }
        /// </remarks>
        /// <param name="dto">CreateReportDto object</param>
        /// <returns>Returns success phrase with report id</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<ResponseDto>> CreateReport([FromForm] CreateReportDto dto)
        {
            var command = _mapper.Map<CreateReportCommand>(dto);
            command.StudentGuid = UserGuid;
            command.StudentName = User.Claims
                .Where(c => c.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")
                .Select(c => c.Value)
                .SingleOrDefault() ?? "";
            if (HttpContext.Request.Form.Files.Count > 0)
                command.FormFiles = HttpContext.Request.Form.Files;

            var reportId = await Mediator!.Send(command);

            var response = new ResponseDto();
            return Ok(response.Success($"Create new Report (id = {reportId}) is successful"));
        }
    }
}
