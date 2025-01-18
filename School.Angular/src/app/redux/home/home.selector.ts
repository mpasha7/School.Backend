import { createFeatureSelector, createSelector } from "@ngrx/store";
import { HomeState } from "./home.state";


export const selectHomeState = createFeatureSelector<HomeState>('homeStore');

export const selectPublicCourseList = createSelector(
    selectHomeState,
    (state: HomeState) => state.publicCourseList
);

export const selectPublicCourse = createSelector(
    selectHomeState,
    (state: HomeState) => state.publicCourse
);

export const selectHomeLoading = createSelector(
    selectHomeState,
    (state: HomeState) => state.loading
);

export const selectHomeError = createSelector(
    selectHomeState,
    (state: HomeState) => state.error
);