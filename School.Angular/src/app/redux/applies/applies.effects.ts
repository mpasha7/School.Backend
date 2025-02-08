import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { AppliesService } from "../../core/services/applies/applies.service";
import { createApply, createApplyFailure, createApplySuccess, deleteApply, loadApplyList, loadApplyListFailure, loadApplyListSuccess, updateApply } from "./applies.actions";
import { catchError, map, mergeMap, of, switchMap } from "rxjs";


@Injectable()
export class ApplyEffects {
    _loadApplyList$;
    _createApply$;
    _updateApply$;
    _deleteApply$;

    constructor(
        private actions$: Actions,
        private appliesService: AppliesService
    ) {
        this._loadApplyList$ = createEffect(() => actions$.pipe(
            ofType(loadApplyList),
            mergeMap(({courseId}) => this.appliesService.getApplyList(courseId).pipe(
                map((data) => loadApplyListSuccess({applyList: data})),
                catchError((error) => of(loadApplyListFailure({errorObject: error})))
            ))
        ));

        this._createApply$ = createEffect(() => actions$.pipe(
            ofType(createApply),
            mergeMap(({createApplyDto}) => this.appliesService.createApply(createApplyDto).pipe(
                // map((data) => createApplySuccess({responseDto: data})),
                switchMap((data) => of(data.isSuccess
                    ? loadApplyListFailure({errorObject: null})
                    : loadApplyListFailure({errorObject: data})
                )),
                catchError((error) => of(loadApplyListFailure({errorObject: error})))
            ))
        ));

        this._updateApply$ = createEffect(() => actions$.pipe(
            ofType(updateApply),
            mergeMap(({updateApplyDto}) => this.appliesService.updateApply(updateApplyDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadApplyList({courseId: updateApplyDto.courseId})
                    : loadApplyListFailure({errorObject: data})
                )),
                catchError((error) => of(loadApplyListFailure({errorObject: error})))
            ))
        ));

        this._deleteApply$ = createEffect(() => actions$.pipe(
            ofType(deleteApply),
            mergeMap(({id, courseId}) => this.appliesService.deleteApply(id, courseId).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadApplyList({courseId: courseId})
                    : loadApplyListFailure({errorObject: data})
                )),
                catchError((error) => of(loadApplyListFailure({errorObject: error})))
            ))
        ));
    }
}