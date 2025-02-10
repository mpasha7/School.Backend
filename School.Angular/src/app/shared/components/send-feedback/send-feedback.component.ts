import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { AppState } from '../../../redux/store';
import { createFeedback } from '../../../redux/feedbacks/feedbacks.actions';
import { createAssessment } from '../../../redux/assessments/assessments.actions';

@Component({
  selector: 'app-send-feedback',
  templateUrl: './send-feedback.component.html',
  styleUrl: './send-feedback.component.css'
})
export class SendFeedbackComponent implements OnInit {
  feedbackForm: FormGroup;
  studentName!: string;
  lessonTitle!: string;
  reportId!: number;
  lessonId!: number;
  courseId!: number;
  isFinal!: boolean;
  courseTitle!: string;
  studentGuid!: string;

  constructor(
    private dialogRef: MatDialogRef<SendFeedbackComponent>,
    private store: Store<AppState>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.feedbackForm = new FormGroup({
      text: new FormControl<string>('', [Validators.required, Validators.maxLength(2000)]),
      assessmentText: new FormControl<string>('')
    });
  }

  ngOnInit(): void {
    this.studentName = this.data.studentName;
    this.lessonTitle = this.data.lessonTitle;
    this.reportId = this.data.reportId;
    this.lessonId = this.data.lessonId;
    this.courseId = this.data.courseId;
    this.isFinal = this.data.isFinal;
    this.courseTitle = this.data.courseTitle;
    this.studentGuid = this.data.studentGuid;
  }

  closePopup(reportId: number) {
    this.dialogRef.close(reportId);
  }

  saveFeedback() {
    if (this.feedbackForm.valid) {
      let createFeedbackDto = {
        text: this.feedbackForm.value.text,
        reportId: this.reportId,
        lessonId: this.lessonId,
        courseId: this.courseId
      }
      this.store.dispatch(createFeedback({createFeedbackDto: createFeedbackDto}));

      if (this.isFinal) {
        let createAssessmentDto = {
          studentGuid: this.studentGuid,
          text: this.feedbackForm.value.assessmentText,
          courseId: this.courseId
        }
        this.store.dispatch(createAssessment({createAssessmentDto: createAssessmentDto}))
      }

      this.closePopup(this.reportId);
    }
  }
}
