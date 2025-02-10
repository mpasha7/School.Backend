import { createReducer, on } from "@ngrx/store";
import { initialAssessmentState } from "./assessments.state";
import { loadAssessment, loadAssessmentFailure, loadAssessmentSuccess } from "./assessments.actions";


export const assessmentReducer = createReducer(
    initialAssessmentState,
    on(loadAssessment, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadAssessmentSuccess, (state, action) => {
        return {
            ...state,
            assessment: action.assessment,
            loading: false
        }
    }),
    on(loadAssessmentFailure, (state, action) => {
        return {
            ...state,
            assessment: null,
            loading: false,
            error: action.errorObject
        }
    })
);