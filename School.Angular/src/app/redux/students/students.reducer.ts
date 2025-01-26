import { createReducer, on } from "@ngrx/store";
import { initialStudentsState } from "./students.state";
import { 
    loadStudentIds,
    loadStudentIdsFailed,
    loadStudentIdsSuccess,
    loadStudentListByIds,
    loadStudentListByIdsFailed,
    loadStudentListByIdsSuccess,
    loadStudentListBySearch,
    loadStudentListBySearchFailed,
    loadStudentListBySearchSuccess
} from "./students.actions";


export const studentsReducer = createReducer(
    initialStudentsState,
    on(loadStudentIds, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadStudentIdsSuccess, (state, action) => {
        return {
            ...state,
            studentsIds: action.studentIdsVm,
            loading: false
        }
    }),
    on(loadStudentIdsFailed, (state, action) => {
        return {
            ...state,
            studentsIds: null,
            loading: false,
            error: action.errorObject
        }
    }),
    

    /////////////
    on(loadStudentListByIds, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadStudentListByIdsSuccess, (state, action) => {
        return {
            ...state,
            studentList: [...action.students],
            loading: false
        }
    }),
    on(loadStudentListByIdsFailed, (state, action) => {
        return {
            ...state,
            studentsList: [],
            loading: false,
            error: action.errorObject
        }
    }),
    on(loadStudentListBySearch, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadStudentListBySearchSuccess, (state, action) => {
        return {
            ...state,
            studentList: [...action.students],
            loading: false
        }
    }),
    on(loadStudentListBySearchFailed, (state, action) => {
        return {
            ...state,
            studentsList: [],
            loading: false,
            error: action.errorObject
        }
    })
);