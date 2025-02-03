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
import { AuthService } from '../../core/services/auth/auth.service';

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
  isCoach: boolean = false;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService
  ) {
    this.activatedRoute.parent?.params.subscribe(params => this.courseId = params["courseid"]);
    this.getRole();
  }

  ngOnInit(): void {
    this.store.dispatch(loadLessonList({courseId: this.courseId}));
    this.store.select(selectLessonList).subscribe((data) => {
      this.lessonList = data;
    });
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

  getRole() {
    return this.authService.getUserRole().then(role => {
      if (role === 'Coach'){
        this.isCoach = true;
      }
    });
  }
}
