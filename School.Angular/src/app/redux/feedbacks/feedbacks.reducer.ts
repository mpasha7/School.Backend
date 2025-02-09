import { createReducer, on } from "@ngrx/store";
import { initialFeedbackState } from "./feedbacks.state";
import { loadFeedback, loadFeedbackFailure, loadFeedbackSuccess } from "./feedbacks.actions";


export const feedbacksReducer = createReducer(
    initialFeedbackState,
    on(loadFeedback, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadFeedbackSuccess, (state, action) => {
        return {
            ...state,
            feedback: action.feedback,
            loading: false
        }
    }),
    on(loadFeedbackFailure, (state, action) => {
        return {
            ...state,
            feedback: null,
            loading: false,
            error: action.errorObject
        }
    })
);