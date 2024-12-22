import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'School.Angular';
  // client = inject(HttpClient);
  // baseUrl = 'https://localhost:7125/api';

  // private getCourses(): Observable<CourseListVm> {
  //   return this.client.get<CourseListVm>(`${this.baseUrl}/courses`);
  // };
  // courses$ = this.getCourses();

  // createCourseForm = new FormGroup({
  //   title: new FormControl<string>(''),
  //   description: new FormControl<string>(''),
  //   shortDescription: new FormControl<string | null>(null),
  //   publicDescription: new FormControl<string | null>(null),
  //   photoPath: new FormControl<string | null>(null),
  //   beginQuestionnaire: new FormControl<string | null>(null),
  //   endQuestionnaire: new FormControl<string | null>(null),
  // });

  // createCourse() {
  //   const createCourseDto = {
  //     title: this.createCourseForm.value.title,
  //     description: this.createCourseForm.value.description,
  //     shortDescription: this.createCourseForm.value.shortDescription,
  //     publicDescription: this.createCourseForm.value.publicDescription,
  //     photoPath: this.createCourseForm.value.photoPath,
  //     beginQuestionnaire: this.createCourseForm.value.beginQuestionnaire,
  //     endQuestionnaire: this.createCourseForm.value.endQuestionnaire,
  //   };
  //   this.client.post(`${this.baseUrl}/courses`, createCourseDto)
  //     .subscribe({
  //       next: (value) => {
  //         console.log(value);
  //         this.courses$ = this.getCourses();
  //         this.createCourseForm.reset();
  //       }
  //     });
  // }

  // deleteCourse(id: number) {
  //   if (!confirm()) { return; }

  //   this.client.delete(`${this.baseUrl}/courses/${id.toString()}`)
  //     .subscribe({
  //       next: (value) => {
  //         this.courses$ = this.getCourses();
  //       }
  //     })
  // }
}
