import { CourseEffects } from "./courses/courses.effects";
import { coursesReducer } from "./courses/courses.reducer";
import { CoursesState } from "./courses/courses.state";


export interface AppState {
    coursesState: CoursesState;
}

export const store = {
    coursesStore: coursesReducer
}

export const appEffects = [
    CourseEffects
]