import { createReducer, on } from "@ngrx/store";
import { initialCoursesState } from "./courses.state";
import { loadCourse, loadCourseFailure, loadCourseList, loadCourseListFailure, loadCourseListSuccess, loadCourseSuccess } from "./courses.actions";


export const coursesReducer = createReducer(
    initialCoursesState,
    on(loadCourseList, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadCourseListSuccess, (state, action) => {
        return {
            ...state,
            courseList: [...action.courseList],
            loading: false
        }
    }),
    on(loadCourseListFailure, (state, action) => {
        return {
            ...state,
            courseList: [],
            loading: false,
            error: action.errorObject
        }
    }),
    on(loadCourse, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadCourseSuccess, (state, action) => {
        return {
            ...state,
            selectedCourse: action.course,
            loading: false
        }
    }),
    on(loadCourseFailure, (state, action) => {
        return {
            ...state,
            selectedCourse: null,
            loading: false,
            error: action.errorObject
        }
    })
);