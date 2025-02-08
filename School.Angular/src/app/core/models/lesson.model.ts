import { CourseLookupDto } from "./course.model";
import { ReportDetailsVm, ReportLookupDto } from "./report.model";

export interface LessonListVm {
    lessons: LessonLookupDto[];
    course: CourseLookupDto;
    maxLessonNumber: number;
}

export interface LessonLookupDto {
    id: number;

    number: number;
    title: string | null;
}

export interface LessonDetailsVm {
    id: number;

    number: number;
    title: string | null;
    description: string | null;
    videoLink: string | null;

    course: CourseLookupDto;
    report: ReportDetailsVm | null;
}

export interface CreateLessonDto {
    courseId: number;

    number: number;
    title: string;
    description: string | null;
    videoLink: string | null;
}

export interface UpdateLessonDto {
    id: number;
    courseId: number;

    number: number;
    title: string;
    description: string | null;
    videoLink: string | null;
}