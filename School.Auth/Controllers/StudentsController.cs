using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Auth.Models;

namespace School.Auth.Controllers
{
    //[Produces("application/json")]
    //[Route("branch/api/[controller]")]
    //[ApiController]
    //[Authorize(Roles = "Coach")]
    //public class StudentsController : ControllerBase
    //{
    //    private readonly UserManager<IdentityUser> _userManager;

    //    public StudentsController(UserManager<IdentityUser> userManager)
    //    {
    //        this._userManager = userManager;
    //    }

    //    [HttpGet("byids/{ids}")]
    //    //[Route("api/students/byids/{ids}")]
    //    public async Task<ActionResult<StudentIdsVm>> GetStudentsByIds(string ids)
    //    {
    //        string[] idList = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

    //        var students = (await _userManager.GetUsersInRoleAsync("Student"))
    //            .Where(s => idList.Contains(s.Id))
    //            .ToList();
    //        var dtoList = new List<StudentLookupDto>();
    //        foreach (var student in students)
    //        {
    //            dtoList.Add(new StudentLookupDto
    //            {
    //                Id = student.Id,
    //                UserName = student.UserName,
    //                Email = student.Email,
    //                Phone = student.PhoneNumber
    //            });
    //        }
    //        return Ok(new StudentIdsVm { Students = dtoList });
    //    }

    //    [HttpGet("bysearch/{search}")]
    //    //[Route("api/students/bysearch/{search}")]
    //    public async Task<ActionResult<StudentIdsVm>> GetStudentsBySearch(string search)
    //    {
    //        var students = (await _userManager.GetUsersInRoleAsync("Student"))
    //            .Where(s => s.Email != null
    //                     && s.Email.ToLower().Contains(search.ToLower()))
    //            .ToList();
    //        var dtoList = new List<StudentLookupDto>();
    //        foreach (var student in students)
    //        {
    //            dtoList.Add(new StudentLookupDto
    //            {
    //                Id = student.Id,
    //                UserName = student.UserName,
    //                Email = student.Email,
    //                Phone = student.PhoneNumber
    //            });
    //        }
    //        return Ok(new StudentIdsVm { Students = dtoList });
    //    }
    //}
}
