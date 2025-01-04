import { Component, OnInit } from '@angular/core';
import { CourseLookupDto } from '../../core/models/course.model';
import { RouterLink } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { deleteCourse, loadCourseList } from '../../redux/courses/courses.actions';
import { selectCourseError, selectCourseList } from '../../redux/courses/courses.selector';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [RouterLink, SharedModule],
  templateUrl: './course-list.component.html',
  styleUrl: './course-list.component.css'
})
export class CourseListComponent implements OnInit {
  courseList!: CourseLookupDto[];
  errorObject!: any;

  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {
    this.store.dispatch(loadCourseList());
    this.store.select(selectCourseList).subscribe((data) => {
      this.courseList = data;
    });
    this.store.select(selectCourseError).subscribe((data) => {
      this.errorObject = data;
    });
  }

  onDeleteCourse(id: number) {
    if (confirm("Вы хотите удалить этот курс?")) {
      this.store.dispatch(deleteCourse({id: id}));
    }
  }
}
