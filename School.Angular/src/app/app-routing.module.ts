import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoursesComponent } from './courses/courses.component';
import { LessonsComponent } from './lessons/lessons.component';
import { CourseListComponent } from './courses/course-list/course-list.component';
import { CourseFormComponent } from './courses/course-form/course-form.component';
import { LessonListComponent } from './lessons/lesson-list/lesson-list.component';
import { LessonFormComponent } from './lessons/lesson-form/lesson-form.component';
import { LessonDetailsComponent } from './lessons/lesson-details/lesson-details.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { UnauthorizedComponent } from './error-pages/unauthorized/unauthorized.component';
import { SigninRedirectCallbackComponent } from './redirects/signin-redirect-callback/signin-redirect-callback.component';
import { SignoutRedirectCallbackComponent } from './redirects/signout-redirect-callback/signout-redirect-callback.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './public-pages/home/home.component';
import { PublicCourseComponent } from './public-pages/public-course/public-course.component';
import { LogoutComponent } from './logout/logout.component';
import { StudentsComponent } from './students/students.component';
import { StudentListComponent } from './students/student-list/student-list.component';
import { StudentToCourseComponent } from './students/student-to-course/student-to-course.component';
import { StudentFromCourseComponent } from './students/student-from-course/student-from-course.component';
import { CourseMessagesComponent } from './courses/course-messages/course-messages.component';
import { CourseAppliesComponent } from './courses/course-applies/course-applies.component';
import { CourseReportsComponent } from './courses/course-reports/course-reports.component';
import { CourseCommentsComponent } from './courses/course-comments/course-comments.component';
import { CourseQuestComponent } from './courses/course-quest/course-quest.component';

const courseRoutes: Routes = [
  { path: 'list', component: CourseListComponent },
  { path: 'form/:id', component: CourseFormComponent,
        canActivate: [AuthGuard], data: { roles: ['Coach'] } },
  { path: 'messages/:id', component: CourseMessagesComponent },
  { path: 'applies/:id', component: CourseAppliesComponent,
        canActivate: [AuthGuard], data: { roles: ['Coach'] } },
  { path: 'reports/:id', component: CourseReportsComponent,
        canActivate: [AuthGuard], data: { roles: ['Coach'] } },
  { path: 'comments/:id', component: CourseCommentsComponent,
        canActivate: [AuthGuard], data: { roles: ['Coach'] } },
  { path: 'quest/:id', component: CourseQuestComponent },
  { path: '', redirectTo: '/courses/list', pathMatch: 'full'},
  { path: '**', redirectTo: '/404', pathMatch: 'full' }
]

const lessonRoutes: Routes = [
  { path: 'list', component: LessonListComponent },
  { path: 'form/:id', component: LessonFormComponent,
        canActivate: [AuthGuard], data: { roles: ['Coach'] } },
  { path: 'details/:id', component: LessonDetailsComponent },
  { path: '', redirectTo: '/lessons/list', pathMatch: 'full'},
  { path: '**', redirectTo: '/404', pathMatch: 'full' }
]

const studentRoutes: Routes = [
  { path: 'list', component: StudentListComponent },
  { path: 'to-course', component: StudentToCourseComponent },  
  { path: 'from-course', component: StudentFromCourseComponent },
  { path: '', redirectTo: '/students/list', pathMatch: 'full'},
  { path: '**', redirectTo: '/404', pathMatch: 'full' }
]

const routes: Routes = [
  { path: 'courses', component: CoursesComponent, children: courseRoutes, 
        canActivate: [AuthGuard], data: { roles: ['Coach', 'Student'] } },
  { path: 'lessons/:courseid', component: LessonsComponent, children: lessonRoutes, 
        canActivate: [AuthGuard], data: { roles: ['Coach', 'Student'] } },
  { path: 'students', component: StudentsComponent, children: studentRoutes,
        canActivate: [AuthGuard], data: { roles: ['Coach'] } },
  { path: 'home', component: HomeComponent },
  { path: 'home/:id', component: PublicCourseComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'signin-callback', component: SigninRedirectCallbackComponent },
  { path: 'signout-callback', component: SignoutRedirectCallbackComponent },  
  { path: '404', component: NotFoundComponent },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full'},
  { path: '**', redirectTo: '/404', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
