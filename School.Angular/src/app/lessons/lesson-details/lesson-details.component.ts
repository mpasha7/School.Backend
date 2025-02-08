import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { LessonDetailsVm } from '../../core/models/lesson.model';
import { loadLesson } from '../../redux/lessons/lessons.actions';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { selectLesson } from '../../redux/lessons/lessons.selector';
import { selectCourseList } from '../../redux/courses/courses.selector';
import { SharedModule } from '../../shared/shared.module';
import { AuthService } from '../../core/services/auth/auth.service';
import { FormArray, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { createReport } from '../../redux/reports/reports.actions';
import { ReportDetailsVm } from '../../core/models/report.model';
import { lastValueFrom, tap, timeout } from 'rxjs';

@Component({
  selector: 'app-lesson-details',
  standalone: true,
  imports: [RouterLink, SharedModule, FormsModule, ReactiveFormsModule],
  templateUrl: './lesson-details.component.html',
  styleUrl: './lesson-details.component.css'
})
export class LessonDetailsComponent implements OnInit {
  selectedLesson!: LessonDetailsVm | null;
  report!: ReportDetailsVm | null | undefined;
  lessonId!: number;
  courseId!: number;
  isCoach: boolean = false;
  showReport: boolean = false;
  reportForm: FormGroup;
  files: File[] | null = null;
  fileLabels: string = 'Файлы не выбраны';

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private router: Router
  ) {
    this.activatedRoute.params.subscribe(params => this.lessonId = params['id']);
    this.activatedRoute.parent?.params.subscribe(params => this.courseId = params['courseid']);
    this.getRole();

    this.reportForm = new FormGroup ({
      text: new FormControl<string>('', [Validators.required])
    });
  }
  
  ngOnInit(): void {
    this.store.dispatch(loadLesson({id: this.lessonId, courseId: this.courseId}));
    this.store.select(selectLesson).subscribe((data) => {
      this.selectedLesson = data;
      this.report = this.selectedLesson?.report;
    });
  }

  getRole() {
    return this.authService.getUserRole().then(role => {
      if (role === 'Coach'){
        this.isCoach = true;
      }
    });
  }

  onFileChange(event: any) {
    const formFiles: File[] = event.target.files;
    if (formFiles) {
      this.files = formFiles;
      this.fileLabels = '';
      for (const ff of formFiles) {
        this.fileLabels += `${ff.name};`;
      }
    }
  }

  async saveReportData() {
    const formData = new FormData();
    if (this.files){
      for(let f of this.files) {
        formData.append('file', f, f.name);
      }
    }
    formData.append('lessonid', this.lessonId.toString());
    formData.append('courseid', this.courseId.toString());
    formData.append('text', this.reportForm.value.text);

    this.store.dispatch(createReport({createReportDto: formData}));
    this.ngOnInit();    // TODO: Переделать!
    this.ngOnInit();
    this.ngOnInit();
    this.showReport = true;

    // this.store.select(selectLesson).subscribe((data) => {
    //   this.selectedLesson = data;
    //   this.report = this.selectedLesson?.report;
    // });

    // this.selectedLesson = await lastValueFrom(this.store.select(selectLesson));
    // this.report = this.selectedLesson?.report;
    // this.ngOnInit();

    // this.store.select(selectLesson).pipe(tap({
    //   next: (data) => {
    //     this.selectedLesson = data;
    //     this.report = this.selectedLesson?.report;
    //   }
    // }));
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
}

  cancelForm() {
    this.reportForm.patchValue({
      text: ''
    });
    this.files = null;
    this.fileLabels = 'Файлы не выбраны';
  }
}
