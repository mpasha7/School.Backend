import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { AssessmentsService } from "../../core/services/assessments/assessments.service";
import { createAssessment, loadAssessment, loadAssessmentFailure, loadAssessmentSuccess } from "./assessments.actions";
import { catchError, map, mergeMap, of, switchMap } from "rxjs";


@Injectable()
export class AssessmentEffects {
    _loadAssessment$;
    _createAssessment$;

    constructor(
        private actions$: Actions,
        private assessmentService: AssessmentsService
    ) {
        this._loadAssessment$ = createEffect(() => actions$.pipe(
            ofType(loadAssessment),
            mergeMap(({courseId}) => this.assessmentService.getAssessment(courseId).pipe(
                map((data) => loadAssessmentSuccess({assessment: data})),
                catchError((error) => of(loadAssessmentFailure({errorObject: error})))
            ))
        ));

        this._createAssessment$ = createEffect(() => actions$.pipe(
            ofType(createAssessment),
            mergeMap(({createAssessmentDto}) => this.assessmentService.createAssessment(createAssessmentDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadAssessmentFailure({errorObject: null})
                    : loadAssessmentFailure({errorObject: data})
                )),
                catchError((error) => of(loadAssessmentFailure({errorObject: error})))
            ))
        ));
    }
}