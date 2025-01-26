using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace School.Auth.Pages.Users
{
    [IgnoreAntiforgeryToken]
    public class ListModel : AdminPageModel
    {
        private UserManager<IdentityUser> userManager;

        public IEnumerable<IdentityUser> Users { get; set; }
        public IEnumerable<string?> Roles { get; set; }

        public string Role { get; set; }
        public string SearchString { get; set; } = "";

        public ListModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            Roles = roleManager.Roles.Select(r => r.Name).ToList();
        }

        public async Task OnGetAsync(string searchstring = "", string role = "All")
        {
            if (string.IsNullOrWhiteSpace(searchstring))
            {
                Role = role;
                if (Role == "All")
                    Users = userManager.Users.ToList();
                else
                    Users = (await userManager.GetUsersInRoleAsync(Role)).ToList();
            }
            else
            {
                Role = "All";
                SearchString = searchstring;
                Users = userManager.Users
                    .Where(u => u.Email != null && u.Email.ToLower().Contains(SearchString.ToLower()))
                    .ToList();
            }
        }

        public IActionResult OnPostAsync(string role)
        {
            return RedirectToPage("List", new { role = role });
        }

        public IActionResult OnPostSearchAsync(string searchstring)
        {
            return RedirectToPage("List", new { searchstring = searchstring });
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            IdentityUser? user = await userManager.FindByIdAsync(id);
            if (user is not null)
            {
                await userManager.DeleteAsync(user);
            }
            return RedirectToPage();
        }
    }
}
