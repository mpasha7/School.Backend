import { createAction, props } from "@ngrx/store";
import { ReportDetailsVm, ReportListVm } from "../../core/models/report.model";


export enum EReportActions {
    GetReportList = "[Report] Get Report List",
    GetReportListSuccess = "[Report] Get Report List Success",
    GetReportListFailure = "[Report] Get Report List Failure",
    GetReport = "[Report] Get Report",
    GetReportSuccess = "[Report] Get Report Success",
    GetReportFailure = "[Report] Get Report Failure",
    CreateReport = "[Report] Create Report"
}

export const loadReportList = createAction(
    EReportActions.GetReportList,
    props<{courseId: number}>()
);
export const loadReportListSuccess = createAction(
    EReportActions.GetReportListSuccess,
    props<{reportList: ReportListVm}>()
);
export const loadReportListFailure = createAction(
    EReportActions.GetReportListFailure,
    props<{errorObject: any}>()
);

export const loadReport = createAction(
    EReportActions.GetReport,
    props<{courseId: number, lessonId: number, id: number}>()
);
export const loadReportSuccess = createAction(
    EReportActions.GetReportSuccess,
    props<{report: ReportDetailsVm}>()
);
export const loadReportFailure = createAction(
    EReportActions.GetReportFailure,
    props<{errorObject: any}>()
);

export const createReport = createAction(
    EReportActions.CreateReport,
    props<{createReportDto: any}>()
);