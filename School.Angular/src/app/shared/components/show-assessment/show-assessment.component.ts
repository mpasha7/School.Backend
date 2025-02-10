import { Component, Inject, OnInit } from '@angular/core';
import { AssessmentDetailsVm } from '../../../core/models/assessment.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-show-assessment',
  templateUrl: './show-assessment.component.html',
  styleUrl: './show-assessment.component.css'
})
export class ShowAssessmentComponent implements OnInit {
  assessment!: AssessmentDetailsVm | null;
  courseTitle!: string;

  constructor(
    private dialogRef: MatDialogRef<ShowAssessmentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {
    this.assessment = this.data.assessment;
    this.courseTitle = this.data.courseTitle;
  }

  closePopup() {
    this.dialogRef.close();
  }
}
