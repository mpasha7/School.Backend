import { createFeatureSelector, createSelector } from "@ngrx/store";
import { AppliesState } from "./applies.state";


export const selectAppliesState = createFeatureSelector<AppliesState>('appliesStore');

export const selectApplyList = createSelector(
    selectAppliesState,
    (state: AppliesState) => state.applyList
);

export const selectApplyLoading = createSelector(
    selectAppliesState,
    (state: AppliesState) => state.loading
);

export const selectApplyIsSuccess = createSelector(
    selectAppliesState,
    (state: AppliesState) => state.isSuccess
);

export const selectApplyError = createSelector(
    selectAppliesState,
    (state: AppliesState) => state.error
);