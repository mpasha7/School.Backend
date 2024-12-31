import { CourseLookupDto } from "../../core/models/course.model";
import { LessonDetailsVm, LessonLookupDto } from "../../core/models/lesson.model";


export interface LessonsState {
    lessonsList: LessonLookupDto[];
    containingCourse: CourseLookupDto | null;
    maxLessonNumber: number;
    selectedLesson: LessonDetailsVm | null;
    loading: boolean;
    error: any;
}

export const initialLessonsState: LessonsState = {
    lessonsList: [],
    containingCourse: null,
    maxLessonNumber: 0,
    selectedLesson: null,
    loading: false,
    error: null
}