import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { PublicCourseDetailsVm } from '../../core/models/course.model';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { loadPublicCourse } from '../../redux/home/home.actions';
import { selectPublicCourse } from '../../redux/home/home.selector';

@Component({
  selector: 'app-public-course',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './public-course.component.html',
  styleUrl: './public-course.component.css'
})
export class PublicCourseComponent implements OnInit {
  selectedCourse!: PublicCourseDetailsVm | null;
  courseId!: number;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.params.subscribe(params => this.courseId = params['id']);
  }

  ngOnInit(): void {
    this.store.dispatch(loadPublicCourse({id: this.courseId}));
    this.store.select(selectPublicCourse).subscribe((data) => {
      this.selectedCourse = data;
    });
  }
}
