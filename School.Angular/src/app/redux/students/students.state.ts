import { Student, StudentLookupDto, StudentsIdsVm } from "../../core/models/student.model";


export interface StudentsState {
    studentsIds: StudentsIdsVm | null;
    studentList: Student[];
    loading: boolean;
    error: any;
}

export const initialStudentsState: StudentsState = {
    studentsIds: null,
    studentList: [],
    loading: false,
    error: null
}