import { Actions, createEffect, ofType } from "@ngrx/effects";
import { HomeService } from "../../core/services/home/home.service";
import { loadPublicCourse, loadPublicCourseFailure, loadPublicCourseList, loadPublicCourseListFailure, loadPublicCourseListSuccess, loadPublicCourseSuccess } from "./home.actions";
import { catchError, map, mergeMap, of } from "rxjs";
import { Injectable } from "@angular/core";


@Injectable()
export class HomeEffects {
    _loadPublicCourseList$;
    _loadPublicCourse$;

    constructor(
        private actions$: Actions,
        private homeService: HomeService
    ) {
        this._loadPublicCourseList$ = createEffect(() => actions$.pipe(
            ofType(loadPublicCourseList),
            mergeMap(() => this.homeService.getPublicCourseList().pipe(
                map((data) => loadPublicCourseListSuccess({publicCourseList: data.courses})),
                catchError((error) => of(loadPublicCourseListFailure({errorObject: error})))
            ))
        ));

        this._loadPublicCourse$ = createEffect(() => actions$.pipe(
            ofType(loadPublicCourse),
            mergeMap(({id}) => this.homeService.getPublicCourse(id).pipe(
                map((data) => loadPublicCourseSuccess({publicCourse: data})),
                catchError((error) => of(loadPublicCourseFailure({errorObject: error})))
            ))
        ));
    }
}