import { ApplyEffects } from "./applies/applies.effects";
import { appliesReducer } from "./applies/applies.reducer";
import { AppliesState } from "./applies/applies.state";
import { AssessmentEffects } from "./assessments/assessments.effects";
import { assessmentReducer } from "./assessments/assessments.reducer";
import { AssessmentState } from "./assessments/assessments.state";
import { CourseEffects } from "./courses/courses.effects";
import { coursesReducer } from "./courses/courses.reducer";
import { CoursesState } from "./courses/courses.state";
import { FeedbackEffects } from "./feedbacks/feedbacks.effects";
import { feedbacksReducer } from "./feedbacks/feedbacks.reducer";
import { FeedbackState } from "./feedbacks/feedbacks.state";
import { HomeEffects } from "./home/home.effects";
import { homeReducer } from "./home/home.reducer";
import { HomeState } from "./home/home.state";
import { LessonEffects } from "./lessons/lessons.effects";
import { lessonsReducer } from "./lessons/lessons.reducer";
import { LessonsState } from "./lessons/lessons.state";
import { MessageEffects } from "./messages/messages.effects";
import { messagesReducer } from "./messages/messages.reducer";
import { MessagesState } from "./messages/messages.state";
import { ReportEffects } from "./reports/reports.effects";
import { reportsReducer } from "./reports/reports.reducer";
import { ReportsState } from "./reports/reports.state";
import { StudentEffects } from "./students/students.effects";
import { studentsReducer } from "./students/students.reducer";
import { StudentsState } from "./students/students.state";


export interface AppState {
    coursesState: CoursesState;
    lessonsState: LessonsState;
    homeState: HomeState;
    studentsState: StudentsState;
    messagesState: MessagesState;
    appliesState: AppliesState;
    reportsState: ReportsState;
    feedbackState: FeedbackState;
    assessmentState: AssessmentState;
}

export const store = {
    coursesStore: coursesReducer,
    lessonsStore: lessonsReducer,
    homeStore: homeReducer,
    studentsStore: studentsReducer,
    messagesStore: messagesReducer,
    appliesStore: appliesReducer,
    reportsStore: reportsReducer,
    feedbackStore: feedbacksReducer,
    assessmentStore: assessmentReducer
}

export const appEffects = [
    CourseEffects,
    LessonEffects,
    HomeEffects,
    StudentEffects,
    MessageEffects,
    ApplyEffects,
    ReportEffects,
    FeedbackEffects,
    AssessmentEffects
]