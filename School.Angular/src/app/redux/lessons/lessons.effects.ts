import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { LessonsService } from "../../core/services/lessons/lessons.service";
import { createLesson, deleteLesson, loadLesson, loadLessonFailure, loadLessonList, loadLessonListFailure, loadLessonListSuccess, loadLessonSuccess, updateLesson } from "./lessons.actions";
import { catchError, map, mergeMap, of, switchMap } from "rxjs";


@Injectable()
export class LessonEffects {
    _loadLessonList$;
    _loadLesson$;
    _createLesson$;
    _updateLesson$;
    _deleteLesson$;

    constructor(
        private actions$: Actions,
        private lessonsService: LessonsService
    ) {
        this._loadLessonList$ = createEffect(() => actions$.pipe(
            ofType(loadLessonList),
            mergeMap(({courseId}) => this.lessonsService.getLessonList(courseId).pipe(
                map((data) => loadLessonListSuccess({lessonList: data})),
                catchError((error) => of(loadLessonListFailure({errorObject: error})))
            ))
        ));
        
        this._loadLesson$ = createEffect(() => actions$.pipe(
            ofType(loadLesson),
            mergeMap(({id, courseId}) => this.lessonsService.getLesson(id, courseId).pipe(
                map((data) => loadLessonSuccess({lesson: data})),
                catchError((error) => of(loadLessonFailure({errorObject: error})))
            ))
        ));

        this._createLesson$ = createEffect(() => actions$.pipe(
            ofType(createLesson),
            mergeMap(({createLessonDto}) => this.lessonsService.createLesson(createLessonDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadLessonList({courseId: createLessonDto.courseId})
                    : loadLessonListFailure({errorObject: data})
                )),
                catchError((error) => of(loadLessonListFailure({errorObject: error})))
            ))
        ));

        this._updateLesson$ = createEffect(() => actions$.pipe(
            ofType(updateLesson),
            mergeMap(({updateLessonDto}) => this.lessonsService.updateLesson(updateLessonDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadLessonList({courseId: updateLessonDto.courseId})
                    : loadLessonListFailure({errorObject: data})
                )),
                catchError((error) => of(loadLessonListFailure({errorObject: error})))
            ))
        ));

        this._deleteLesson$ = createEffect(() => actions$.pipe(
            ofType(deleteLesson),
            mergeMap(({id, courseId}) => this.lessonsService.deleteLesson(id, courseId).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadLessonList({courseId: courseId})
                    : loadLessonListFailure({errorObject: data})
                )),
                catchError((error) => of(loadLessonListFailure({errorObject: error})))
            ))
        ));
    }
}