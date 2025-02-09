import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { FeedbacksService } from "../../core/services/feedbacks/feedbacks.service";
import { createFeedback, loadFeedback, loadFeedbackFailure, loadFeedbackSuccess } from "./feedbacks.actions";
import { catchError, map, mergeMap, of, switchMap } from "rxjs";


@Injectable()
export class FeedbackEffects {
    _loadFeedback$;
    _createFeedback$;

    constructor(
        private actions$: Actions,
        private feedbacksService: FeedbacksService
    ) {
        this._loadFeedback$ = createEffect(() => actions$.pipe(
            ofType(loadFeedback),
            mergeMap(({courseId, lessonId, reportId}) => this.feedbacksService.getFeedback(courseId, lessonId, reportId).pipe(
                map((data) => loadFeedbackSuccess({feedback: data})),
                catchError((error) => of(loadFeedbackFailure({errorObject: error})))
            ))
        ));

        this._createFeedback$ = createEffect(() => actions$.pipe(
            ofType(createFeedback),
            mergeMap(({createFeedbackDto}) => this.feedbacksService.createFeedback(createFeedbackDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadFeedbackFailure({errorObject: null})
                    : loadFeedbackFailure({errorObject: data})
                )),
                catchError((error) => of(loadFeedbackFailure({errorObject: error})))
            ))
        ));
    }
}