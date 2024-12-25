import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoursesComponent } from './courses/courses.component';
import { LessonsComponent } from './lessons/lessons.component';
import { CourseListComponent } from './courses/course-list/course-list.component';
import { CourseFormComponent } from './courses/course-form/course-form.component';
import { LessonListComponent } from './lessons/lesson-list/lesson-list.component';
import { LessonFormComponent } from './lessons/lesson-form/lesson-form.component';
import { LessonDetailsComponent } from './lessons/lesson-details/lesson-details.component';

const courseRoutes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "/list"},
  { path: "list", component: CourseListComponent },
  { path: "form/:id", component: CourseFormComponent }
]

const lessonRoutes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "/list"},
  { path: "list", component: LessonListComponent },
  { path: "form/:id", component: LessonFormComponent },
  { path: "details/:id", component: LessonDetailsComponent }
]

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "/courses/list"},
  { path: "courses", component: CoursesComponent, children: courseRoutes },
  { path: "lessons/:courseid", component: LessonsComponent, children: lessonRoutes }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
