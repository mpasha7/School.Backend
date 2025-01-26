using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace School.Auth.Pages.Roles
{
    [IgnoreAntiforgeryToken]
    public class ListModel : AdminPageModel
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public IEnumerable<IdentityRole> Roles { get; set; }

        public ListModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task OnGet()
        {
            Roles = await roleManager.Roles.ToListAsync();
        }

        public async Task<string> GetMembersString(string role)
        {
            IEnumerable<IdentityUser> users = await userManager.GetUsersInRoleAsync(role);
            string result = users.Count() == 0
                ? "Нет представителей"
                : string.Join(", ", users.Take(3).Select(u => u.UserName).ToArray());
            return users.Count() > 3
                ? $"{result} и др."
                : result;
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await roleManager.DeleteAsync(role);
                return RedirectToPage();
            }

            return Page();
        }
    }
}
