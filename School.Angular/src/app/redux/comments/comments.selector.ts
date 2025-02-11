import { createFeatureSelector, createSelector } from "@ngrx/store";
import { CommentsState } from "./comments.state";


export const selectCommentsState = createFeatureSelector<CommentsState>('commentsStore');

export const selectCommentList = createSelector(
    selectCommentsState,
    (state: CommentsState) => state.commentList
);

export const selectCommentLoading = createSelector(
    selectCommentsState,
    (state: CommentsState) => state.loading
);

export const selectCommentError = createSelector(
    selectCommentsState,
    (state: CommentsState) => state.error
);