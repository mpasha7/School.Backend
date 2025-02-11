export interface CommentListVm {
    comments: CommentLookupDto[];
}

export interface CommentLookupDto {
    id: number;

    studentName: string;
    createdAt: Date;
    text: string;
    isPublic: boolean;
}