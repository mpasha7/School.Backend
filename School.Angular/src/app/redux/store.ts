import { CourseEffects } from "./courses/courses.effects";
import { coursesReducer } from "./courses/courses.reducer";
import { CoursesState } from "./courses/courses.state";
import { HomeEffects } from "./home/home.effects";
import { homeReducer } from "./home/home.reducer";
import { HomeState } from "./home/home.state";
import { LessonEffects } from "./lessons/lessons.effects";
import { lessonsReducer } from "./lessons/lessons.reducer";
import { LessonsState } from "./lessons/lessons.state";


export interface AppState {
    coursesState: CoursesState;
    lessonsState: LessonsState;
    homeState: HomeState;
}

export const store = {
    coursesStore: coursesReducer,
    lessonsStore: lessonsReducer,
    homeStore: homeReducer
}

export const appEffects = [
    CourseEffects,
    LessonEffects,
    HomeEffects
]