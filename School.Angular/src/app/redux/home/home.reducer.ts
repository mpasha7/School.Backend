import { createReducer, on } from "@ngrx/store";
import { initialHomeState } from "./home.state";
import { loadPublicCourse, loadPublicCourseFailure, loadPublicCourseList, loadPublicCourseListFailure, loadPublicCourseListSuccess, loadPublicCourseSuccess } from "./home.actions";


export const homeReducer = createReducer(
    initialHomeState,
    on(loadPublicCourseList, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadPublicCourseListSuccess, (state, action) => {
        return {
            ...state,
            publicCourseList: [...action.publicCourseList],
            loading: false
        }
    }),
    on(loadPublicCourseListFailure, (state, action) => {
        return {
            ...state,
            publicCourseList: [],
            loading: false,
            error: action.errorObject
        }
    }),
    on(loadPublicCourse, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadPublicCourseSuccess, (state, action) => {
        return {
            ...state,
            publicCourse: action.publicCourse,
            loading: false
        }
    }),
    on(loadPublicCourseFailure, (state, action) => {
        return {
            ...state,
            publicCourse: null,
            loading: false,
            error: action.errorObject
        }
    })
);