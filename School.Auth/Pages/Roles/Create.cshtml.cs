using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace School.Auth.Pages.Roles
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : AdminPageModel
    {
        private RoleManager<IdentityRole> roleManager;

        [BindProperty]
        [Required]
        public string Name { get; set; }

        public CreateModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole { Name = Name };
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToPage("List");
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return Page();
        }
    }
}
