import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CourseDetailsVm, CourseListVm } from '../models/course.model';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  baseUrl = 'https://localhost:7125/api/courses';

  constructor(private http: HttpClient) { }

  getCourseList(): Observable<CourseListVm> {
    return this.http.get<CourseListVm>(this.baseUrl);
  }

  getCourse(id: number): Observable<CourseDetailsVm> {
    return this.http.get<CourseDetailsVm>(`${this.baseUrl}/${id}`);
  }

  createCourse(createCourseDto: any): Observable<number> {
    return this.http.post<number>(this.baseUrl, createCourseDto);
  }

  updateCourse(updateCourseDto: any) {
    return this.http.put(this.baseUrl, updateCourseDto);
  }

  deleteCourse(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
