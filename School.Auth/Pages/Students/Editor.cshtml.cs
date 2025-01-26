using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Auth.Models;
using School.Domain;
using System.Text.Json;

namespace School.Auth.Pages.Students
{
    [IgnoreAntiforgeryToken]
    public class EditorModel : CoachPageModel
    {
        private UserManager<IdentityUser> userManager;

        public IdentityUser? Student { get; set; }
        public string ids { get; set; }
        public StudentIdsVm Ids { get; set; }

        public EditorModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IEnumerable<StudentCourseLookupDto> Purchased() =>
            Ids.studentsIds.FirstOrDefault(s => s.studentGuid == Student.Id)?.courses 
                ?? new List<StudentCourseLookupDto>();

        public IEnumerable<StudentCourseLookupDto> NonPurchased()
        {
            List<int> purchasedList = Purchased().Select(c => c.courseId).ToList() ?? new List<int>();
            return Ids.allCourses.Where(c => !purchasedList.Contains(c.courseId)).ToList();
        }

        public async Task OnGetAsync(string id, string ids)
        {
            this.ids = ids;
            this.Ids = JsonSerializer.Deserialize<StudentIdsVm>(this.ids);
            this.Student = await userManager.FindByIdAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(int? courseid, string? studentid, string ids)
        {
            if (courseid is not null && !string.IsNullOrWhiteSpace(studentid))
            {
                this.Ids = JsonSerializer.Deserialize<StudentIdsVm>(ids);

                StudentCourseLookupDto course = Ids.allCourses.First(c => c.courseId == courseid);
                StudentLookupDto? student = Ids.studentsIds.FirstOrDefault(s => s.studentGuid == studentid);
                if (student != null)
                {
                    if (student.courses.Any(c => c.courseId == course.courseId))
                    {
                        RemoveStudentFromCourseDto dto = new RemoveStudentFromCourseDto
                        {
                            studentGuid = student.studentGuid,
                            courseId = course.courseId
                        };
                        return Redirect($"https://localhost:49824/students/from-course?dto={JsonSerializer.Serialize<RemoveStudentFromCourseDto>(dto)}");
                    }
                    else
                    {
                        AddStudentToCourseDto dto = new AddStudentToCourseDto
                        {
                            studentGuid = student.studentGuid,
                            courseId = course.courseId
                        };
                        return Redirect($"https://localhost:49824/students/to-course?dto={JsonSerializer.Serialize<AddStudentToCourseDto>(dto)}");
                    }
                }
                else
                {
                    IdentityUser studentUser = await userManager.FindByIdAsync(studentid);
                    AddStudentToCourseDto dto = new AddStudentToCourseDto
                    {
                        studentGuid = studentUser.Id,
                        courseId = course.courseId
                    };
                    return Redirect($"https://localhost:49824/students/to-course?dto={JsonSerializer.Serialize<AddStudentToCourseDto>(dto)}");
                }
            }
            return Page();
        }
    }
}
