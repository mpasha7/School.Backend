import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ReportsState } from "./reports.state";


export const selectReportsState = createFeatureSelector<ReportsState>('reportsStore');

export const selectReportList = createSelector(
    selectReportsState,
    (state: ReportsState) => state.reportsList
);

export const selectMaxNumber = createSelector(
    selectReportsState,
    (state: ReportsState) => state.maxNumber
);

export const selectReport = createSelector(
    selectReportsState,
    (state: ReportsState) => state.selectedReport
);

export const selectReportLoading = createSelector(
    selectReportsState,
    (state: ReportsState) => state.loading
);

export const selectReportError = createSelector(
    selectReportsState,
    (state: ReportsState) => state.error
);