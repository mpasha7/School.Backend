import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { ApplyLookupDto } from '../../core/models/apply.model';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { MatDialog } from '@angular/material/dialog';
import { deleteApply, loadApplyList, updateApply } from '../../redux/applies/applies.actions';
import { selectApplyError, selectApplyList } from '../../redux/applies/applies.selector';
import { ShowResultComponent } from '../../shared/components/show-result/show-result.component';

@Component({
  selector: 'app-course-applies',
  standalone: true,
  imports: [RouterLink, SharedModule],
  templateUrl: './course-applies.component.html',
  styleUrl: './course-applies.component.css'
})
export class CourseAppliesComponent implements OnInit {
  courseId!: number;
  courseTitle!: string;
  applyList!: ApplyLookupDto[];
  actionResult: boolean = false;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private dialog: MatDialog
  ) {
    this.activatedRoute.params.subscribe(params => this.courseId = params['id']);
    this.activatedRoute.queryParams.subscribe((queryParams) => {
      this.courseTitle = queryParams["courseTitle"];
    });
  }

  ngOnInit(): void {
    this.store.dispatch(loadApplyList({courseId: this.courseId}));
    this.store.select(selectApplyList).subscribe((data) => {
      this.applyList = data;
    });
  }

  onAcceptApply(apply: ApplyLookupDto) {
    if (confirm("Вы хотите принять эту заявку?")) {
      const updateApplyDto = {
        id: apply.id,
        studentGuid: apply.studentGuid,
        courseId: this.courseId
      }
      this.store.dispatch(updateApply({updateApplyDto: updateApplyDto}));
      this.store.select(selectApplyError).subscribe((data) => {
        this.actionResult = !data ? true : false;
      });
      this.showResult('Принятие заявки');
    }    
  }

  onRejectApply(apply: ApplyLookupDto) {
    if (confirm("Вы хотите удалить эту заявку?")) {
      this.store.dispatch(deleteApply({id: apply.id, courseId: this.courseId}));
      this.store.select(selectApplyError).subscribe((data) => {
        this.actionResult = !data ? true : false;
      });
      this.showResult('Удаление заявки');
    }    
  }

  showResult(resultText: string) {
    this.dialog.open(
      ShowResultComponent,
      {
        width: '50%',
        data: {
          result: this.actionResult,
          resultText: resultText
        }
      }
    );
  }
}
