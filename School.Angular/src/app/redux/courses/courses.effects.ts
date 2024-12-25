import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { CoursesService } from "../../core/services/courses.service";
import { createCourse, deleteCourse, loadCourse, loadCourseFailure, loadCourseList, loadCourseListFailure, loadCourseListSuccess, loadCourseSuccess, updateCourse } from "./courses.actions";
import { catchError, map, mergeMap, of, switchMap } from "rxjs";


@Injectable()
export class CourseEffects {
    _loadCourseList$;
    _loadCourse$;
    _createCourse$;
    _updateCourse$;
    _deleteCourse$;

    constructor(
        private actions$: Actions,
        private coursesService: CoursesService
    ) {
        this._loadCourseList$ = createEffect(() => actions$.pipe(
            ofType(loadCourseList),
            mergeMap(() => this.coursesService.getCourseList().pipe(
                map((data) => loadCourseListSuccess({courseList: data.courses})),
                catchError((error) => of(loadCourseListFailure({errorObject: error})))
            ))
        ));

        this._loadCourse$ = createEffect(() => actions$.pipe(
            ofType(loadCourse),
            mergeMap(({id}) => this.coursesService.getCourse(id).pipe(
                map((data) => loadCourseSuccess({course: data})),
                catchError((error) => of(loadCourseFailure({errorObject: error})))
            ))
        ));

        this._createCourse$ = createEffect(() => actions$.pipe(
            ofType(createCourse),
            mergeMap(({createCourseDto}) => this.coursesService.createCourse(createCourseDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadCourseList() // TODO: В любом случае сработает при загрузке CourseListComponent!!!
                    : loadCourseListFailure({errorObject: data})
                )),
                catchError((error) => of(loadCourseListFailure({errorObject: error})))
            ))
        ));

        this._updateCourse$ = createEffect(() => actions$.pipe(
            ofType(updateCourse),
            mergeMap(({updateCourseDto}) => this.coursesService.updateCourse(updateCourseDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadCourseList()
                    : loadCourseListFailure({errorObject: data})
                )),
                catchError((error) => of(loadCourseListFailure({errorObject: error})))
            ))
        ));

        this._deleteCourse$ = createEffect(() => actions$.pipe(
            ofType(deleteCourse),
            mergeMap(({id}) => this.coursesService.deleteCourse(id).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadCourseList()
                    : loadCourseListFailure({errorObject: data})
                )),
                catchError((error) => of(loadCourseListFailure({errorObject: error})))
            ))
        ));
    }
}