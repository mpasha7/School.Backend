import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { DatePipe, NgClass } from '@angular/common';
import { CommentListVm, CommentLookupDto } from '../../core/models/comment.model';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { AuthService } from '../../core/services/auth/auth.service';
import { loadCommentList, updateComment } from '../../redux/comments/comments.actions';
import { selectCommentList } from '../../redux/comments/comments.selector';

@Component({
  selector: 'app-course-comments',
  standalone: true,
  imports: [RouterLink, SharedModule, DatePipe, NgClass],
  templateUrl: './course-comments.component.html',
  styleUrl: './course-comments.component.css'
})
export class CourseCommentsComponent implements OnInit {
  courseId!: number;
  courseTitle!: string;
  commentList!: CommentLookupDto[];
  expandedIndex = 0;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService
  ) {
    this.activatedRoute.params.subscribe(params => this.courseId = params['id']);
    this.activatedRoute.queryParams.subscribe((queryParams) => {
      this.courseTitle = queryParams["courseTitle"];
    });
  }

  ngOnInit(): void {
    this.store.dispatch(loadCommentList({courseId: this.courseId}));
    this.store.select(selectCommentList).subscribe((data) => {
      this.commentList = data;
    });
  }

  updateComment(id: number) {
    let updateCommentDto = {
      id: id,
      courseId: this.courseId
    };
    this.store.dispatch(updateComment({updateCommentDto: updateCommentDto}));
    this.store.select(selectCommentList).subscribe((data) => {
      this.commentList = data;
    });
  }
}
