import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoursesComponent } from './courses/courses.component';
import { LessonsComponent } from './lessons/lessons.component';
import { CourseListComponent } from './courses/course-list/course-list.component';
import { CourseFormComponent } from './courses/course-form/course-form.component';

const courseRoutes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "/list"},
  { path: "list", component: CourseListComponent },
  { path: "form/:id", component: CourseFormComponent }
]

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "/courses/list"},
  { path: "courses", component: CoursesComponent, children: courseRoutes },
  { path: "lessons", component: LessonsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
