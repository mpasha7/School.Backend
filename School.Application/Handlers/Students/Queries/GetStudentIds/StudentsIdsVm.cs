﻿namespace School.Application.Handlers.Students.Queries.GetStudentIds
{
    public class StudentsIdsVm
    {
        public IList<StudentLookupDto> StudentsIds { get; set; } = new List<StudentLookupDto>();
        public IList<StudentCourseLookupDto> AllCourses { get; set; } = new List<StudentCourseLookupDto>();
    }
}
