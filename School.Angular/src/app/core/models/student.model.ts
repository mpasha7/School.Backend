export interface Student {
    id: string;
    username: string;
    email: string;
    phone: string;
}

export interface StudentCourseLookupDto {
    CourseId: number;
    Title: string;
}

export interface StudentLookupDto {
    StudentGuid: string;
    Courses: StudentCourseLookupDto[];
}

export interface StudentsIdsVm {
    StudentIds: StudentLookupDto[];
    AllCourses: StudentCourseLookupDto[];
}

export interface AddStudentToCourseDto {
    StudentGuid: string;
    CourseId: number;
}

export interface RemoveStudentFromCourseDto {
    StudentGuid: string;
    CourseId: number;
}