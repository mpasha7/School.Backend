import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LessonDetailsVm, LessonListVm } from '../models/lesson.model';
import { ResponseDto } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class LessonsService {
  baseUrl = 'https://localhost:7125/api/lessons';

  constructor(private http: HttpClient) { }

  getLessonList(courseId: number): Observable<LessonListVm> {
    const params = new HttpParams()
      .set("courseid", courseId.toString());
    return this.http.get<LessonListVm>(this.baseUrl, {params});
  }

  getLesson(id: number, courseId: number): Observable<LessonDetailsVm> {
    const params = new HttpParams()
      .set("courseid", courseId.toString());
    return this.http.get<LessonDetailsVm>(`${this.baseUrl}/${id}`, {params});
  }

  createLesson(createLessonDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(this.baseUrl, createLessonDto);
  }

  updateLesson(updateLessonDto: any): Observable<ResponseDto> {
    return this.http.put<ResponseDto>(this.baseUrl, updateLessonDto);
  }

  deleteLesson(id: number, courseId: number): Observable<ResponseDto> {
    const params = new HttpParams()
      .set("courseid", courseId.toString());
    return this.http.delete<ResponseDto>(`${this.baseUrl}/${id}`, {params});
  }
}
