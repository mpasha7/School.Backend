import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from '../environment-url/environment-url.service';
import { Observable } from 'rxjs';
import { MessageListVm } from '../../models/message.model';
import { ResponseDto } from '../../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {
  basePath = 'api/messages';

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  getMessageList(courseId: number): Observable<MessageListVm> {
    const params = new HttpParams()
      .set("courseid", courseId.toString());

    return this.http.get<MessageListVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      {params}
    );
  }

  createMessage(createMessageDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      createMessageDto
    );
  }

  deleteMessage(id: number, courseId: number): Observable<ResponseDto> {
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
