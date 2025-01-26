import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { StudentsService } from "../../core/services/students/students.service";
import { catchError, map, mergeMap, of, switchMap } from "rxjs";
import { 
    addStudentToCourse,
    loadStudentIds,
    loadStudentIdsFailed,
    loadStudentIdsSuccess,
    loadStudentListByIds,
    loadStudentListByIdsFailed,
    loadStudentListByIdsSuccess,
    loadStudentListBySearch,
    loadStudentListBySearchFailed,
    loadStudentListBySearchSuccess,
    removeStudentFromCourse
} from "./students.actions";


@Injectable()
export class StudentEffects {
    _loadStudentsIds$;
    _addStudentToCourse$;
    _removeStudentFromCourse$;

    //////////
    _loadStudentListByIds$;
    _loadStudentListBySearch$;

    constructor(
        private actions$: Actions,
        private studentsService: StudentsService
    ) {
        this._loadStudentsIds$ = createEffect(() => actions$.pipe(
            ofType(loadStudentIds),
            mergeMap(() => this.studentsService.getStudentsIds().pipe(
                map((data) => loadStudentIdsSuccess({studentIdsVm: data})),
                catchError((error) => of(loadStudentIdsFailed({errorObject: error})))
            ))
        ));

        this._addStudentToCourse$ = createEffect(() => actions$.pipe(
            ofType(addStudentToCourse),
            mergeMap(({addToCourseDto}) => this.studentsService.addStudentToCourse(addToCourseDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadStudentIds()
                    : loadStudentIdsFailed({errorObject: data})
                )),
                catchError((error) => of(loadStudentIdsFailed({errorObject: error})))
            ))
        ));

        this._removeStudentFromCourse$ = createEffect(() => actions$.pipe(
            ofType(removeStudentFromCourse),
            mergeMap(({removeFromCourseDto}) => this.studentsService.removeStudentFromCourse(removeFromCourseDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadStudentIds()
                    : loadStudentIdsFailed({errorObject: data})
                )),
                catchError((error) => of(loadStudentIdsFailed({errorObject: error})))
            ))
        ));


        //////////////
        this._loadStudentListByIds$ = createEffect(() => actions$.pipe(
            ofType(loadStudentListByIds),
            mergeMap(({ids}) => this.studentsService.getStudentsByIds(ids).pipe(
                map((data) => loadStudentListByIdsSuccess({students: data})),
                catchError((error) => of(loadStudentListByIdsFailed({errorObject: error})))
            ))
        ));

        this._loadStudentListBySearch$ = createEffect(() => actions$.pipe(
            ofType(loadStudentListBySearch),
            mergeMap(({search}) => this.studentsService.getStudentsBySearch(search).pipe(
                map((data) => loadStudentListBySearchSuccess({students: data})),
                catchError((error) => of(loadStudentListBySearchFailed({errorObject: error})))
            ))
        ));
    }
}