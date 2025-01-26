using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Auth.Models;
using System.Text.Json;

namespace School.Auth.Pages.Students
{
    [IgnoreAntiforgeryToken]
    public class ListModel : CoachPageModel
    {
        private UserManager<IdentityUser> userManager;

        public IEnumerable<IdentityUser> Students { get; set; }
        public string Search { get; set; }
        public string ids { get; set; }
        public StudentIdsVm Ids { get; set; }

        public ListModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task OnGetAsync(string ids, string search = "")
        {
            this.ids = ids;
            this.Ids = JsonSerializer.Deserialize<StudentIdsVm>(this.ids);

            if (!string.IsNullOrWhiteSpace(search))
            {
                Search = search;
                Students = (await userManager.GetUsersInRoleAsync("Student"))
                    .Where(u => u.Email != null && u.Email.ToLower().Contains(search.ToLower()));
            }
            else
            {
                var studentsIds = Ids.studentsIds.Select(s => s.studentGuid).ToList();
                Students = (await userManager.GetUsersInRoleAsync("Student")).Where(u => studentsIds.Contains(u.Id)).ToList();
            }
        }

        public IActionResult OnPostAsync(string search)
        {
            return RedirectToPage("List", new { search = search });
        }
    }
}
