import { createAction, props } from "@ngrx/store";
import { FeedbackDetailsVm } from "../../core/models/feedback.model";


export enum EFeedbackActions {
    GetFeedback = "[Feedback] Get Feedback",
    GetFeedbackSuccess = "[Feedback] Get Feedback Success",
    GetFeedbackFailure = "[Feedback] Get Feedback Failure",
    CreateFeedback = "[Feedback] Create Feedback",
}

export const loadFeedback = createAction(
    EFeedbackActions.GetFeedback,
    props<{courseId: number, lessonId: number, reportId: number}>()
);
export const loadFeedbackSuccess = createAction(
    EFeedbackActions.GetFeedbackSuccess,
    props<{feedback: FeedbackDetailsVm}>()
);
export const loadFeedbackFailure = createAction(
    EFeedbackActions.GetFeedbackFailure,
    props<{errorObject: any}>()
);

export const createFeedback = createAction(
    EFeedbackActions.CreateFeedback,
    props<{createFeedbackDto: any}>()
);