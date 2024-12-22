import { Component, OnInit } from '@angular/core';
import { CoursesService } from '../../core/services/courses.service';
import { CourseListVm } from '../../core/models/course.model';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [AsyncPipe, RouterLink],
  templateUrl: './course-list.component.html',
  styleUrl: './course-list.component.css'
})
export class CourseListComponent implements OnInit {
  courses$!: Observable<CourseListVm>;

  constructor(private service: CoursesService) {}

  ngOnInit(): void {
    this.courses$ = this.service.getCourseList();
  }

  deleteCourse(id: number) {
    if (!confirm()) { return; }

    this.service.deleteCourse(id)
      .subscribe({
        next: (value) => {
          this.courses$ = this.service.getCourseList();
        }
      })
  }
}
