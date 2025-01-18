import { PublicCourseDetailsVm, PublicCourseLookupDto } from "../../core/models/course.model";


export interface HomeState {
    publicCourseList: PublicCourseLookupDto[];
    publicCourse: PublicCourseDetailsVm | null;
    loading: boolean;
    error: any;
}

export const initialHomeState: HomeState = {
    publicCourseList: [],
    publicCourse: null,
    loading: false,
    error: null
}