import { CourseDetailsVm, CourseLookupDto } from "../../core/models/course.model";


export interface CoursesState {
    courseList: CourseLookupDto[];
    selectedCourse: CourseDetailsVm | null;
    loading: boolean;
    error: any;
}

export const initialCoursesState: CoursesState = {
    courseList: [],
    selectedCourse: null,
    loading: false,
    error: null
}