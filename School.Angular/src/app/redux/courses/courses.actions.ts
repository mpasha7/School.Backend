import { createAction, props } from "@ngrx/store";
import { CourseDetailsVm, CourseLookupDto, CreateCourseDto, UpdateCourseDto } from "../../core/models/course.model";


export enum ECourseActions {
    GetCourseList = "[Course] Get Course List",
    GetCourseListSuccess = "[Course] Get Course List Success",
    GetCourseListFailure = "[Course] Get Course List Failure",
    GetCourse = "[Course] Get Course",
    GetCourseSuccess = "[Course] Get Course Success",
    GetCourseFailure = "[Course] Get Course Failure",
    CreateCourse = "[Course] Create Course",
    UpdateCourse = "[Course] Update Course",
    DeleteCourse = "[Course] Delete Course"
}

export const loadCourseList = createAction(ECourseActions.GetCourseList);
export const loadCourseListSuccess = createAction(
    ECourseActions.GetCourseListSuccess,
    props<{courseList: CourseLookupDto[]}>()
);
export const loadCourseListFailure = createAction(
    ECourseActions.GetCourseListFailure,
    props<{errorObject: any}>()
);

export const loadCourse = createAction(
    ECourseActions.GetCourse,
    props<{id: number}>()
);
export const loadCourseSuccess = createAction(
    ECourseActions.GetCourseSuccess,
    props<{course: CourseDetailsVm}>()
);
export const loadCourseFailure = createAction(
    ECourseActions.GetCourseFailure,
    props<{errorObject: any}>()
);

export const createCourse = createAction(
    ECourseActions.CreateCourse,
    props<{createCourseDto: any}>()
);

export const updateCourse = createAction(
    ECourseActions.UpdateCourse,
    props<{updateCourseDto: any}>()
);

export const deleteCourse = createAction(
    ECourseActions.DeleteCourse,
    props<{id: number}>()
);