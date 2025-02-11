import { Component, OnInit } from '@angular/core';
import { LessonLookupDto } from '../../core/models/lesson.model';
import { AppState } from '../../redux/store';
import { Store } from '@ngrx/store';
import { deleteLesson, loadLessonList } from '../../redux/lessons/lessons.actions';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { selectContainingCourse, selectLessonList, selectMaxLessonNumber } from '../../redux/lessons/lessons.selector';
import { SharedModule } from '../../shared/shared.module';
import { AuthService } from '../../core/services/auth/auth.service';
import { loadAssessment } from '../../redux/assessments/assessments.actions';
import { selectAssessment } from '../../redux/assessments/assessments.selector';
import { AssessmentDetailsVm } from '../../core/models/assessment.model';
import { MatDialog } from '@angular/material/dialog';
import { ShowAssessmentComponent } from '../../shared/components/show-assessment/show-assessment.component';
import { SendCommentComponent } from '../../shared/components/send-comment/send-comment.component';
import { ShowResultComponent } from '../../shared/components/show-result/show-result.component';

@Component({
  selector: 'app-lesson-list',
  standalone: true,
  imports: [RouterLink, SharedModule],
  templateUrl: './lesson-list.component.html',
  styleUrl: './lesson-list.component.css'
})
export class LessonListComponent implements OnInit {
  lessonList!: LessonLookupDto[];
  courseId!: number;
  courseTitle!: string | null;
  maxLessonNumber!: number;
  isCoach: boolean = false;
  assessment!: AssessmentDetailsVm | null;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private dialog: MatDialog,
    private authService: AuthService
  ) {
    this.activatedRoute.parent?.params.subscribe(params => this.courseId = params["courseid"]);
    this.getRole();
  }

  ngOnInit(): void {
    this.store.dispatch(loadLessonList({courseId: this.courseId}));
    this.store.select(selectLessonList).subscribe((data) => {
      this.lessonList = data;
    });
    this.store.select(selectContainingCourse).subscribe((data) => {
      this.courseTitle = data?.title ? data.title : "";
    });
    this.store.select(selectMaxLessonNumber).subscribe((data) => {
      this.maxLessonNumber = data
    });
  }

  onDeleteLesson(id: number) {
    if (confirm("Вы хотите удалить этот урок?")) {
      this.store.dispatch(deleteLesson({id: id, courseId: this.courseId}));
    }
  }

  getRole() {
    return this.authService.getUserRole().then(role => {
      if (role === 'Coach'){
        this.isCoach = true;
      }
    });
  }

  getAssessment() {
    this.store.dispatch(loadAssessment({courseId: this.courseId}));
    this.store.select(selectAssessment).subscribe((data) => {
      this.assessment = data;
      this.showAssessment();
    });
  }

  showAssessment() {
    this.dialog.open(
      ShowAssessmentComponent,
      {
        width: '50%',
        data: {
          assessment: this.assessment,
          courseTitle: this.courseTitle
        }
      }
    );
  }

  sendComment() {
    const dialogRef = this.dialog.open(
      SendCommentComponent,
      {
        width: '50%',
        data: {
          courseId: this.courseId,
          courseTitle: this.courseTitle
        }
      }
    );
    dialogRef.afterClosed().subscribe(_ => {
      this.showSendResult('Создание комментария');
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
