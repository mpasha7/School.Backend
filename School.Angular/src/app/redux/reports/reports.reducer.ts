import { createReducer, on } from "@ngrx/store";
import { initialReportsState } from "./reports.state";
import { loadReport, loadReportFailure, loadReportList, loadReportListFailure, loadReportListSuccess, loadReportSuccess } from "./reports.actions";


export const reportsReducer = createReducer(
    initialReportsState,
    on(loadReportList, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadReportListSuccess, (state, action) => {
        return {
            ...state,
            reportsList: [...action.reportList.reports],
            maxNumber: action.reportList.maxLessonNumber,
            loading: false
        }
    }),
    on(loadReportListFailure, (state, action) => {
        return {
            ...state,
            reportsList: [],
            maxNumber: 0,
            loading: false,
            error: action.errorObject
        }
    }),
    on(loadReport, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadReportSuccess, (state, action) => {
        return {
            ...state,
            selectedReport: action.report,
            loading: false
        }
    }),
    on(loadReportFailure, (state, action) => {
        return {
            ...state,
            selectedReport: null,
            loading: false,
            error: action.errorObject
        }
    })
);