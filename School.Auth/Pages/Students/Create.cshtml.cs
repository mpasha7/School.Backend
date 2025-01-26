using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Domain;
using System.ComponentModel.DataAnnotations;
using School.Auth.Models;
using System.Text.Json;

namespace School.Auth.Pages.Students
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : CoachPageModel
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        [BindProperty]
        [Required(ErrorMessage = "Введите имя")]
        public string UserName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Введите Email")]
        [EmailAddress(ErrorMessage = "Email не соответствует формату")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Введите номер телефона")]
        [Phone(ErrorMessage = "Номер телефона не соответствует формату")]
        public string Phone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [BindProperty]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public int? CourseId { get; set; }
        public IEnumerable<StudentCourseLookupDto> Courses { get; set; }
        public string ids { get; set; }
        public StudentIdsVm Ids { get; set; }

        public CreateModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task OnGetAsync(string ids)
        {
            this.ids = ids;
            this.Ids = JsonSerializer.Deserialize<StudentIdsVm>(this.ids);
            Courses = Ids.allCourses;
        }

        public async Task<IActionResult> OnPostAsync(string ids)
        {
            this.Ids = JsonSerializer.Deserialize<StudentIdsVm>(ids);
            Courses = Ids.allCourses;

            if (ModelState.IsValid)
            {
                IdentityUser student = new IdentityUser
                {
                    UserName = UserName,
                    Email = Email,
                    PhoneNumber = Phone
                };
                IdentityResult result = await userManager.CreateAsync(student, Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(student, "Student");
                }

                if (result.Succeeded && CourseId != null && CourseId > 0)
                {
                    AddStudentToCourseDto dto = new AddStudentToCourseDto
                    {
                        studentGuid = student.Id,
                        courseId = CourseId.Value
                    };
                    return Redirect($"https://localhost:49824/students/to-course?dto={JsonSerializer.Serialize<AddStudentToCourseDto>(dto)}");
                }
                if (result.Succeeded)
                {
                    return RedirectToPage("List", new { ids = ids });
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
