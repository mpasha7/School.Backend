import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { CommentsService } from "../../core/services/comments/comments.service";
import { createComment, loadCommentList, loadCommentListFailure, loadCommentListSuccess, updateComment } from "./comments.actions";
import { catchError, map, mergeMap, of, switchMap } from "rxjs";


@Injectable()
export class CommentEffects {
    _loadCommentList$;
    _createComment$;
    _updateComment$;

    constructor(
        private actions$: Actions,
        private commentsService: CommentsService
    ) {
        this._loadCommentList$ = createEffect(() => actions$.pipe(
            ofType(loadCommentList),
            mergeMap(({courseId}) => this.commentsService.getCommentList(courseId).pipe(
                map((data) => loadCommentListSuccess({commentList: data})),
                catchError((error) => of(loadCommentListFailure({errorObject: error})))
            ))
        ));

        this._createComment$ = createEffect(() => actions$.pipe(
            ofType(createComment),
            mergeMap(({createCommentDto}) => this.commentsService.createComment(createCommentDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadCommentListFailure({errorObject: null})
                    : loadCommentListFailure({errorObject: data})
                )),
                catchError((error) => of(loadCommentListFailure({errorObject: error})))
            ))
        ));

        this._updateComment$ = createEffect(() => actions$.pipe(
            ofType(updateComment),
            mergeMap(({updateCommentDto}) => this.commentsService.updateComment(updateCommentDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadCommentList({courseId: updateCommentDto.courseId})
                    : loadCommentListFailure({errorObject: data})
                )),
                catchError((error) => of(loadCommentListFailure({errorObject: error})))
            ))
        ));
    }
}