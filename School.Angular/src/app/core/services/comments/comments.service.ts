import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from '../environment-url/environment-url.service';
import { Observable } from 'rxjs';
import { CommentListVm } from '../../models/comment.model';
import { ResponseDto } from '../../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {
  basePath = 'api/comments';

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  getCommentList(courseId: number): Observable<CommentListVm> {
    return this.http.get<CommentListVm>(
      this.createCompleteRoute(this.envUrl.urlAddress, `${this.basePath}/${courseId}`)
    );
  }

  createComment(createCommentDto: any): Observable<ResponseDto> {
    return this.http.post<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      createCommentDto
    );
  }

  updateComment(updateCommentDto: any): Observable<ResponseDto> {
    return this.http.put<ResponseDto>(
      this.createCompleteRoute(this.envUrl.urlAddress, this.basePath),
      updateCommentDto
    );
  }

  private createCompleteRoute = (envAddress: string, route: string) => {
    return `${envAddress}/${route}`;
  }
}
