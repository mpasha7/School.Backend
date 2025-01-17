import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LessonDetailsVm, LessonListVm } from '../models/lesson.model';
import { ResponseDto } from '../models/response.model';
import { EnvironmentUrlService } from './environment-url/environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class LessonsService {
  basePath = 'api/lessons';

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  getLessonList(courseId: number): Observable<LessonListVm> {
    const params = new HttpParams()
      .set("courseid", courseId.toString());

    return this.http.get<LessonListVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      {params});
  }

  getLesson(id: number, courseId: number): Observable<LessonDetailsVm> {
    const params = new HttpParams()
      .set("courseid", courseId.toString());

    return this.http.get<LessonDetailsVm>(      
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${id}`),
      {params});
  }

  createLesson(createLessonDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      createLessonDto);
  }

  updateLesson(updateLessonDto: any): Observable<ResponseDto> {
    return this.http.put<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      updateLessonDto);
  }

  deleteLesson(id: number, courseId: number): Observable<ResponseDto> {
    const params = new HttpParams()
      .set("courseid", courseId.toString());

    return this.http.delete<ResponseDto>(    
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${id}`),
      {params});
  }

  private createCompleteRoute = (envAddress: string, route: string) => {
    return `${envAddress}/${route}`;
  }
}
