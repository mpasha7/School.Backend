import { createAction, props } from "@ngrx/store";
import { MessageListVm } from "../../core/models/message.model";


export enum EMessageActions {
    GetMessageList = "[Message] Get Message List",
    GetMessageListSuccess = "[Message] Get Message List Success",
    GetMessageListFailure = "[Message] Get Message List Failure",
    CreateMessage = "[Message] Create Message",
    DeleteMessage = "[Message] Delete Message"
}

export const loadMessageList = createAction(
    EMessageActions.GetMessageList,
    props<{courseId: number}>()
);
export const loadMessageListSuccess = createAction(
    EMessageActions.GetMessageListSuccess,
    props<{messageList: MessageListVm}>()
);
export const loadMessageListFailure = createAction(
    EMessageActions.GetMessageListFailure,
    props<{errorObject: any}>()
);

export const createMessage = createAction(
    EMessageActions.CreateMessage,
    props<{createMessageDto: any}>()
);

export const deleteMessage = createAction(
    EMessageActions.DeleteMessage,
    props<{id: number, courseId: number}>()
);