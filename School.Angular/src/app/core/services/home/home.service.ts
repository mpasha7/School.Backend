import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from '../environment-url/environment-url.service';
import { Observable } from 'rxjs';
import { PublicCourseDetailsVm, PublicCourseListVm } from '../../models/course.model';


@Injectable({
  providedIn: 'root'
})
export class HomeService {
  basePath = 'api/home';

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  getPublicCourseList(): Observable<PublicCourseListVm> {
    return this.http.get<PublicCourseListVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath)
    );
  }

  getPublicCourse(id: number): Observable<PublicCourseDetailsVm> {
    return this.http.get<PublicCourseDetailsVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${id}`)
    );
  }

  private createCompleteRoute = (envAddress: string, route: string) => {
    return `${envAddress}/${route}`;
  }
}
