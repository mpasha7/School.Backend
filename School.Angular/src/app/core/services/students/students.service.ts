import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EnvironmentUrlService } from '../environment-url/environment-url.service';
import { Student, StudentsIdsVm } from '../../models/student.model';
import { ResponseDto } from '../../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  basePath = 'api/students';
  branchPath = 'branch/api/students'

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  getStudentsIds(): Observable<StudentsIdsVm> {
    return this.http.get<StudentsIdsVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath)
    );
  }

  addStudentToCourse(addToCourseDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/add`),
      addToCourseDto
    );
  }

  removeStudentFromCourse(removeFromCourseDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/remove`),
      removeFromCourseDto
    );
  }


  ///////////
  getStudentsByIds(ids: string): Observable<Student[]> {
    return this.http.get<Student[]>(
      this.createCompleteRoute(this.envUrl.authAddress, `${this.branchPath}/byids/${ids}`)
    );
  }

  getStudentsBySearch(search: string): Observable<Student[]> {
    return this.http.get<Student[]>(
      this.createCompleteRoute(this.envUrl.authAddress, `${this.branchPath}/bysearch/${search}`)
    );
  }

  private createCompleteRoute = (envAddress: string, route: string) => {
    return `${envAddress}/${route}`;
  }
}
