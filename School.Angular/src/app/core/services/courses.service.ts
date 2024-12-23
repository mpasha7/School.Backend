import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CourseDetailsVm, CourseListVm } from '../models/course.model';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { ResponseDto } from '../models/response.model';

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

  createCourse(createCourseDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(this.baseUrl, createCourseDto);
  }

  updateCourse(updateCourseDto: any): Observable<ResponseDto> {
    return this.http.put<ResponseDto>(this.baseUrl, updateCourseDto);
  }

  deleteCourse(id: number): Observable<ResponseDto> {
    return this.http.delete<ResponseDto>(`${this.baseUrl}/${id}`);
  }
}
