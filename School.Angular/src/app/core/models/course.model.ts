export interface CourseListVm {
    courses: CourseLookupDto[];
}

export interface CourseLookupDto {
    id: number;

    title: string | null;
    description: string | null;
    photoPath: string | null;
}

export interface CourseDetailsVm {
    id: number;

    title: string | null;
    description: string | null;
    shortDescription: string | null;
    publicDescription: string | null;
    photoPath: string | null;
    beginQuestionnaire: string | null;
    endQuestionnaire: string | null;
}

export interface CreateCourseDto {
    title: string;
    description: string;
    shortDescription: string | null;
    publicDescription: string | null;
    photoPath: string | null;
    beginQuestionnaire: string | null;
    endQuestionnaire: string | null;
}

export interface UpdateCourseDto {
    id: number;
    
    title: string;
    description: string;
    shortDescription: string | null;
    publicDescription: string | null;
    photoPath: string | null;
    beginQuestionnaire: string | null;
    endQuestionnaire: string | null;
}