import { CommentLookupDto } from "../../core/models/comment.model";


export interface CommentsState {
    commentList: CommentLookupDto[];
    loading: boolean;
    error: any;
}

export const initialCommentsState: CommentsState = {
    commentList: [],
    loading: false,
    error: null
}