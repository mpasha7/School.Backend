import { AssessmentDetailsVm } from "../../core/models/assessment.model";


export interface AssessmentState {
    assessment: AssessmentDetailsVm | null;
    loading: boolean;
    error: any;
}

export const initialAssessmentState: AssessmentState = {
    assessment: null,
    loading: false,
    error: null
}