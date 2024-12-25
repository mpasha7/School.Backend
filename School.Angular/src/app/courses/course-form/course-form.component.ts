import { Component, OnInit } from '@angular/core';
import { CoursesService } from '../../core/services/courses.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AppState } from '../../redux/store';
import { Store } from '@ngrx/store';
import { createCourse, loadCourse, updateCourse } from '../../redux/courses/courses.actions';
import { CourseDetailsVm, CreateCourseDto, UpdateCourseDto } from '../../core/models/course.model';
import { selectCourse } from '../../redux/courses/courses.selector';
import { JsonPipe } from '@angular/common';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-course-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, RouterLink],
  templateUrl: './course-form.component.html',
  styleUrl: './course-form.component.css'
})
export class CourseFormComponent implements OnInit {
  courseForm: FormGroup;
  pageTitle: string = '';
  courseId!: number;

  constructor(
    private store: Store<AppState>,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.courseForm = new FormGroup({
      title: new FormControl<string>(''),
      description: new FormControl<string>(''),
      shortDescription: new FormControl<string | null>(null),
      publicDescription: new FormControl<string | null>(null),
      photoPath: new FormControl<string | null>(null),
      beginQuestionnaire: new FormControl<string | null>(null),
      endQuestionnaire: new FormControl<string | null>(null),
    });
    this.courseId = this.activatedRoute.snapshot.params["id"];
    // this.activatedRoute.parent?.params.subscribe(params => this.courseId = params["courseid"]);
  }

  ngOnInit(): void {
    if (this.courseId > 0) {
      this.pageTitle = "Редактирование курса";

      this.store.dispatch(loadCourse({id: this.courseId}));

      this.store.select(selectCourse).subscribe((data) => {
        this.courseForm.patchValue({
          title: data?.title,
          description: data?.description,
          shortDescription: data?.shortDescription,
          publicDescription: data?.publicDescription,
          photoPath: data?.photoPath,
          beginQuestionnaire: data?.beginQuestionnaire,
          endQuestionnaire: data?.endQuestionnaire
        });
      });
    }
    else {      
      this.pageTitle = "Создание курса";
    }
  }

  saveCourseData() {
    const createCourseDto = {
      title: this.courseForm.value.title,
      description: this.courseForm.value.description,
      shortDescription: this.courseForm.value.shortDescription,
      publicDescription: this.courseForm.value.publicDescription,
      photoPath: this.courseForm.value.photoPath,
      beginQuestionnaire: this.courseForm.value.beginQuestionnaire,
      endQuestionnaire: this.courseForm.value.endQuestionnaire
    };

    if (this.courseId > 0) {
      const updateCourseDto = {id: this.courseId, ...createCourseDto};
      this.store.dispatch(updateCourse({updateCourseDto: updateCourseDto}));
    }
    else {
      this.store.dispatch(createCourse({createCourseDto: createCourseDto}));
    }
    this.router.navigate([""]);
  }
}
