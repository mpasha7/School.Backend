import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { CourseDetailsVm } from '../../core/models/course.model';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { loadCourse } from '../../redux/courses/courses.actions';
import { selectCourse } from '../../redux/courses/courses.selector';

@Component({
  selector: 'app-course-quest',
  standalone: true,
  imports: [RouterLink, SharedModule],
  templateUrl: './course-quest.component.html',
  styleUrl: './course-quest.component.css'
})
export class CourseQuestComponent implements OnInit {
  courseId!: number;
  courseTitle!: string;
  isBegin!: boolean;
  questionaire!: string | undefined;
  subtitle!: string;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.params.subscribe(params => this.courseId = params['id']);
    this.activatedRoute.queryParams.subscribe((queryParams) => {
      this.courseTitle = queryParams["courseTitle"];
      this.isBegin = queryParams["begin"];
    });
  }

  ngOnInit(): void {
    this.store.dispatch(loadCourse({id: this.courseId}));
    this.store.select(selectCourse).subscribe((data) => {
      if (this.isBegin) {
        this.questionaire = data?.beginQuestionnaire ? data.beginQuestionnaire : undefined;
        this.subtitle = 'Анкета перед началом курса';
      }
      else {
        this.questionaire = data?.endQuestionnaire ? data.endQuestionnaire : undefined;
        this.subtitle = 'Анкета в конце курса';
      }
    });
  }

}
