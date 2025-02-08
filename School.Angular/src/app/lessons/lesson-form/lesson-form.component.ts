import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router, RouterLink } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { createLesson, loadLesson, updateLesson } from '../../redux/lessons/lessons.actions';
import { selectContainingCourse, selectLesson } from '../../redux/lessons/lessons.selector';
import { selectCourse, selectCourseList } from '../../redux/courses/courses.selector';
import { loadCourse } from '../../redux/courses/courses.actions';
import { Observable } from 'rxjs';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-lesson-form',
  standalone: true,
  imports: [SharedModule, FormsModule, ReactiveFormsModule, RouterLink],
  templateUrl: './lesson-form.component.html',
  styleUrl: './lesson-form.component.css'
})
export class LessonFormComponent implements OnInit {
  lessonForm!: FormGroup;
  lessonId!: number;
  pageTitle: string = '';
  courseId!: number;
  courseTitle!: string | null;
  maxLessonNumber!: number;

  constructor(
    private store: Store<AppState>,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.lessonForm = new FormGroup({
      number: new FormControl<number>(0, [Validators.required]),
      title: new FormControl<string>('', [Validators.required, Validators.maxLength(200)]),
      description: new FormControl<string | null>('', [Validators.required]),
      videoLink: new FormControl<string | null>(null)
    });
    this.activatedRoute.params.subscribe(params => this.lessonId = params["id"]);
    this.activatedRoute.parent?.params.subscribe(params => this.courseId = params["courseid"]);
  }

  ngOnInit(): void {
    if (this.lessonId > 0) {
      this.pageTitle = "Редактирование урока";

      this.store.dispatch(loadLesson({id: this.lessonId, courseId: this.courseId}));
      this.store.select(selectLesson).subscribe((data) => {
        this.lessonForm.patchValue({
          number: data?.number,
          title: data?.title,
          description: data?.description,
          videoLink: data?.videoLink
        });        
      });
      this.store.select(selectContainingCourse).subscribe(data => this.courseTitle = data?.title ? data.title : "");
    }
    else {
      this.pageTitle = "Создание урока";

      this.activatedRoute.queryParams.subscribe((queryParams) => {
        this.maxLessonNumber = queryParams["maxNumber"];
      });
      this.maxLessonNumber++;
      this.lessonForm.patchValue({
        number: this.maxLessonNumber
      });
      this.store.dispatch(loadCourse({id: this.courseId}));
      this.store.select(selectCourse).subscribe(data => this.courseTitle = data?.title ? data.title : ""); // TODO:
    }
  }

  saveLessonData() {
    const createLessonDto = {
      courseId: this.courseId,
      number: this.lessonForm.value.number,
      title: this.lessonForm.value.title,
      description: this.lessonForm.value.description,
      videoLink: this.lessonForm.value.videoLink
    };

    if (this.lessonId > 0) {
      const updateLessonDto = {id: this.lessonId, ...createLessonDto};
      this.store.dispatch(updateLesson({updateLessonDto: updateLessonDto}));
    }
    else {
      this.store.dispatch(createLesson({createLessonDto: createLessonDto}));
    }
    this.router.navigate([`/lessons/${this.courseId}/list`]);
  }
}
