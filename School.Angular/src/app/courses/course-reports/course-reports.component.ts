import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { ReportDetailsVm, ReportLookupDto } from '../../core/models/report.model';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { AuthService } from '../../core/services/auth/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { loadReport, loadReportList } from '../../redux/reports/reports.actions';
import { selectMaxNumber, selectReport, selectReportList } from '../../redux/reports/reports.selector';
import { DatePipe } from '@angular/common';
import { selectMaxLessonNumber } from '../../redux/lessons/lessons.selector';
import { SendFeedbackComponent } from '../../shared/components/send-feedback/send-feedback.component';
import { ShowResultComponent } from '../../shared/components/show-result/show-result.component';

@Component({
  selector: 'app-course-reports',
  standalone: true,
  imports: [RouterLink, SharedModule, DatePipe],
  templateUrl: './course-reports.component.html',
  styleUrl: './course-reports.component.css'
})
export class CourseReportsComponent implements OnInit {
  courseId!: number;
  courseTitle!: string;
  reportList!: ReportLookupDto[];
  answeredReportsIds: number[] = [];
  maxLessonNumber: number = 0;
  selectedReport!: ReportDetailsVm | null;
  isCoach: boolean = false;
  expandedIndex = 0;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private dialog: MatDialog
  ) {
    this.activatedRoute.params.subscribe(params => this.courseId = params['id']);
    this.activatedRoute.queryParams.subscribe((queryParams) => {
      this.courseTitle = queryParams["courseTitle"];
    });
    this.getRole();
  }

  ngOnInit(): void {
    this.store.dispatch(loadReportList({courseId: this.courseId}));
    this.store.select(selectReportList).subscribe((data) => {
      this.reportList = data;
    });
    this.store.select(selectMaxNumber).subscribe((data) => {
      this.maxLessonNumber = data;
    });
  }

  getRole() {
    return this.authService.getUserRole().then(role => {
      if (role === 'Coach'){
        this.isCoach = true;
      }
    });
  }

  getReport(report: ReportLookupDto) {
    this.store.dispatch(loadReport({courseId: this.courseId, lessonId: report.lessonId, id: report.id}));
    this.store.select(selectReport).subscribe((data) => {
      this.selectedReport = data;
    });
  }

  sendFeedback(
    studentName: string,
    lessonTitle: string,
    reportId: number,
    lessonId: number,
    lessonNumber: number,
    studentGuid: string
  ) {
    const dialogRef = this.dialog.open(
      SendFeedbackComponent,
      {
        width: '50%',
        data: {
          studentName: studentName,
          lessonTitle: lessonTitle,
          reportId: reportId,
          lessonId: lessonId,
          courseId: this.courseId,
          isFinal: lessonNumber == this.maxLessonNumber,
          courseTitle: this.courseTitle,
          studentGuid: studentGuid
        }
      }
    );
    dialogRef.afterClosed().subscribe(result => {
      if ((result as number) > 0) {
        this.showSendResult('Создание обратной связи');
        this.answeredReportsIds.push(result);
      }
    });
  }

  showSendResult(resultText: string) {
    this.dialog.open(
      ShowResultComponent,
      {
        width: '50%',
        data: {
          result: true,
          resultText: resultText
        }
      }
    );
  }
}
