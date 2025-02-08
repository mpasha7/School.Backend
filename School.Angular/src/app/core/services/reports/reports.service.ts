import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from '../environment-url/environment-url.service';
import { Observable } from 'rxjs';
import { ReportDetailsVm, ReportListVm } from '../../models/report.model';
import { ResponseDto } from '../../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  basePath = 'api/reports';

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  getReportList(courseId: number): Observable<ReportListVm> {
    return this.http.get<ReportListVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${courseId}`)
    );
  }

  getReport(courseId: number, lessonId: number, id: number): Observable<ReportDetailsVm> {
    return this.http.get<ReportDetailsVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${courseId}/${lessonId}/${id}`)
    );
  }

  createReport(createReportDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      createReportDto
    );
  }

  private createCompleteRoute = (envAddress: string, route: string) => {
    return `${envAddress}/${route}`;
  }
}