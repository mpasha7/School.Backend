import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { CourseListVm } from '../models/course.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'School.Angular';
  client = inject(HttpClient);

  private getContacts(): Observable<CourseListVm> {
    return this.client.get<CourseListVm>('https://localhost:7125/api/courses');
  };
  courses$ = this.getContacts();
}
