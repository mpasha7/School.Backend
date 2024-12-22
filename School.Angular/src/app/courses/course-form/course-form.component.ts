import { Component } from '@angular/core';
import { CoursesService } from '../../core/services/courses.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-course-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, RouterLink],
  templateUrl: './course-form.component.html',
  styleUrl: './course-form.component.css'
})
export class CourseFormComponent {

  createCourseForm = new FormGroup({
    title: new FormControl<string>(''),
    description: new FormControl<string>(''),
    shortDescription: new FormControl<string | null>(null),
    publicDescription: new FormControl<string | null>(null),
    photoPath: new FormControl<string | null>(null),
    beginQuestionnaire: new FormControl<string | null>(null),
    endQuestionnaire: new FormControl<string | null>(null),
  });

  constructor(private service: CoursesService, private router: Router) {}

  createCourse() {
    const createCourseDto = {
      title: this.createCourseForm.value.title,
      description: this.createCourseForm.value.description,
      shortDescription: this.createCourseForm.value.shortDescription,
      publicDescription: this.createCourseForm.value.publicDescription,
      photoPath: this.createCourseForm.value.photoPath,
      beginQuestionnaire: this.createCourseForm.value.beginQuestionnaire,
      endQuestionnaire: this.createCourseForm.value.endQuestionnaire,
    };

    this.service.createCourse(createCourseDto)
      .subscribe({
        next: (value) => {
          console.log("Create Course: id = " + value);
          this.router.navigate([""]);
        }
      })
  }
}
