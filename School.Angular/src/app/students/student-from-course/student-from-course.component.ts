import { Component, Inject, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { ActivatedRoute, Router } from '@angular/router';
import { DOCUMENT } from '@angular/common';
import { removeStudentFromCourse } from '../../redux/students/students.actions';
import { selectStudentsIds, selectStudentsLoading } from '../../redux/students/students.selector';
import { Constants } from '../../shared/constants/constants';

@Component({
  selector: 'app-student-from-course',
  standalone: true,
  templateUrl: './student-from-course.component.html',
  styleUrl: './student-from-course.component.css'
})
export class StudentFromCourseComponent implements OnInit {
  removeFromCourseDto!: any;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    this.activatedRoute.queryParams.subscribe((params: any) => this.removeFromCourseDto = JSON.parse(params["dto"]));
    this.store.dispatch(removeStudentFromCourse({removeFromCourseDto: this.removeFromCourseDto}));
  }

  ngOnInit(): void {
    this.store.select(selectStudentsLoading).subscribe((data) => {

    });
    this.router.navigate(["students/list"]);
  }

}
