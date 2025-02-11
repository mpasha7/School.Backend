import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { AppState } from '../../../redux/store';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { createComment } from '../../../redux/comments/comments.actions';

@Component({
  selector: 'app-send-comment',
  templateUrl: './send-comment.component.html',
  styleUrl: './send-comment.component.css'
})
export class SendCommentComponent implements OnInit {
  commentForm: FormGroup;
  courseId!: number;
  courseTitle!: string;

  constructor(
    private dialogRef: MatDialogRef<SendCommentComponent>,
    private store: Store<AppState>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.commentForm = new FormGroup({
      text: new FormControl<string>('', [Validators.required, Validators.maxLength(2000)])
    });
  }

  ngOnInit(): void {
    this.courseId = this.data.courseId;
    this.courseTitle = this.data.courseTitle;
  }

  closePopup() {
    this.dialogRef.close();
  }

  saveComment() {
    if (this.commentForm.valid) {
      let createCommentDto = {
        text: this.commentForm.value.text,
        courseId: this.courseId
      }
      this.store.dispatch(createComment({createCommentDto: createCommentDto}));
    }
    this.closePopup();
  }
}
