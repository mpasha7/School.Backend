import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { addStudentToCourse, loadStudentIds } from '../../redux/students/students.actions';
import { selectStudentsIds, selectStudentsLoading } from '../../redux/students/students.selector';
import { StudentsIdsVm } from '../../core/models/student.model';
import { DOCUMENT, JsonPipe } from '@angular/common';
import { Constants } from '../../shared/constants/constants';

@Component({
  selector: 'app-student-to-course',
  standalone: true,
  imports: [JsonPipe],
  templateUrl: './student-to-course.component.html',
  styleUrl: './student-to-course.component.css'
})
export class StudentToCourseComponent implements OnInit {
  addToCourseDto!: any;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { 
    this.activatedRoute.queryParams.subscribe((params: any) => this.addToCourseDto = JSON.parse(params["dto"]));
    this.store.dispatch(addStudentToCourse({addToCourseDto: this.addToCourseDto}));
  }

  ngOnInit(): void {
    this.store.select(selectStudentsLoading).subscribe((data) => {

    });
    this.router.navigate(["students/list"]);
  }
}
