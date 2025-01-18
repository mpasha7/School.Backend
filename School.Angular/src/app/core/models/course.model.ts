import { FileLookupDto } from "./file.model";


export interface CourseListVm {
    courses: CourseLookupDto[];
}
export interface CourseLookupDto {
    id: number;

    title: string | null;
    description: string | null;

    photo: FileLookupDto | null;
}
export interface CourseDetailsVm {
    id: number;

    title: string | null;
    description: string | null;
    shortDescription: string | null;
    publicDescription: string | null;
    beginQuestionnaire: string | null;
    endQuestionnaire: string | null;

    photo: FileLookupDto | null;
}



export interface PublicCourseListVm {
    courses: PublicCourseLookupDto[];
}
export interface PublicCourseLookupDto {
    id: number;

    title: string | null;
    shortDescription: string | null;

    photo: FileLookupDto | null;
}
export interface PublicCourseDetailsVm {
    id: number;

    title: string | null;
    publicDescription: string | null;

    photo: FileLookupDto | null;
}



export interface CreateCourseDto {
    title: string;
    description: string;
    shortDescription: string | null;
    publicDescription: string | null;
    beginQuestionnaire: string | null;
    endQuestionnaire: string | null;
}
export interface UpdateCourseDto {
    id: number;
    
    title: string;
    description: string;
    shortDescription: string | null;
    publicDescription: string | null;
    beginQuestionnaire: string | null;
    endQuestionnaire: string | null;
}