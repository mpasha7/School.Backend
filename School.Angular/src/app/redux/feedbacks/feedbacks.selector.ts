import { createFeatureSelector, createSelector } from "@ngrx/store";
import { FeedbackState } from "./feedbacks.state";


export const selectFeedbackState = createFeatureSelector<FeedbackState>('feedbackStore');

export const selectFeedback = createSelector(
    selectFeedbackState,
    (state: FeedbackState) => state.feedback
);

export const selectFeedbackLoading = createSelector(
    selectFeedbackState,
    (state: FeedbackState) => state.loading
);

export const selectFeedbackError = createSelector(
    selectFeedbackState,
    (state: FeedbackState) => state.error
);