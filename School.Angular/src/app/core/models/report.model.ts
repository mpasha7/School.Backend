import { FileLookupDto } from "./file.model";


export interface ReportListVm {
    reports: ReportLookupDto[];
    maxLessonNumber: number;
}

export interface ReportLookupDto {
    id: number;
    studentGuid: string;
    studentName: string;
    createdAt: Date;

    lessonId: number;
    lessonNumber: number;
    lessonTitle: string;
}

export interface ReportDetailsVm {
    id: number;
    text: string;
    photos: FileLookupDto[];
}