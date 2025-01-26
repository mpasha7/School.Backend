using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace School.Auth.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminPageModel : PageModel
    {
    }
}
