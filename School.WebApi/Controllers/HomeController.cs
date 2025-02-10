using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Application.Handlers.Courses.Queries.PublicCourseDetails;
using School.Application.Handlers.Courses.Queries.PublicCourseList;

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
    public class HomeController : BaseController
    {
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of courses for public access
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /home
        /// </remarks>
        /// <returns>Returns PublicCourseListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet]
        public async Task<ActionResult<PublicCourseListVm>> PublicCourseList()
        {
            var query = new PublicCourseListQuery();
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the public course by id
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// GET /home/5
        /// </remarks>
        /// <param name="id">Course id (int)</param>
        /// <returns>Returns PublicCourseDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Request is not correct</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="403">No access to object</response>
        /// <response code="404">Object is not found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicCourseDetailsVm>> PublicCourse(int id)
        {
            var query = new PublicCourseDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator!.Send(query);
            return Ok(vm);
        }
    }
}
