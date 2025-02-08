import { createAction, props } from "@ngrx/store";
import { ApplyListVm } from "../../core/models/apply.model";
import { ResponseDto } from "../../core/models/response.model";


export enum EApplyActions {
    GetApplyList = "[Apply] Get Apply List",
    GetApplyListSuccess = "[Apply] Get Apply List Success",
    GetApplyListFailure = "[Apply] Get Apply List Failure",
    CreateApply = "[Apply] Create Apply",
    UpdateApply = "[Apply] Update Apply",
    DeleteApply = "[Apply] Delete Apply"
}

export const loadApplyList = createAction(
    EApplyActions.GetApplyList,
    props<{courseId: number}>()
);
export const loadApplyListSuccess = createAction(
    EApplyActions.GetApplyListSuccess,
    props<{applyList: ApplyListVm}>()
);
export const loadApplyListFailure = createAction(
    EApplyActions.GetApplyListFailure,
    props<{errorObject: any}>()
);

export const createApply = createAction(
    EApplyActions.CreateApply,
    props<{createApplyDto: any}>()
);
export const createApplySuccess = createAction(
    EApplyActions.CreateApply,
    props<{responseDto: ResponseDto}>()
);
export const createApplyFailure = createAction(
    EApplyActions.CreateApply,
    props<{errorObject: any}>()
);

export const updateApply = createAction(
    EApplyActions.UpdateApply,
    props<{updateApplyDto: any}>()
);

export const deleteApply = createAction(
    EApplyActions.DeleteApply,
    props<{id: number, courseId: number}>()
);