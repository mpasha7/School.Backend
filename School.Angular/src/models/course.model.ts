export interface CourseListVm {
    courses: CourseLookupDto[];
}

export interface CourseLookupDto {
    id: number;
    title: string | null;
    description: string | null;
    photoPath: string | null;
}