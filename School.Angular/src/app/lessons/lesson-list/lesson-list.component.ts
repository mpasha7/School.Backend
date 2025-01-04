import { Component, OnInit } from '@angular/core';
import { LessonLookupDto } from '../../core/models/lesson.model';
import { AppState } from '../../redux/store';
import { Store } from '@ngrx/store';
import { deleteLesson, loadLessonList } from '../../redux/lessons/lessons.actions';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { selectContainingCourse, selectLessonList, selectMaxLessonNumber } from '../../redux/lessons/lessons.selector';
import { selectCourseList } from '../../redux/courses/courses.selector';
import { JsonPipe } from '@angular/common';
import { Subscription } from 'rxjs';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-lesson-list',
  standalone: true,
  imports: [RouterLink, SharedModule],
  templateUrl: './lesson-list.component.html',
  styleUrl: './lesson-list.component.css'
})
export class LessonListComponent implements OnInit {
  lessonList!: LessonLookupDto[];
  courseId!: number;
  courseTitle!: string | null;
  maxLessonNumber!: number;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.parent?.params.subscribe(params => this.courseId = params["courseid"]);
  }

  ngOnInit(): void {
    this.store.dispatch(loadLessonList({courseId: this.courseId}));
    this.store.select(selectLessonList).subscribe((data) => {
      this.lessonList = data;
    });
    // this.lessonList.sort((a, b) => a.number - b.number);

    this.store.select(selectContainingCourse).subscribe((data) => {
      this.courseTitle = data?.title ? data.title : "";
    });
    this.store.select(selectMaxLessonNumber).subscribe((data) => {
      this.maxLessonNumber = data
    });
  }

  onDeleteLesson(id: number) {
    if (confirm("Вы хотите удалить этот урок?")) {
      this.store.dispatch(deleteLesson({id: id, courseId: this.courseId}));
    }
  }
}
