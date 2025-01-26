import { createFeatureSelector, createSelector } from "@ngrx/store";
import { StudentsState } from "./students.state";


export const selectStudentsState = createFeatureSelector<StudentsState>('studentsStore');

export const selectStudentsIds = createSelector(
    selectStudentsState,
    (state: StudentsState) => state.studentsIds
);

export const selectStudentsLoading = createSelector(
    selectStudentsState,
    (state: StudentsState) => state.loading
);

////////////////////////
export const selectStudentsList = createSelector(
    selectStudentsState,
    (state: StudentsState) => state.studentList
);