import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from '../environment-url/environment-url.service';
import { Observable } from 'rxjs';
import { FeedbackDetailsVm } from '../../models/feedback.model';
import { ResponseDto } from '../../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class FeedbacksService {
  basePath = 'api/feedbacks';

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  getFeedback(courseId: number, lessonId: number, reportId: number)
      : Observable<FeedbackDetailsVm> {
    return this.http.get<FeedbackDetailsVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${courseId}/${lessonId}/${reportId}`)
    );
  }

  createFeedback(createFeedbackDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      createFeedbackDto
    );
  }

  private createCompleteRoute = (envAddress: string, route: string) => {
    return `${envAddress}/${route}`;
  }
}
