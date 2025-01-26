using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace School.Auth.Pages
{
    [Authorize(Roles = "Coach")]
    public class CoachPageModel : PageModel
    {
    }
}
