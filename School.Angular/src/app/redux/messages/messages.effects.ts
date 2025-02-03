import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { MessagesService } from "../../core/services/messages/messages.service";
import { createMessage, deleteMessage, loadMessageList, loadMessageListFailure, loadMessageListSuccess } from "./messages.actions";
import { catchError, map, mergeMap, of, switchMap } from "rxjs";


@Injectable()
export class MessageEffects {
    _loadMessageList$;
    _createMessage$;
    _deleteMessage$;

    constructor(
        private actions$: Actions,
        private messagesService: MessagesService
    ) {
        this._loadMessageList$ = createEffect(() => actions$.pipe(
            ofType(loadMessageList),
            mergeMap(({courseId}) => this.messagesService.getMessageList(courseId).pipe(
                map((data) => loadMessageListSuccess({messageList: data})),
                catchError((error) => of(loadMessageListFailure({errorObject: error})))
            ))
        ));

        this._createMessage$ = createEffect(() => actions$.pipe(
            ofType(createMessage),
            mergeMap(({createMessageDto}) => this.messagesService.createMessage(createMessageDto).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadMessageList({courseId: createMessageDto.courseId})
                    : loadMessageListFailure({errorObject: data})
                )),
                catchError((error) => of(loadMessageListFailure({errorObject: error})))
            ))
        ));

        this._deleteMessage$ = createEffect(() => actions$.pipe(
            ofType(deleteMessage),
            mergeMap(({id, courseId}) => this.messagesService.deleteMessage(id, courseId).pipe(
                switchMap((data) => of(data.isSuccess
                    ? loadMessageList({courseId: courseId})
                    : loadMessageListFailure({errorObject: data})
                )),
                catchError((error) => of(loadMessageListFailure({errorObject: error})))
            ))
        ));
    }
}