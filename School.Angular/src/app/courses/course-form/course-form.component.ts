import { Component, OnInit } from '@angular/core';
import { CoursesService } from '../../core/services/courses.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AppState } from '../../redux/store';
import { Store } from '@ngrx/store';
import { createCourse, loadCourse, updateCourse } from '../../redux/courses/courses.actions';
import { CourseDetailsVm, CreateCourseDto, UpdateCourseDto } from '../../core/models/course.model';
import { selectCourse } from '../../redux/courses/courses.selector';
import { JsonPipe } from '@angular/common';
import { Subscription } from 'rxjs';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-course-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, RouterLink, SharedModule],
  templateUrl: './course-form.component.html',
  styleUrl: './course-form.component.css'
})
export class CourseFormComponent implements OnInit {
  courseForm: FormGroup;
  pageTitle: string = '';
  courseId!: number;
  file: File | null = null;
  fileLabel: string = 'Файл не выбран';

  constructor(
    private store: Store<AppState>,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.courseForm = new FormGroup({
      title: new FormControl<string>('', [Validators.required, Validators.maxLength(200)]),
      description: new FormControl<string>('', [Validators.required]),
      shortDescription: new FormControl<string | null>('', [Validators.required]),
      publicDescription: new FormControl<string | null>('', [Validators.required]),
      beginQuestionnaire: new FormControl<string | null>(null),
      endQuestionnaire: new FormControl<string | null>(null),
    });
    this.courseId = this.activatedRoute.snapshot.params["id"];
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
          // photoPath: data?.photoPath,
          beginQuestionnaire: data?.beginQuestionnaire,
          endQuestionnaire: data?.endQuestionnaire
        });
      });
    }
    else {      
      this.pageTitle = "Создание курса";
    }
  }

  onFileChange(event: any) {
    const formFile = event.target.files[0];
    if (formFile) {
      this.file = formFile;
      this.fileLabel = formFile.name;
    }
  }

  saveCourseData() {
    const formData = new FormData();
    if (this.file){
      formData.append('file', this.file, this.file.name);
    }
    formData.append('title', this.courseForm.value.title);
    formData.append('description', this.courseForm.value.description);
    formData.append('shortdescription', this.courseForm.value.shortDescription);
    formData.append('publicdescription', this.courseForm.value.publicDescription);
    formData.append('beginquestionnaire', this.courseForm.value.beginQuestionnaire);
    formData.append('endquestionnaire', this.courseForm.value.endQuestionnaire);

    if (this.courseId > 0) {
      formData.append('id', this.courseId.toString());
      this.store.dispatch(updateCourse({updateCourseDto: formData}));
    }
    else {
      this.store.dispatch(createCourse({createCourseDto: formData}));
    }
    this.router.navigate([""]);
  }
}
