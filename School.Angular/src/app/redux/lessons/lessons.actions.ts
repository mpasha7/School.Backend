import { createAction, props } from "@ngrx/store";
import { LessonDetailsVm, LessonListVm, LessonLookupDto } from "../../core/models/lesson.model";


export enum ELessonActions {
    GetLessonList = "[Lesson] Get Lesson List",
    GetLessonListSuccess = "[Lesson] Get Lesson List Success",
    GetLessonListFailure = "[Lesson] Get Lesson List Failure",
    GetLesson = "[Lesson] Get Lesson",
    GetLessonSuccess = "[Lesson] Get Lesson Success",
    GetLessonFailure = "[Lesson] Get Lesson Failure",
    CreateLesson = "[Lesson] Create Lesson",
    UpdateLesson = "[Lesson] Update Lesson",
    DeleteLesson = "[Lesson] Delete Lesson"
}

export const loadLessonList = createAction(
    ELessonActions.GetLessonList,
    props<{courseId: number}>()
);
export const loadLessonListSuccess = createAction(
    ELessonActions.GetLessonListSuccess,
    props<{lessonList: LessonListVm}>()
);
export const loadLessonListFailure = createAction(
    ELessonActions.GetLessonListFailure,
    props<{errorObject: any}>()
);

export const loadLesson = createAction(
    ELessonActions.GetLesson,
    props<{id: number, courseId: number}>()
);
export const loadLessonSuccess = createAction(
    ELessonActions.GetLessonSuccess,
    props<{lesson: LessonDetailsVm}>()    
);
export const loadLessonFailure = createAction(
    ELessonActions.GetLessonFailure,
    props<{errorObject: any}>()
);

export const createLesson = createAction(
    ELessonActions.CreateLesson,
    props<{createLessonDto: any}>()
);

export const updateLesson = createAction(
    ELessonActions.UpdateLesson,
    props<{updateLessonDto: any}>()
);

export const deleteLesson = createAction(
    ELessonActions.DeleteLesson,
    props<{id: number, courseId: number}>()
);