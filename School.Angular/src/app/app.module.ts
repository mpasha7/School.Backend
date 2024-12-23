import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient } from '@angular/common/http';
import { CoursesComponent } from './courses/courses.component';
import { CourseListComponent } from './courses/course-list/course-list.component';
import { CourseFormComponent } from './courses/course-form/course-form.component';
import { LessonsComponent } from './lessons/lessons.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { LayoutModule } from './layout/layout.module';
import { StoreModule } from '@ngrx/store';
import { appEffects, store } from './redux/store';
import { EffectsModule } from '@ngrx/effects';

@NgModule({
  declarations: [
    AppComponent,
    // HeaderComponent,
    // FooterComponent,
    // CoursesComponent,
    // CourseListComponent,
    // CourseFormComponent,
    // LessonsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AsyncPipe, 
    FormsModule, 
    ReactiveFormsModule,
    LayoutModule,
    StoreModule.forRoot(store),
    EffectsModule.forRoot(appEffects)
  ],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent]
})
export class AppModule { }
