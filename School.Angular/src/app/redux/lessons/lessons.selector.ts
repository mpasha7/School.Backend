import { createFeatureSelector, createSelector } from "@ngrx/store";
import { LessonsState } from "./lessons.state";


export const selectLessonsState = createFeatureSelector<LessonsState>('lessonsStore');

export const selectLessonList = createSelector(
    selectLessonsState,
    (state: LessonsState) => state.lessonsList
);

export const selectContainingCourse = createSelector(
    selectLessonsState,
    (state: LessonsState) => state.containingCourse
);

export const selectLesson = createSelector(
    selectLessonsState,
    (state: LessonsState) => state.selectedLesson
);

export const selectLessonLoading = createSelector(
    selectLessonsState,
    (state: LessonsState) => state.loading
);

export const selectLessonError = createSelector(
    selectLessonsState,
    (state: LessonsState) => state.error
);