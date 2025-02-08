import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseReportsComponent } from './course-reports.component';

describe('CourseReportsComponent', () => {
  let component: CourseReportsComponent;
  let fixture: ComponentFixture<CourseReportsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CourseReportsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseReportsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
