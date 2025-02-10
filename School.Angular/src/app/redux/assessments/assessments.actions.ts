import { createAction, props } from "@ngrx/store";
import { AssessmentDetailsVm } from "../../core/models/assessment.model";


export enum EAssessmentActions {
    GetAssessment = "[Assessment] Get Assessment",
    GetAssessmentSuccess = "[Assessment] Get Assessment Success",
    GetAssessmentFailure = "[Assessment] Get Assessment Failure",
    CreateAssessment = "[Assessment] Create Assessment",
}

export const loadAssessment = createAction(
    EAssessmentActions.GetAssessment,
    props<{courseId: number}>()
);
export const loadAssessmentSuccess = createAction(
    EAssessmentActions.GetAssessmentSuccess,
    props<{assessment: AssessmentDetailsVm}>()
);
export const loadAssessmentFailure = createAction(
    EAssessmentActions.GetAssessmentFailure,
    props<{errorObject: any}>()
);

export const createAssessment = createAction(
    EAssessmentActions.CreateAssessment,
    props<{createAssessmentDto: any}>()
);