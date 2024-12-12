export interface LessonListVm {
    lessons: LessonLookupDto[];
}

export interface LessonLookupDto {
    id: number;
    number: number;
    title: string | null;
}