import { CourseEffects } from "./courses/courses.effects";
import { coursesReducer } from "./courses/courses.reducer";
import { CoursesState } from "./courses/courses.state";
import { LessonEffects } from "./lessons/lessons.effects";
import { lessonsReducer } from "./lessons/lessons.reducer";
import { LessonsState } from "./lessons/lessons.state";


export interface AppState {
    coursesState: CoursesState;
    lessonsState: LessonsState;
}

export const store = {
    coursesStore: coursesReducer,
    lessonsStore: lessonsReducer
}

export const appEffects = [
    CourseEffects,
    LessonEffects
]