import { createReducer, on } from "@ngrx/store";
import { initialCommentsState } from "./comments.state";
import { loadCommentList, loadCommentListFailure, loadCommentListSuccess } from "./comments.actions";


export const commentsReducer = createReducer(
    initialCommentsState,
    on(loadCommentList, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadCommentListSuccess, (state, action) => {
        return {
            ...state,
            commentList: [...action.commentList.comments],
            loading: false
        }
    }),
    on(loadCommentListFailure, (state, action) => {
        return {
            ...state,
            commentList: [],
            loading: false,
            error: action.errorObject
        }
    })
);