import { createFeatureSelector, createSelector } from "@ngrx/store";
import { MessagesState } from "./messages.state";


export const selectMessagesState = createFeatureSelector<MessagesState>('messagesStore');

export const selectMessageList = createSelector(
    selectMessagesState,
    (state: MessagesState) => state.messageList
);

export const selectMessageLoading = createSelector(
    selectMessagesState,
    (state: MessagesState) => state.loading
);

export const selectMessageError = createSelector(
    selectMessagesState,
    (state: MessagesState) => state.error
);