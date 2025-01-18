import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { PublicCourseLookupDto } from '../../core/models/course.model';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { loadPublicCourseList } from '../../redux/home/home.actions';
import { selectHomeError, selectPublicCourseList } from '../../redux/home/home.selector';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, SharedModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  courseList!: PublicCourseLookupDto[];
  errorObject!: any;

  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {
    this.store.dispatch(loadPublicCourseList());
    this.store.select(selectPublicCourseList).subscribe((data) => {
      this.courseList = data;
    });
    this.store.select(selectHomeError).subscribe((data) => {
      this.errorObject = data;
    });
  }
}
