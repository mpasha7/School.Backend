import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { ReportsService } from "../../core/services/reports/reports.service";
import { createReport, loadReport, loadReportFailure, loadReportList, loadReportListFailure, loadReportListSuccess, loadReportSuccess } from "./reports.actions";
import { catchError, map, mergeMap, of, switchMap } from "rxjs";
import { loadLesson, loadLessonListFailure } from "../lessons/lessons.actions";


@Injectable()
export class ReportEffects {
    _loadReportList$;
    _loadReport$;
    _createReport$;

    constructor(
        private actions$: Actions,
        private reportsService: ReportsService
    ) {
        this._loadReportList$ = createEffect(() => actions$.pipe(
            ofType(loadReportList),
            mergeMap(({courseId}) => this.reportsService.getReportList(courseId).pipe(
                map((data) => loadReportListSuccess({reportList: data})),
                catchError((error) => of(loadReportListFailure({errorObject: error})))
            ))
        ));

        this._loadReport$ = createEffect(() => actions$.pipe(
            ofType(loadReport),
            mergeMap(({courseId, lessonId, id}) => this.reportsService.getReport(courseId, lessonId, id).pipe(
                map((data) => loadReportSuccess({report: data})),
                catchError((error) => of(loadReportFailure({errorObject: error})))
            ))
        ));

        this._createReport$ = createEffect(() => actions$.pipe(
            ofType(createReport),
            mergeMap(({createReportDto}) => this.reportsService.createReport(createReportDto).pipe(
                switchMap((data) => of(data.isSuccess
                    // ? loadLesson({id: createReportDto.lessonid as number, courseId: createReportDto.courseid as number})
                    ? loadReportListFailure({errorObject: null})
                    : loadReportListFailure({errorObject: data})
                )),
                catchError((error) => of(loadReportListFailure({errorObject: error})))
            ))
        ));
        // this._createReport$ = createEffect(() => actions$.pipe(
        //     ofType(createReport),
        //     mergeMap(({createReportDto}) => this.reportsService.createReport(createReportDto).pipe(
        //         map((data) => loadLesson({id: createReportDto.lessonid as number, courseId: createReportDto.courseid as number})),
        //         catchError((error) => of(loadReportListFailure({errorObject: error})))
        //     ))
        // ));
    }
}