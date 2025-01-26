import { createAction, props } from "@ngrx/store";
import { Student, StudentsIdsVm } from "../../core/models/student.model";


export enum EStudentActions {
    GetStudentIds = "[Student] Get Student Ids",
    GetStudentIdsSuccess = "[Student] Get Student Ids Success",
    GetStudentIdsFailed = "[Student] Get Student Ids Failed",
    AddStudentToCourse = "[Student] Add Student To Course",
    RemoveStudentFromCourse = "[Student] Remove Student From Course",

    ///////
    GetStudentListByIds = "[Student] Get Student List By Ids",
    GetStudentListByIdsSuccess = "[Student] Get Student List By Ids Success",
    GetStudentListByIdsFailed = "[Student] Get Student List By Ids Failed",
    GetStudentListBySearch = "[Student] Get Student List By Search",
    GetStudentListBySearchSuccess = "[Student] Get Student List By Search Success",
    GetStudentListBySearchFailed = "[Student] Get Student List By Search Failed",
}

export const loadStudentIds = createAction(
    EStudentActions.GetStudentIds
);
export const loadStudentIdsSuccess = createAction(
    EStudentActions.GetStudentIdsSuccess,
    props<{studentIdsVm: StudentsIdsVm}>()
);
export const loadStudentIdsFailed = createAction(
    EStudentActions.GetStudentIdsFailed,
    props<{errorObject: any}>()
);

export const addStudentToCourse = createAction(
    EStudentActions.AddStudentToCourse,
    props<{addToCourseDto: any}>()
);

export const removeStudentFromCourse = createAction(
    EStudentActions.RemoveStudentFromCourse,
    props<{removeFromCourseDto: any}>()
);


/////////////
export const loadStudentListByIds = createAction(
    EStudentActions.GetStudentListByIds,
    props<{ids: string}>()
);
export const loadStudentListByIdsSuccess = createAction(
    EStudentActions.GetStudentListByIdsSuccess,
    props<{students: Student[]}>()
);
export const loadStudentListByIdsFailed = createAction(
    EStudentActions.GetStudentListByIdsFailed,
    props<{errorObject: any}>()
);

export const loadStudentListBySearch = createAction(
    EStudentActions.GetStudentListBySearch,
    props<{search: string}>()
);
export const loadStudentListBySearchSuccess = createAction(
    EStudentActions.GetStudentListBySearchSuccess,
    props<{students: Student[]}>()
);
export const loadStudentListBySearchFailed = createAction(
    EStudentActions.GetStudentListBySearchFailed,
    props<{errorObject: any}>()
);