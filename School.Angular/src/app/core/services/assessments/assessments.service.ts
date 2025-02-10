import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from '../environment-url/environment-url.service';
import { Observable } from 'rxjs';
import { AssessmentDetailsVm } from '../../models/assessment.model';
import { ResponseDto } from '../../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class AssessmentsService {
  basePath = 'api/assessments';

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  getAssessment(courseId: number): Observable<AssessmentDetailsVm> {
    return this.http.get<AssessmentDetailsVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${courseId}`)
    );
  }

  createAssessment(createAssessmentDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      createAssessmentDto
    );
  }

  private createCompleteRoute = (envAddress: string, route: string) => {
    return `${envAddress}/${route}`;
  }
}
