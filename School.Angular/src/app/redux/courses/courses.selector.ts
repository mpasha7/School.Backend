import { createFeatureSelector, createSelector } from "@ngrx/store";
import { CoursesState } from "./courses.state";


export const selectCoursesState = createFeatureSelector<CoursesState>('coursesStore');

export const selectCourseList = createSelector(
    selectCoursesState,
    (state: CoursesState) => state.courseList
);

export const selectCourse = createSelector(
    selectCoursesState,
    (state: CoursesState) => state.selectedCourse
);

export const selectCourseLoading = createSelector(
    selectCoursesState,
    (state: CoursesState) => state.loading
);

export const selectCourseError = createSelector(
    selectCoursesState,
    (state: CoursesState) => state.error
);