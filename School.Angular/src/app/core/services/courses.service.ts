import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CourseDetailsVm, CourseListVm } from '../models/course.model';
import { Observable } from 'rxjs';
import { ResponseDto } from '../models/response.model';
import { EnvironmentUrlService } from './environment-url/environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  basePath = 'api/courses';

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  getCourseList(): Observable<CourseListVm> {
    return this.http.get<CourseListVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath)
    );
  }

  getCourse(id: number): Observable<CourseDetailsVm> {
    return this.http.get<CourseDetailsVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${id}`)
    );
  }

  createCourse(createCourseFormData: FormData): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      createCourseFormData);
  }

  updateCourse(updateCourseFormData: FormData): Observable<ResponseDto> {
    return this.http.put<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      updateCourseFormData);
  }

  deleteCourse(id: number): Observable<ResponseDto> {
    return this.http.delete<ResponseDto>(      
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${id}`)
    );
  }

  private createCompleteRoute = (envAddress: string, route: string) => {
    return `${envAddress}/${route}`;
  }
}
