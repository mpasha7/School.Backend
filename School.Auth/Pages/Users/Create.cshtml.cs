using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace School.Auth.Pages.Users
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : AdminPageModel
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        [BindProperty]
        [Required(ErrorMessage = "¬ведите им€")]
        public string UserName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "¬ведите Email")]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "¬ведите номер телефона")]
        [Phone]
        public string Phone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "¬ведите пароль")]
        public string Password { get; set; }

        [BindProperty]
        [Compare("Password", ErrorMessage = "ѕароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string? RoleId { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }

        public CreateModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            Roles = roleManager.Roles.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = UserName,
                    Email = Email,
                    PhoneNumber = Phone
                };
                IdentityResult result = await userManager.CreateAsync(user, Password);
                if (result.Succeeded && !string.IsNullOrWhiteSpace(RoleId) && RoleId != "None")
                {
                    IdentityRole? role = await roleManager.FindByIdAsync(RoleId);
                    if (role == null)
                        throw new ArgumentNullException(nameof(role));

                    result = await userManager.AddToRoleAsync(user, role.Name);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddClaimsAsync(user, new List<Claim>
                        {
                            new Claim("given_name", UserName),
                            new Claim("family_name", ""),
                            new Claim("email", Email),
                            new Claim("phone", Phone),
                            new Claim("role", role.Name)
                        });
                    }
                }
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
