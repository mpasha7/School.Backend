import { createReducer, on } from "@ngrx/store";
import { initialLessonsState } from "./lessons.state";
import { loadLesson, loadLessonFailure, loadLessonList, loadLessonListFailure, loadLessonListSuccess, loadLessonSuccess } from "./lessons.actions";


export const lessonsReducer = createReducer(
    initialLessonsState,
    on(loadLessonList, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadLessonListSuccess, (state, action) => {
        return {
            ...state,
            lessonsList: [...action.lessonList.lessons],
            containingCourse: action.lessonList.course,
            loading: false
        }
    }),
    on(loadLessonListFailure, (state, action) => {
        return {
            ...state,
            lessonsList: [],
            containingCourse: null,
            loading: false,
            error: action.errorObject
        }
    }),
    on(loadLesson, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadLessonSuccess, (state, action) => {
        return {
            ...state,
            selectedLesson: action.lesson,
            containingCourse: action.lesson.course,
            loading: false
        }
    }),
    on(loadLessonFailure, (state, action) => {
        return {
            ...state,
            selectedLesson: null,
            containingCourse: null,
            loading: false,
            error: action.errorObject
        }
    })
);