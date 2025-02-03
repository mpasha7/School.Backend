import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { LessonDetailsVm } from '../../core/models/lesson.model';
import { loadLesson } from '../../redux/lessons/lessons.actions';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { selectLesson } from '../../redux/lessons/lessons.selector';
import { selectCourseList } from '../../redux/courses/courses.selector';
import { SharedModule } from '../../shared/shared.module';
import { AuthService } from '../../core/services/auth/auth.service';

@Component({
  selector: 'app-lesson-details',
  standalone: true,
  imports: [RouterLink, SharedModule],
  templateUrl: './lesson-details.component.html',
  styleUrl: './lesson-details.component.css'
})
export class LessonDetailsComponent implements OnInit {
  selectedLesson!: LessonDetailsVm | null;
  lessonId!: number;
  courseId!: number;
  isCoach: boolean = false;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService
  ) {
    this.activatedRoute.params.subscribe(params => this.lessonId = params['id']);
    this.activatedRoute.parent?.params.subscribe(params => this.courseId = params['courseid']);
    this.getRole();
  }
  
  ngOnInit(): void {
    this.store.dispatch(loadLesson({id: this.lessonId, courseId: this.courseId}));
    this.store.select(selectLesson).subscribe((data) => {
      this.selectedLesson = data;
    });
  }

  getRole() {
    return this.authService.getUserRole().then(role => {
      if (role === 'Coach'){
        this.isCoach = true;
      }
    });
  }
}
