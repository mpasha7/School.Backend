import { FeedbackDetailsVm } from "../../core/models/feedback.model";


export interface FeedbackState {
    feedback: FeedbackDetailsVm | null;
    loading: boolean;
    error: any;
}

export const initialFeedbackState: FeedbackState = {
    feedback: null,
    loading: false,
    error: null
}