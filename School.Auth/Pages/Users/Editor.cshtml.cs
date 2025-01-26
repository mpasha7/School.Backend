using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static Duende.IdentityServer.Models.IdentityResources;

namespace School.Auth.Pages.Users
{
    [IgnoreAntiforgeryToken]
    public class EditorModel : AdminPageModel
    {
        private UserManager<IdentityUser> userManager;

        [BindProperty]
        [Required]
        public string Id { get; set; }

        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [Phone]
        public string Phone { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [BindProperty]
        [Compare("Password", ErrorMessage = "ѕароли не совпадают")]
        public string? ConfirmPassword { get; set; }

        public EditorModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task OnGetAsync(string id)
        {
            IdentityUser user = await userManager.FindByIdAsync(id);
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Phone = user.PhoneNumber;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByIdAsync(Id);
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                IdentityResult result;
                var claims = await userManager.GetClaimsAsync(user);
                if (user.UserName != UserName)
                {
                    user.UserName = UserName;
                    Claim? claim = claims.Where(c => c.Type == "given_name").SingleOrDefault();
                    if (claim != null)
                    {
                        result = await userManager.RemoveClaimAsync(user, claim);
                        if (result.Succeeded)
                            result = await userManager.AddClaimAsync(user, new Claim("given_name", UserName));
                    }                        
                }
                if (user.Email != Email)
                {
                    user.Email = Email;
                    Claim? claim = claims.Where(c => c.Type == "email").SingleOrDefault();
                    if (claim != null)
                    {
                        result = await userManager.RemoveClaimAsync(user, claim);
                        if (result.Succeeded)
                            result = await userManager.AddClaimAsync(user, new Claim("email", Email));
                    }
                }
                if (user.PhoneNumber != Phone)
                {
                    user.PhoneNumber = Phone;
                    Claim? claim = claims.Where(c => c.Type == "phone").SingleOrDefault();
                    if (claim != null)
                    {
                        result = await userManager.RemoveClaimAsync(user, claim);
                        if (result.Succeeded)
                            result = await userManager.AddClaimAsync(user, new Claim("phone", Phone));
                    }
                }
                
                result = await userManager.UpdateAsync(user);
                if (result.Succeeded && !string.IsNullOrEmpty(Password))
                {
                    await userManager.RemovePasswordAsync(user);
                    result = await userManager.AddPasswordAsync(user, Password);
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
