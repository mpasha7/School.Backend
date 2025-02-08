import { ReportDetailsVm, ReportLookupDto } from "../../core/models/report.model";


export interface ReportsState {
    reportsList: ReportLookupDto[];
    maxNumber: number;
    selectedReport: ReportDetailsVm | null;
    loading: boolean;
    error: any;
}

export const initialReportsState: ReportsState = {
    reportsList: [],
    maxNumber: 0,
    selectedReport: null,
    loading: false,
    error: null
}