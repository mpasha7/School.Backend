import { CourseLookupDto } from "../../core/models/course.model";
import { LessonDetailsVm, LessonLookupDto } from "../../core/models/lesson.model";


export interface LessonsState {
    lessonsList: LessonLookupDto[];
    selectedLesson: LessonDetailsVm | null;
    containingCourse: CourseLookupDto | null;
    loading: boolean;
    error: any;
}

export const initialLessonsState: LessonsState = {
    lessonsList: [],
    containingCourse: null,
    selectedLesson: null,
    loading: false,
    error: null
}