using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace School.Auth.Pages.Roles
{
    [IgnoreAntiforgeryToken]
    public class EditorModel : AdminPageModel
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public IdentityRole Role { get; set; }
        public List<string> MembersOrNot { get; set; } = new List<string> { "Представители", "НЕ представители" };
        public string Choice { get; set; }
        public string SearchString { get; set; }

        public EditorModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<IdentityUser>> Members() =>
            await userManager.GetUsersInRoleAsync(Role.Name);

        public async Task<IEnumerable<IdentityUser>> NonMembers() =>
            userManager.Users.ToList().Except(await Members());

        public async Task OnGetAsync(string id, string choice = "Представители", string searchstring = "")
        {
            Role = await roleManager.FindByIdAsync(id);
            Choice = choice;
            SearchString = searchstring;
        }

        public async Task<IActionResult> OnPostAsync(string userid, string rolename)
        {
            Role = await roleManager.FindByNameAsync(rolename);
            IdentityUser user = await userManager.FindByIdAsync(userid);

            var claims = await userManager.GetClaimsAsync(user);
            Claim? claim = claims.Where(c => c.Type == "role").SingleOrDefault();

            IdentityResult result;
            if (await userManager.IsInRoleAsync(user, rolename))
            {
                result = await userManager.RemoveFromRoleAsync(user, rolename);
                if (result.Succeeded)
                    result = await userManager.RemoveClaimAsync(user, claim);
            }
            else
            {
                result = await userManager.AddToRoleAsync(user, rolename);
                if (result.Succeeded)
                    result = await userManager.AddClaimAsync(user, new Claim("role", Role.Name));
            }

            string roleId = Role.Id;
            if (result.Succeeded)
            {
                return RedirectToPage(new { id = roleId });
            }
            else
            {
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostChoiceAsync(string searchstring, string choice)
        {
            return RedirectToPage("Editor", new { searchstring = searchstring, choice = choice });
        }
    }
}
