import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { LessonDetailsVm } from '../../core/models/lesson.model';
import { loadLesson } from '../../redux/lessons/lessons.actions';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { selectLesson } from '../../redux/lessons/lessons.selector';
import { selectCourseList } from '../../redux/courses/courses.selector';

@Component({
  selector: 'app-lesson-details',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './lesson-details.component.html',
  styleUrl: './lesson-details.component.css'
})
export class LessonDetailsComponent implements OnInit {
  selectedLesson!: LessonDetailsVm | null;
  lessonId!: number;
  courseId!: number;
  // courseTitle!: string | null;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.params.subscribe(params => this.lessonId = params["id"]);
    this.activatedRoute.parent?.params.subscribe(params => this.courseId = params["courseid"]);
  }
  ngOnInit(): void {
    this.store.dispatch(loadLesson({id: this.lessonId, courseId: this.courseId}));
    this.store.select(selectLesson).subscribe((data) => {
      this.selectedLesson = data;
    });
  }
}
