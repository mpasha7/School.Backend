import { createFeatureSelector, createSelector } from "@ngrx/store";
import { AssessmentState } from "./assessments.state";


export const selectAssessmentState = createFeatureSelector<AssessmentState>('assessmentStore');

export const selectAssessment = createSelector(
    selectAssessmentState,
    (state: AssessmentState) => state.assessment
);

export const selectAssessmentLoading = createSelector(
    selectAssessmentState,
    (state: AssessmentState) => state.loading
);

export const selectAssessmentError = createSelector(
    selectAssessmentState,
    (state: AssessmentState) => state.error
);