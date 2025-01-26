import { CourseEffects } from "./courses/courses.effects";
import { coursesReducer } from "./courses/courses.reducer";
import { CoursesState } from "./courses/courses.state";
import { HomeEffects } from "./home/home.effects";
import { homeReducer } from "./home/home.reducer";
import { HomeState } from "./home/home.state";
import { LessonEffects } from "./lessons/lessons.effects";
import { lessonsReducer } from "./lessons/lessons.reducer";
import { LessonsState } from "./lessons/lessons.state";
import { StudentEffects } from "./students/students.effects";
import { studentsReducer } from "./students/students.reducer";
import { StudentsState } from "./students/students.state";


export interface AppState {
    coursesState: CoursesState;
    lessonsState: LessonsState;
    homeState: HomeState;
    studentsState: StudentsState;
}

export const store = {
    coursesStore: coursesReducer,
    lessonsStore: lessonsReducer,
    homeStore: homeReducer,
    studentsStore: studentsReducer
}

export const appEffects = [
    CourseEffects,
    LessonEffects,
    HomeEffects,
    StudentEffects
]