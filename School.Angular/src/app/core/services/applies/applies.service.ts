import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from '../environment-url/environment-url.service';
import { Observable } from 'rxjs';
import { ApplyListVm } from '../../models/apply.model';
import { ResponseDto } from '../../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class AppliesService {
  basePath = 'api/applies';

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  getApplyList(courseId: number): Observable<ApplyListVm> {
    const params = new HttpParams()
      .set("courseid", courseId.toString());

    return this.http.get<ApplyListVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      {params}
    );
  }

  createApply(createApplyDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      createApplyDto
    );
  }

  updateApply(updateApplyDto: any): Observable<ResponseDto> {
    return this.http.put<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      updateApplyDto
    );
  }

  deleteApply(id: number, courseId: number): Observable<ResponseDto> {
    const params = new HttpParams()
      .set("courseid", courseId.toString());

    return this.http.delete<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${id}`),
      {params}
    );
  }

  private createCompleteRoute = (envAddress: string, route: string) => {
    return `${envAddress}/${route}`;
  }
}
