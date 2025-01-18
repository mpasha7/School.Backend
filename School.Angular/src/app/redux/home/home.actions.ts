import { createAction, props } from "@ngrx/store";
import { PublicCourseDetailsVm, PublicCourseLookupDto } from "../../core/models/course.model";


export enum EHomeActions {
    PublicCourseList = "[Home] Public Course List",
    PublicCourseListSuccess = "[Home] Public Course List Success",
    PublicCourseListFailure = "[Home] Public Course List Failure",
    PublicCourse = "[Home] Public Course",
    PublicCourseSuccess = "[Home] Public Course Success",
    PublicCourseFailure = "[Home] Public Course Failure"
}

export const loadPublicCourseList = createAction(
    EHomeActions.PublicCourseList
);
export const loadPublicCourseListSuccess = createAction(
    EHomeActions.PublicCourseListSuccess,
    props<{publicCourseList: PublicCourseLookupDto[]}>()
);
export const loadPublicCourseListFailure = createAction(
    EHomeActions.PublicCourseListFailure,
    props<{errorObject: any}>()
);

export const loadPublicCourse = createAction(
    EHomeActions.PublicCourse,
    props<{id: number}>()
);
export const loadPublicCourseSuccess = createAction(
    EHomeActions.PublicCourseSuccess,
    props<{publicCourse: PublicCourseDetailsVm}>()
);
export const loadPublicCourseFailure = createAction(
    EHomeActions.PublicCourseFailure,
    props<{errorObject: any}>()
);