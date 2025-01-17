import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from '../environment-url/environment-url.service';
import { AuthService } from '../auth/auth.service';


// Лишний
@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  public getData = (route: string) => {
    return this.http.get(this.createCompleteRoute(this.envUrl.urlAddress, route));
  }

  public postData = (route: string, createCourseFormData: FormData) => {
    return this.http.post(
      this.createCompleteRoute(this.envUrl.urlAddress, route),
      createCourseFormData
    );
  }

  public putData = (route: string, updateCourseFormData: FormData) => {
    return this.http.put(
      this.createCompleteRoute(this.envUrl.urlAddress, route),
      updateCourseFormData
    );
  }

  public deleteData = (route: string) => {
    return this.http.delete(this.createCompleteRoute(this.envUrl.urlAddress, route));
  }

  private createCompleteRoute = (envAddress: string, route: string) => {
    return `${envAddress}/${route}`;
  }
}
