export interface ApplyListVm {
    applies: ApplyLookupDto[];
}

export interface ApplyLookupDto {
    id: number;
    studentGuid: string;
    studentName: string;
}