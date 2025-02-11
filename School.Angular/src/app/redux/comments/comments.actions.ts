import { createAction, props } from "@ngrx/store";
import { CommentListVm, CommentLookupDto } from "../../core/models/comment.model";


export enum ECommentActions {
    GetCommentList = "[Comment] Get Comment List",
    GetCommentListSuccess = "[Comment] Get Comment List Success",
    GetCommentListFailure = "[Comment] Get Comment List Failure",
    CreateComment = "[Comment] Create Comment",
    UpdateComment = "[Comment] Update Comment"
}

export const loadCommentList = createAction(
    ECommentActions.GetCommentList,
    props<{courseId: number}>()
);
export const loadCommentListSuccess = createAction(
    ECommentActions.GetCommentListSuccess,
    props<{commentList: CommentListVm}>()
);
export const loadCommentListFailure = createAction(
    ECommentActions.GetCommentListFailure,
    props<{errorObject: any}>()
);

export const createComment = createAction(
    ECommentActions.CreateComment,
    props<{createCommentDto: any}>()
);

export const updateComment = createAction(
    ECommentActions.UpdateComment,
    props<{updateCommentDto: any}>()
);