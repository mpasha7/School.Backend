import { createReducer, on } from "@ngrx/store";
import { initialMessagesState } from "./messages.state";
import { loadMessageList, loadMessageListFailure, loadMessageListSuccess } from "./messages.actions";


export const messagesReducer = createReducer(
    initialMessagesState,
    on(loadMessageList, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadMessageListSuccess, (state, action) => {
        return {
            ...state,
            messageList: [...action.messageList.messages],
            loading: false
        }
    }),
    on(loadMessageListFailure, (state, action) => {
        return {
            ...state,
            messageList: [],
            loading: false,
            error: action.errorObject
        }
    })
);