import { ApplyLookupDto } from "../../core/models/apply.model";


export interface AppliesState {
    applyList: ApplyLookupDto[];
    loading: boolean;
    isSuccess: boolean;
    error: any;
}

export const initialAppliesState: AppliesState = {
    applyList: [],
    loading: false,
    isSuccess: false,
    error: null
}