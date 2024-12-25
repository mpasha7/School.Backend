import { Component, OnInit } from '@angular/core';
import { CourseLookupDto } from '../../core/models/course.model';
import { RouterLink } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { deleteCourse, loadCourseList } from '../../redux/courses/courses.actions';
import { selectCourseList } from '../../redux/courses/courses.selector';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './course-list.component.html',
  styleUrl: './course-list.component.css'
})
export class CourseListComponent implements OnInit {
  courseList!: CourseLookupDto[];

  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {
    this.store.dispatch(loadCourseList());
    this.store.select(selectCourseList).subscribe((data) => {
      this.courseList = data;
    });
  }

  onDeleteCourse(id: number) {
    if (confirm("Вы хотите удалить этот курс?")) {
      this.store.dispatch(deleteCourse({id: id}));
    }
  }
}
